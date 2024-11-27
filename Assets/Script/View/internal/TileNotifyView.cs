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
    public class TileNotifyView : ITileNotifyView
    {
        [Inject] Func<Vector3, TileNotifyItemView> _factory;
        [Inject] IGridProvider _gridProvider;

        public void ShowIcon(Vector2Int position)
        {
            Log.DebugLogComment("発見アイコン表示");
            var v = _factory.Invoke(_gridProvider.GetTilemap((int)Const.TilemapLayer.Ground).CellToWorld((Vector3Int)position));
            v.Enter().Forget();
        }
    }
}