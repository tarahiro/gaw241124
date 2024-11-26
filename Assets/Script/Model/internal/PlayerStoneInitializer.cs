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
    public class PlayerStoneInitializer:IPlayerStoneInitializer
    {
        [Inject] IPlayerHoldStoneModel _holdStoneModel;
        [Inject] IPlayerStonePutter _putterModel;
        [Inject] IGridProvider _gridProvider;
        [Inject] StoneProvider stoneProvider;

        readonly static Vector2Int _initialStonePosition = new Vector2Int(-3, 1);
        public void InitializeModel()
        {
            _holdStoneModel.AddStone(5);

            var tilemap = _gridProvider.GetTilemap((int)Const.TilemapLayer.Stone);
            

            for(int i = tilemap.origin.x; i < tilemap.origin.x + tilemap.size.x; i++)
            {
                for(int j = tilemap.origin.y; j < tilemap.origin.y + tilemap.size.y; j++)
                {
                    Vector2Int vec = new Vector2Int(i, j);
                    if(tilemap.GetTile((Vector3Int)vec) == stoneProvider.GetTilebase(Const.Side.Player))
                    {
                        _putterModel.PutStone(vec);

                    }

                }
            }
        }
    }
}