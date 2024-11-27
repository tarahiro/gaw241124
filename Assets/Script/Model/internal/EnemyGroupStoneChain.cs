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
    public class EnemyGroupStoneChain : IEnemyGroupStoneChain
    {
        Vector2Int _initialStonePosition;
        List<Vector2Int> _initialEyesightDirection;
        IGridProvider _gridProvider;
        StoneProvider _stoneProvider;


        public EnemyGroupStoneChain(Vector2Int position,List<Vector2Int> eyesightDirection , IGridProvider gridProvider, StoneProvider stoneProvider)
        {
            _initialStonePosition = position;
            _initialEyesightDirection = eyesightDirection;
            _gridProvider = gridProvider;
            _stoneProvider = stoneProvider;
        }

        List<Vector2Int> _directionList;
        Tilemap _map;
        Subject<List<Vector2Int>> _aroundFilled = new Subject<List<Vector2Int>>();
        Subject<Vector2Int> _eyesightStarted = new Subject<Vector2Int>();

        public List<Vector2Int> StonePositionList { get; set; } = new List<Vector2Int>();
        public List<Vector2Int> EmptyAroundList { get; set; } = new List<Vector2Int>();
        public List<Vector2Int> EyesightList { get; set; } = new List<Vector2Int>();

        public IObservable<List<Vector2Int>> AroundFilled => _aroundFilled;
        public IObservable<Vector2Int> EyesightStarted => _eyesightStarted;


        public void Initialize()
        {
            _directionList = GridUtil.GetDirectionList();
            _map = _gridProvider.GetTilemap((int)Const.TilemapLayer.Stone);

            AddStone(_initialStonePosition, _initialEyesightDirection);

        }

        void RegisterStonePosition(Vector2Int stonePosition)
        {
            for (int i = 0; i < _directionList.Count; i++)
            {
                var p = stonePosition + _directionList[i];
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

        void RegisterEmptyAroundPosition(Vector2Int stonePosition)
        {
            for (int i = 0; i < _directionList.Count; i++)
            {
                var v = stonePosition + _directionList[i];
                if (_gridProvider.IsPositionable(v, (int)Const.Positionable.EnemyStone))
                {
                    if (!EmptyAroundList.Contains(v))
                    {
                        EmptyAroundList.Add(v);
                    }
                }
            }
        }

        void RegisterEyeSight(List<Vector2Int> eyesightPosition)
        {
            foreach (var e in eyesightPosition)
            {
                if (_gridProvider.IsPositionable(e, (int)Const.Positionable.EnemyStone))
                {

                    EyesightList.Add(e);
                    _eyesightStarted.OnNext(e);
                }
            }
        }

        public bool IsKilledIfStonePutted(Vector2Int position)
        {
            Log.DebugAssert("–¢ŽÀ‘•");
            return false;
        }

        public bool IsAtari()
        {
            return EmptyAroundList.Count == 1;
        }

        public void GetNoticeStoneOnAround(Vector2Int position)
        {
            EmptyAroundList.Remove(position);

            if(EmptyAroundList.Count == 0)
            {
                _aroundFilled.OnNext(StonePositionList);
            }
        }

        public void AddStone(Vector2Int position, List<Vector2Int> eyesightDirection)
        {
            StonePositionList.Add(position);
            EmptyAroundList.Remove(position);

            RegisterEmptyAroundPosition(position);

            List<Vector2Int> vec = new List<Vector2Int>();
            Log.DebugLog("EyeSight");
            foreach (var e in eyesightDirection)
            {
                Log.DebugLog("EyeSightAdd");
                vec.Add(position + e);
            }
            RegisterEyeSight(vec);
        }

        public void RemovePlayerStone(Vector2Int position)
        {
            if (!EmptyAroundList.Contains(position))
            {
                EmptyAroundList.Add(position);
            }
        }
    }
}