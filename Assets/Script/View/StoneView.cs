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
    public class StoneView : MonoBehaviour, IStoneView
    {
        [Inject] IGridProvider _gridProvider;

        [SerializeField] TileBase _tileBase;

        Subject<Vector2Int> _fieldTouched = new Subject<Vector2Int>();
        public IObservable<Vector2Int> FieldTouched => _fieldTouched;

        public void PutStone(Vector3Int _position)
        {
            _gridProvider.GetTilemap((int)Const.TilemapLayer.Stone).SetTile(_position, _tileBase);
        }

        void Update()
        {
            if(TTouch.GetInstance().State == TouchConst.TouchState.Begin)
            {
                var hit = TTouch.GetInstance().Hit2D;
                if (hit.collider)
                {
                    _fieldTouched.OnNext(GridUtil.ConvertPosition(hit.point));
                }
            }
        }
    }
}