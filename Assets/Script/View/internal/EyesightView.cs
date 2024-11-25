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
    public class EyesightView: MonoBehaviour,IEyesightView

    {
        [Inject] IGridProvider _gridProvider;

        [SerializeField] TileBase _tileBase;

        public void PutEyesight(Vector2Int position)
        {
            _gridProvider.GetTilemap((int)Const.TilemapLayer.Stone).SetTile((Vector3Int)position, _tileBase);
        }
    }
}