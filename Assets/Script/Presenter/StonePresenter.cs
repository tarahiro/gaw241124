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
        [Inject] IStonePutTryer _stonePutTryer;
        [Inject] IStonePutterModel _model;
        [Inject] IPlayerStoneInitializer _playerStoneInitializer;
        [Inject] IPlayerHoldStoneModel _holdStoneModel;
        [Inject] IStoneView _view;
        [Inject] IStoneNumberUiView _numberUiView;

        CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void Initialize()
        {
            _model.StonePutted.Subscribe(OnCreateStone).AddTo(_compositeDisposable);
            _holdStoneModel.StoneUpdated.Subscribe(_numberUiView.UpdateStoneNumber).AddTo(_compositeDisposable);
            _view.FieldTouched.Subscribe(_stonePutTryer.TryPutStone).AddTo(_compositeDisposable);

            _playerStoneInitializer.InitializeModel();
        }

        void OnCreateStone(Vector2Int position)
        {
            _view.PutStone((Vector3Int)position);
        }
    }
}