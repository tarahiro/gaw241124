using Cysharp.Threading.Tasks;
using gaw241124.Model;
using gaw241124.View;
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
    public class HidePresenter:IInitializable
    {
        [Inject] IHideModel _model;
        [Inject] IHideView _view;

        CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void Initialize()
        {
            _model.GroundShowed.Subscribe(_view.ShowMap).AddTo(_compositeDisposable);
            _model.Hided.Subscribe(_view.Hide).AddTo(_compositeDisposable);

            _model.InitializeModel();
        }
    }
}