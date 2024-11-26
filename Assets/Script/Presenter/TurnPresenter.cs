using Cysharp.Threading.Tasks;
using gaw241124.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace gaw241124.Presenter
{
    public class TurnPresenter : IInitializable
    {
        [Inject] ITurnModel _model;
        [Inject] ITurn _turn;
        CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void Initialize()
        {
            _turn.TurnExited.Subscribe(_ => _model.TryGoNextSideTurn()).AddTo(_compositeDisposable);
        }
    }
}