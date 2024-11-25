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

        private void Awake()
        {
            GridPaintUtil.PaintOnAvailableTile(_tileBase, (int)Const.TilemapLayer.Cross, _gridProvider, (int)Const.TilemapLayer.Ground, (int)Const.Positionable.Groundable);
        }
    }
}