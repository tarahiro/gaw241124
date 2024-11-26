using Cysharp.Threading.Tasks;
using gaw241124;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tarahiro;
using Tarahiro.TGrid;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241124.Model
{
    public class EnemyGroupBrain : IEnemyGroupBrain
    {
        IEnemyGroupStoneContainer _container;
        IEnemyGroupStonePutTryer _putTryer;
        EnemyGroupStatus _status;

        [Inject]
        public EnemyGroupBrain(IEnemyGroupStoneContainer container, IEnemyGroupStonePutTryer putTryer, EnemyGroupStatus status)
        {
            _container = container;
            _putTryer = putTryer;
            _status = status;
        }


        public async UniTask Enter()
        {
            await UniTask.WaitForSeconds(.2f);

            bool isStonePutted = false;

            Log.DebugLog("EnemyBrain開始");

            //アタリの石がいたら、逃げる
            if (_container.TryGetAtariStone(out var v))
            {
                Log.DebugLog("AtariExist");
                isStonePutted =  _putTryer.TryPutStone(v.EmptyAroundList[0],new List<Vector2Int>());
            }

            if (!isStonePutted)
            {
                if (_status.IsPercievePlayer)
                {
                    //気づいたプレイヤーの石の周囲のうち、Enemyの石の重心からもっとも近いところに置く
                    var list = GridUtil.GetDirectionList();

                    List<DistanceSortPosition> distanceSortPositionList = new List<DistanceSortPosition>();

                    for (int i = 0; i < _status.PercievedPlayerStone.Count; i++)
                    {
                        for (int j = 0; j < list.Count; j++)
                        {
                           
                            Vector2 diff = _status.PercievedPlayerStone[i] + list[j] - _container.Centroid();
                            float f = diff.sqrMagnitude;
                            distanceSortPositionList.Add(new DistanceSortPosition(_status.PercievedPlayerStone[i] + list[j], f));
                        }
                    }

                    var query =  distanceSortPositionList.OrderBy(x => x.Distance);

                    foreach(var item in query)
                    {
                        isStonePutted = _putTryer.TryPutStone(item.Position, new List<Vector2Int>() );
                        if (isStonePutted) break;
                    }


                }
            }


        }

    }

    class DistanceSortPosition
    {
        public Vector2Int Position { get; set; }
        public float Distance { get; set; }

        public DistanceSortPosition(Vector2Int position, float distance)
        {
            Position = position;
            Distance = distance;
        }
    }
}