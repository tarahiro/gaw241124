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
    public class CrossView : MonoBehaviour
    {
        [Inject] IGridProvider _gridProvider;

        [SerializeField] TileBase _tileBase;

        private void Start()
        {
            var ground = _gridProvider.GetTilemap((int)Const.TilemapLayer.Ground);
            var cross = _gridProvider.GetTilemap((int)Const.TilemapLayer.Grid);
            for (int i = ground.origin.x; i < ground.size.x + ground.origin.x; i++)
            {
                for (int j = ground.origin.y; j < ground.size.y + ground.origin.x; j++)
                {
                    var v = new Vector2Int(i, j);
                    if (_gridProvider.IsPositionable(v, (int)Const.Positionable.Grid))
                    {
                        cross.SetTile((Vector3Int)v,_tileBase);
                    }
                }
            }
        }
    }
}