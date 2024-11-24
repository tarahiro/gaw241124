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

namespace gaw241124.View
{
    public interface ITreasureView
    {
        IObservable<ITreasureItemView> ItemRegistered { get; }

        void DestroyTreasure(int index);

        void InitializeView();
    }
}