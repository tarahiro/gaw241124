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
    public class StonePresenter:IInitializable
    {
        [Inject] IStoneModel _model;
        [Inject] IStoneView _view;
        [Inject] IStoneNumberUiView _numberUiView;

        CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void Initialize()
        {
            _model.StonePutted.Subscribe(OnCreateStone).AddTo(_compositeDisposable);
            _model.StoneUpdated.Subscribe(_numberUiView.UpdateStoneNumber).AddTo(_compositeDisposable);
            _view.FieldTouched.Subscribe(_model.PutStone).AddTo(_compositeDisposable);

            _model.InitializeModel();
        }

        void OnCreateStone(Vector2Int position)
        {
            _view.PutStone((Vector3Int)position);
        }
    }
}