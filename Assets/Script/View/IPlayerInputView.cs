using Cysharp.Threading.Tasks;
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
    public interface IPlayerInputView
    {
        void Enter();
        void Exit();
        IObservable<Vector2Int> FieldTouched { get; }
    }
}