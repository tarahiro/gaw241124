using Cysharp.Threading.Tasks;
using gaw241124;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using Tarahiro.TInput;
using UniRx;
using UnityEngine;
using Tarahiro.TGrid;
using UnityEngine.Tilemaps;
using VContainer;
using VContainer.Unity;

namespace gaw241124.View
{
    public class StonePutView : MonoBehaviour, IStonePutView
    {
        [Inject] IGridProvider _gridProvider;

        [SerializeField] TileBase _tileBase;


        public void PutStone(Vector3Int _position)
        {
            _gridProvider.GetTilemap((int)Const.TilemapLayer.Stone).SetTile(_position, _tileBase);
        }

    }
}