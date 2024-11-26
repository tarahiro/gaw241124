using Cysharp.Threading.Tasks;
using gaw241124;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using Tarahiro.TGrid;
using Tarahiro.TInput;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241124.View
{
    public class CameraFollowObjectView : MonoBehaviour
    {
        [Inject] IGridProvider _gridProvider;

        private void Update()
        {

            Vector2 vec = TTouch.GetInstance().ScreenPointOnThisFrame;

            vec.x = Mathf.Max(vec.x, 0);
            vec.x = Mathf.Min(vec.x, Tarahiro.Const.Resolution.x);

            vec.y = Mathf.Max(vec.y, 0);
            vec.y = Mathf.Min(vec.y, Tarahiro.Const.Resolution.y);

            var tilemap = _gridProvider.GetTilemap((int)Const.TilemapLayer.Ground);

            Vector3 tileposMin = tilemap.GetCellCenterWorld(tilemap.origin);
            Vector3 tileposMax = tilemap.GetCellCenterWorld(tilemap.origin + tilemap.size);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(vec);


            worldPosition.x = Mathf.Max(worldPosition.x, tileposMin.x);
            worldPosition.x = Mathf.Min(worldPosition.x, tileposMax.x);

            worldPosition.y = Mathf.Max(worldPosition.y, tileposMin.y);
            worldPosition.y = Mathf.Min(worldPosition.y, tileposMax.y);

            transform.position = worldPosition;
        }
    }
}