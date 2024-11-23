using Cysharp.Threading.Tasks;
using gaw241124;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using Tarahiro.TInput;
using UniRx;
using UnityEngine;
using UnityEngine.Tilemaps;
using VContainer;
using VContainer.Unity;

namespace gaw241124.View
{
    public class StoneView : MonoBehaviour, IStoneView
    {
        [SerializeField] Tilemap _tileMap;
        [SerializeField] TileBase _tileBase;

        Subject<Vector2Int> _fieldTouched = new Subject<Vector2Int>();
        public IObservable<Vector2Int> FieldTouched => _fieldTouched;

        public void PutStone(Vector3Int _position)
        {
            _tileMap.SetTile(_position, _tileBase);
        }

        void Update()
        {
            if(TTouch.GetInstance().State == TouchConst.TouchState.Begin)
            {
                var hit = TTouch.GetInstance().Hit2D;
                if (hit.collider)
                {
                    Vector2Int vector2Int = new Vector2Int((int)Mathf.Round(hit.point.x - .5f), (int)Mathf.Round(hit.point.y - .5f));
                    Log.DebugLog(vector2Int);
                    _fieldTouched.OnNext(vector2Int);
                }
            }
        }
    }
}