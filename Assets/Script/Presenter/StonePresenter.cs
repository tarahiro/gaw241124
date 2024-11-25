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
    public class StonePresenter: IPostInitializable
    {
        [Inject] IPlayerStonePutTryer _stonePutTryer;
        [Inject] IStonePutterModel _model;
        [Inject] IPlayerStoneInitializer _playerStoneInitializer;
        [Inject] IPlayerHoldStoneModel _holdStoneModel;
        [Inject] IStonePutView _view;
        [Inject] IPlayerInputView _gridTouchView;
        [Inject] IStoneNumberUiView _numberUiView;

        CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void PostInitialize()
        {
            _model.StonePutted.Subscribe(OnCreateStone).AddTo(_compositeDisposable);
            _holdStoneModel.StoneUpdated.Subscribe(_numberUiView.UpdateStoneNumber).AddTo(_compositeDisposable);
            _gridTouchView.FieldTouched.Subscribe(_stonePutTryer.TryPutStone).AddTo(_compositeDisposable);

            _playerStoneInitializer.InitializeModel();
        }

        void OnCreateStone(StonePositionArgs positionArgs)
        {
            _view.PutStone(positionArgs);
        }
    }
}