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
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace gaw241124.View
{
    public class HideView : MonoBehaviour, IHideView
    {
        [Inject] IGridProvider _gridProvider;

        [SerializeField] TileBase _tileBase;

        private void Awake()
        {
          //  GridPaintUtil.PaintOnAvailableTile(_tileBase, (int)Const.TilemapLayer.Hide, _gridProvider, (int)Const.TilemapLayer.Ground, (int)Const.Positionable.Groundable);
        }

        public void Hide(Vector2Int position)
        {
            _gridProvider.GetTilemap((int)Const.TilemapLayer.Hide).SetTile((Vector3Int)position, _tileBase);

        }

        public void ShowMap(Vector2Int position)
        {
            _gridProvider.GetTilemap((int)Const.TilemapLayer.Hide).SetTile((Vector3Int)position, null);
        }
    }
}