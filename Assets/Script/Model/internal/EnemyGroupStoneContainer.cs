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
    public class EnemyGroupStoneContainer : IEnemyGroupStoneContainer, IEnemyGroupDefeatable
    {
        IGridProvider _gridProvider;
        StoneProvider _stoneProvider;
        EnemyGroupStatus _enemyStatus;
        
        Func<Vector2Int, List<Vector2Int>, IEnemyGroupStoneChain> _factory;


        [Inject]
        public EnemyGroupStoneContainer(IGridProvider gridProvider, StoneProvider stoneProvider, EnemyGroupStatus enemyStatus,
            Func<Vector2Int, List<Vector2Int>, IEnemyGroupStoneChain> factory)
        {
            _gridProvider = gridProvider;
            _stoneProvider = stoneProvider;
            _enemyStatus = enemyStatus;
            _factory = factory;
        }


        List<IEnemyGroupStoneChain> _stoneChainList = new List<IEnemyGroupStoneChain>();
        Subject<List<Vector2Int>> _arounded = new Subject<List<Vector2Int>>();
        Subject<Vector2Int> _eyesightStarted = new Subject<Vector2Int>();
        Subject<Vector2Int> _PlayerPercieved = new Subject<Vector2Int>();
        CompositeDisposable _disposable;

        Subject<Unit> _defeated = new Subject<Unit>();

        public IObservable<Unit> Defeated => _defeated;
        public IObservable<List<Vector2Int>> Arounded => _arounded;
        public IObservable<Vector2Int> EyesightStarted => _eyesightStarted;
        public IObservable<Vector2Int> PlayerPercieved => _PlayerPercieved;

        public void InitializeModel(CompositeDisposable disposable)
        {
            _disposable = disposable;
        }

        void RegisterStoneChain(Vector2Int position, List<Vector2Int> eyesightDirection)
        {
            var chain = _factory.Invoke(position, eyesightDirection);
            chain.AroundFilled.Subscribe(OnArounded).AddTo(_disposable);
            chain.EyesightStarted.Subscribe(OnEyesightStarted).AddTo(_disposable);
            chain.Initialize();

            _stoneChainList.Add(chain);
        }

        List<IEnemyGroupStoneChain> _stackedDeleteStoneChainList;

        public void TryNoticePlayerStone(Vector2Int position)
        {
            Log.DebugLog("TryNoticePlayerStone");

            // 処理中にリストが変更される可能性がある
            _stackedDeleteStoneChainList = new List<IEnemyGroupStoneChain>();

            foreach (var stoneChain in _stoneChainList)
            {
                if (stoneChain.EmptyAroundList.Contains(position))
                {
                    stoneChain.GetNoticeStoneOnAround(position);


                    //StoneChain側で感知した方がいいかも
                    if (stoneChain.EyesightList.Contains(position))
                    {
                        _enemyStatus.PercievedPlayerStone.Add(position);
                        _enemyStatus.IsPercievePlayer = true;
                        _PlayerPercieved.OnNext(position);
                    }
                }
            }

            foreach(var stackedStoneChain in _stackedDeleteStoneChainList)
            {
                _stoneChainList.Remove(stackedStoneChain);
                if (_stoneChainList.Count == 0)
                {
                    _defeated.OnNext(Unit.Default);
                }
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

        public bool TryGetAtariStone(out IEnemyGroupStoneChain enemyStoneChain)
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

        public void NoticeEnemyStone(Vector2Int position, List<Vector2Int> eyesightDirection)
        {
            List<IEnemyGroupStoneChain> adjacentChainList = new List<IEnemyGroupStoneChain>();

            foreach (var chain in _stoneChainList)
            {
                if (chain.EmptyAroundList.Contains(position))
                {
                    adjacentChainList.Add(chain);
                }
            }

            //隣接Chainが2個以上だったら、連結
            if(adjacentChainList.Count > 1)
            {
                adjacentChainList[0].AddStone(position,eyesightDirection);
                for(int i = 1; i < adjacentChainList.Count; i++)
                {
                    for(int j = 0; j < adjacentChainList[i].StonePositionList.Count; j++)
                    {
                        adjacentChainList[0].AddStone(adjacentChainList[i].StonePositionList[j],eyesightDirection);
                    }
                }

                for (int i = 1; i < adjacentChainList.Count; i++)
                {
                    _stoneChainList.Remove(adjacentChainList[i]);
                }


            }
            //隣接chainが1個だったら、石を足すだけ
            else if (adjacentChainList.Count == 1)
            {

                adjacentChainList[0].AddStone(position,eyesightDirection);
            }
            //隣接chainが0個だったら、新chainとして登録
            else
            {
                RegisterStoneChain(position, eyesightDirection);
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