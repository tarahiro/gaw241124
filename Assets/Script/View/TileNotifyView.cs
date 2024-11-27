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

namespace gaw241124.View
{
    public abstract class TileNotifyView : ITileNotifyView
    {
        [Inject] protected Func<Vector3, string, ITileNotifyItemView> _factory;
        [Inject] protected IGridProvider _gridProvider;

        protected string _prefabName;

        public virtual void InitializeView()
        {

        }

        public virtual void ShowIcon(Vector2Int position)
        {
            var v = _factory.Invoke(_gridProvider.GetTilemap((int)Const.TilemapLayer.Ground).CellToWorld((Vector3Int)position), _prefabName);
            v.Enter().Forget();
        }
    }
}