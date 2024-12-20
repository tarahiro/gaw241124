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
    public interface ITurnModel
    {
        void Enter();

        void Exit(bool isClear);

        void TryGoNextSideTurn();

        IObservable<Unit> Entered { get; }
        IObservable<bool> Exited { get; }
    }
}