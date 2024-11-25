using Cysharp.Threading.Tasks;
using gaw241124;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using Tarahiro;
using Tarahiro.TGrid;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace gaw241124.Model
{
    public class EnemyStoneContainer : IEnemyStoneContainer
    {
        [Inject] IGridProvider _gridProvider;
        [Inject] StoneProvider _stoneProvider;
        [Inject] Func<Vector2Int, IEnemyStoneChain> _factory;
        [Inject] EnemyStatus _enemyStatus;

        List<IEnemyStoneChain> _stoneChainList = new List<IEnemyStoneChain>();
        Subject<List<Vector2Int>> _arounded = new Subject<List<Vector2Int>>();
        Subject<Vector2Int> _eyesightStarted = new Subject<Vector2Int>();
        CompositeDisposable _disposable;

        public IObservable<List<Vector2Int>> Arounded => _arounded;
        public IObservable<Vector2Int> EyesightStarted => _eyesightStarted;

        public void InitializeModel(CompositeDisposable disposable)
        {
            var map = _gridProvider.GetTilemap((int)Const.TilemapLayer.Stone);
            _disposable = disposable;

            for (int i = map.origin.x; i < map.origin.x + map.size.x; i++)
            {
                for (int j = map.origin.y; j < map.origin.y + map.size.y; j++)
                {
                    var v = new Vector2Int(i, j);
                    if (map.GetTile((Vector3Int)v) == _stoneProvider.GetTilebase(Const.Side.Enemy))
                    {
                        RegisterStoneChain(v);
                    }
                }
            }
        }

        void RegisterStoneChain(Vector2Int position)
        {
            var chain = _factory.Invoke(position);
            chain.AroundFilled.Subscribe(OnArounded).AddTo(_disposable);
            chain.EyesightStarted.Subscribe(OnEyesightStarted).AddTo(_disposable);
            chain.Initialize();

            _stoneChainList.Add(chain);
        }

        List<IEnemyStoneChain> _stackedDeleteStoneChainList;

        public void TryNoticePlayerStone(Vector2Int position)
        {
            // �������Ƀ��X�g���ύX�����\��������
            _stackedDeleteStoneChainList = new List<IEnemyStoneChain>();

            foreach (var stoneChain in _stoneChainList)
            {
                if (stoneChain.EmptyAroundList.Contains(position))
                {
                    stoneChain.GetNoticeStoneOnAround(position);


                    //StoneChain���Ŋ��m����������������
                    if (stoneChain.EyesightList.Contains(position))
                    {
                        _enemyStatus.PercievedPlayerStone.Add(position);
                        _enemyStatus.IsPercievePlayer = true;
                    }
                }
            }

            foreach(var stackedStoneChain in _stackedDeleteStoneChainList)
            {
                _stoneChainList.Remove(stackedStoneChain);
            }
        }

        public bool IsKillAnyStoneChainIfPutted(Vector2Int position)
        {
            foreach (var stone in _stoneChainList)
            {
                if (stone.IsKilledIfStonePutted(position))
                {
                    return true;
                }
            }
            return false;
        }

        public bool TryGetAtariStone(out IEnemyStoneChain enemyStoneChain)
        {
            enemyStoneChain = null;
            foreach (var stone in _stoneChainList)
            {
                if (stone.IsAtari())
                {
                    enemyStoneChain = stone;
                    return true;
                }
            }
            return false;
        }

        void OnArounded(List<Vector2Int> positionList)
        {
            _arounded.OnNext(positionList);

            foreach(var p in positionList)
            {
                foreach(var chain in _stoneChainList)
                {
                    if (chain.StonePositionList.Contains(p))
                    {
                        _stackedDeleteStoneChainList.Add(chain);
                        return;
                    }
                }
            }
        }

        void OnEyesightStarted(Vector2Int position)
        {
            _eyesightStarted.OnNext(position);
        }

        public void NoticeEnemyStone(Vector2Int position)
        {
            List<IEnemyStoneChain> adjacentChainList = new List<IEnemyStoneChain>();

            foreach (var chain in _stoneChainList)
            {
                if (chain.EmptyAroundList.Contains(position))
                {
                    adjacentChainList.Add(chain);
                }
            }

            //�א�Chain��2�ȏゾ������A�A��
            if(adjacentChainList.Count > 1)
            {
                adjacentChainList[0].AddStone(position);
                for(int i = 1; i < adjacentChainList.Count; i++)
                {
                    for(int j = 0; j < adjacentChainList[i].StonePositionList.Count; j++)
                    {
                        adjacentChainList[0].AddStone(adjacentChainList[i].StonePositionList[j]);
                    }
                }

                for (int i = 1; i < adjacentChainList.Count; i++)
                {
                    _stoneChainList.Remove(adjacentChainList[i]);
                }


            }
            //�א�chain��1��������A�΂𑫂�����
            else if (adjacentChainList.Count == 1)
            {

                adjacentChainList[0].AddStone(position);
            }
            //�א�chain��0��������A�Vchain�Ƃ��ēo�^
            else
            {
                RegisterStoneChain(position);
            }
        }

        public Vector2 Centroid()
        {
            Vector2 v = Vector2.zero;
            int stoneCount = 0;

            foreach (var stoneChain in _stoneChainList)
            {
                foreach(var stone in stoneChain.StonePositionList)
                {
                    v += stone;
                    stoneCount++;
                }
            }

            return v / stoneCount;

        }
    }
}