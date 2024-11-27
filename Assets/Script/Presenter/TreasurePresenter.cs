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
    public class TreasurePresenter : IInitializable
    {
        [Inject] ITreasureView _view;
        [Inject] ITreasureModel _model;
        [Inject] IPlayerHoldStoneModel _holdStoneModel;
        [Inject] ITreasureNotifyView _notifyView;
        [Inject] Func<ITreasureItemView, TreasureModelItemArgs> _factory;

        CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void Initialize()
        {
            Log.DebugLogComment("TreasurePresenteèâä˙âª");

            _view.ItemRegistered.Subscribe(OnRegisterItem).AddTo(_compositeDisposable);
            _model.TreasureAchieved.Subscribe(_view.DestroyTreasure).AddTo(_compositeDisposable);
            _model.AddStoneTreasureAchieved.Subscribe(_holdStoneModel.AddStone).AddTo(_compositeDisposable);
            _model.AddStoneTreasureAchievedVec.Subscribe(_notifyView.ShowIcon).AddTo(_compositeDisposable);

            _view.InitializeView();
            _notifyView.InitializeView();
        }

        void OnRegisterItem(ITreasureItemView _itemView)
        {
            _model.RegisterTreasure(_factory.Invoke(_itemView));
        }
    }
}