using Cysharp.Threading.Tasks;
using gaw241124;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using Tarahiro.TGrid;
using UniRx;
using UnityEngine;
using UnityEngine.Tilemaps;
using VContainer;
using VContainer.Unity;

namespace gaw241124.Model
{
    public class EnemyStoneChain : IEnemyStoneChain
    {
        public EnemyStoneChain(Vector2Int position, IGridProvider gridProvider, StoneProvider stoneProvider)
        {
            StonePositionList.Add(position);
            _gridProvider = gridProvider;
            _stoneProvider = stoneProvider;
        }

        IGridProvider _gridProvider;
        StoneProvider _stoneProvider;
        List<Vector2Int> _directionList;
        Tilemap _map;
        Subject<List<Vector2Int>> _aroundFilled = new Subject<List<Vector2Int>>();

        public List<Vector2Int> StonePositionList { get; set; } = new List<Vector2Int>();
        public List<Vector2Int> EmptyAroundList { get; set; } = new List<Vector2Int>();
        public IObservable<List<Vector2Int>> AroundFilled => _aroundFilled;


        public void Initialize()
        {
            _directionList = GridUtil.GetDirectionList();
            _map = _gridProvider.GetTilemap((int)Const.TilemapLayer.Stone);

            //é¸àÕÇÃêŒÇéÊìæ
            RegisterStonePosition(StonePositionList[0]);

            //EmptyAroundListÇéÊìæ
            foreach (var _stonePosition in StonePositionList)
            {
                for (int i = 0; i < _directionList.Count; i++)
                {
                    var v = _stonePosition + _directionList[i];
                    if (_gridProvider.IsPositionable(v, (int)Const.Positionable.EnemyStone))
                    {
                        if (!EmptyAroundList.Contains(v))
                        {
                            EmptyAroundList.Add(v);
                        }
                    }
                }
            }

        }

        void RegisterStonePosition(Vector2Int v)
        {
            for (int i = 0; i < _directionList.Count; i++)
            {
                var p = v + _directionList[i];
                if (_map.GetTile((Vector3Int)p) == _stoneProvider.GetTilebase(Const.Side.Enemy))
                {
                    if (!StonePositionList.Contains(p))
                    {
                        StonePositionList.Add(p);
                        RegisterStonePosition(p);
                    }
                }
            }

        }

        public bool IsKilledIfStonePutted(Vector2Int position)
        {
            Log.DebugAssert("ñ¢é¿ëï");
            return false;
        }

        public void GetNoticeStoneOnAround(Vector2Int position)
        {
            EmptyAroundList.Remove(position);

            if(EmptyAroundList.Count == 0)
            {
                _aroundFilled.OnNext(StonePositionList);
            }
        }
    }
}