using Cysharp.Threading.Tasks;
using gaw241124;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241124.Model
{
    public interface IHideModel
    {

        void InitializeModel();
        void ClearHide(Vector2Int position);
        IObservable<Vector2Int> Hided { get; }
        IObservable<Vector2Int> GroundShowed { get; }
    }
}