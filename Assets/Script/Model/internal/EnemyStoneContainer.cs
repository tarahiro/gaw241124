using Cysharp.Threading.Tasks;
using gaw241124;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using Tarahiro.TGrid;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241124.Model
{
    public class EnemyStoneContainer : IEnemyStoneContainer
    {
        [Inject] IGridProvider _gridProvider;
        [Inject] StoneProvider _stoneProvider;
        [Inject] Func<Vector2Int, IEnemyStoneChain> _factory;

        List<IEnemyStoneChain> _stoneChainList = new List<IEnemyStoneChain>();
        Subject<List<Vector2Int>> _arounded = new Subject<List<Vector2Int>>();


        public IObservable<List<Vector2Int>> Arounded => _arounded;

        public void InitializeModel(CompositeDisposable _disposable)
        {

            //とりあえず単一の石のみ
            var map = _gridProvider.GetTilemap((int)Const.TilemapLayer.Stone);

            for (int i = map.origin.x; i < map.origin.x + map.size.x; i++)
            {
                for (int j = map.origin.y; j < map.origin.y + map.size.y; j++)
                {
                    var v = new Vector2Int(i, j);
                    if (map.GetTile((Vector3Int)v) == _stoneProvider.GetTilebase(Const.Side.Enemy))
                    {
                        var chain = _factory.Invoke(v);

                        chain.AroundFilled.Subscribe(OnArounded).AddTo(_disposable);

                        chain.Initialize();

                        _stoneChainList.Add(chain);
                    }
                }
            }
        }

        List<IEnemyStoneChain> _stackedDeleteStoneChainList;

        public void TryNoticePlayerStone(Vector2Int position)
        {
            // 処理中にリストが変更される可能性がある
            _stackedDeleteStoneChainList = new List<IEnemyStoneChain>();

            foreach (var stoneChain in _stoneChainList)
            {
                if (stoneChain.EmptyAroundList.Contains(position))
                {
                    stoneChain.GetNoticeStoneOnAround(position);
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

        public void NoticeEnemyStone(Vector2Int position)
        {
            foreach (var stone in _stoneChainList)
            {
                if (stone.EmptyAroundList.Contains(position))
                {
                    stone.AddStone(position);
                }
            }

        }
    }
}