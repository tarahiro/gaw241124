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

namespace gaw241124.View
{
    public static class GridPaintUtil
    {
        public static void PaintOnAvailableTile(TileBase paintTile, int paintTileId, IGridProvider _gridProvider, int referenceTileId, int positionableId)
        {
            var ground = _gridProvider.GetTilemap(referenceTileId);
            var cross = _gridProvider.GetTilemap(paintTileId);

            for (int i = ground.origin.x; i < ground.size.x + ground.origin.x; i++)
            {
                for (int j = ground.origin.y; j < ground.size.y + ground.origin.x; j++)
                {
                    var v = new Vector2Int(i, j);
                    if (_gridProvider.IsPositionable(v, positionableId))
                    {
                        cross.SetTile((Vector3Int)v, paintTile);
                    }
                }
            }
        }
    }
}