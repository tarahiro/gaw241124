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
    public class EnemyGroupStonePutter : IEnemyGroupStonePutter
    {
        [Inject] IGridProvider _gridProvider;
        [Inject] StoneProvider _stoneProvider;
        IStonePutterModel _stonePutterModel;
        IEnemyGroupStoneContainer _container;
        Func<Const.Side, Vector2Int, StonePositionArgs> _factory;

        Subject<Vector2Int> _stoneArounded = new Subject<Vector2Int>();
        public IObservable<Vector2Int> StoneArounded => _stoneArounded;

        [Inject]
        public EnemyGroupStonePutter(IStonePutterModel stonePutterModel,
IEnemyGroupStoneContainer container, Func<Const.Side, Vector2Int, StonePositionArgs> factory)
        {
            _stonePutterModel = stonePutterModel;
            _container = container;
            _factory = factory;
        }


        public void PutStone(Vector2Int position, List<Vector2Int> eyesightDirection)
        {
            _stonePutterModel.PutStone(_factory.Invoke(Const.Side.Enemy, position));

            _container.NoticeEnemyStone(position,eyesightDirection);

            foreach (var item in GridUtil.GetDirectionList())
            {
                Vector3Int vec = (Vector3Int)(item + position);
                if (_gridProvider.GetTilemap((int)Const.TilemapLayer.Stone).GetTile(vec) == _stoneProvider.GetTilebase(Const.Side.Player))
                {
                    if (GridUtil.GetDirectionList().All(x =>
                    {
                        var vec2 = (Vector2Int)vec + x;

                        bool groundable = _gridProvider.IsPositionable(vec2, (int)Const.Positionable.Groundable);
                        if (groundable)
                        {
                            return _gridProvider.GetTilemap((int)Const.TilemapLayer.Stone).GetTile((Vector3Int)vec2) == _stoneProvider.GetTilebase(Const.Side.Enemy);
                        }
                        else
                        {
                            return true;
                        }
                    }
                    )
                        )
                    {
                        Log.DebugLog("ÉvÉåÉCÉÑÅ[ÇÃêŒÇàÕÇ¡ÇΩ");
                        _stoneArounded.OnNext((Vector2Int)vec);
                    }
                }
            }
        }
    }
}