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
    public class GridTouchView : ITickable, IGridTouchView
    {
        Subject<Vector2Int> _fieldTouched = new Subject<Vector2Int>();
        public IObservable<Vector2Int> FieldTouched => _fieldTouched;
        public void Tick()
        {
            if (TTouch.GetInstance().State == TouchConst.TouchState.Begin)
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