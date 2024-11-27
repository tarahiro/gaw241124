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
    public class EnemyGroupPresenter : IPostInitializable
    {
        [Inject] IEnemyGroupStoneContainer _stoneContainer;
        [Inject] IEnemyStoneView _stoneView;
        [Inject] IEyesightView _eyesightView;
        [Inject] IPercieveNotifyView _percieveNotifyView;
        [Inject] IAtariNotifyView _atariNotifyView;
        [Inject] IPlayerStoneView _playerStoneView;
        [Inject] IEnemyGroupStonePutter _enemyGroupStonePutter;

        [Inject] CompositeDisposable _compositeDisposable;

        public void PostInitialize()
        {
            _stoneContainer.Arounded.Subscribe(OnArounded).AddTo(_compositeDisposable);
            _stoneContainer.EyesightStarted.Subscribe(_eyesightView.PutEyesight).AddTo(_compositeDisposable);
            _stoneContainer.PlayerPercieved.Subscribe(_percieveNotifyView.ShowIcon).AddTo(_compositeDisposable);
            _stoneContainer.Ataried.Subscribe(_atariNotifyView.ShowIcon).AddTo(_compositeDisposable);
            _stoneContainer.InitializeModel(_compositeDisposable);
            _enemyGroupStonePutter.StoneArounded.Subscribe(x =>
            {
                _playerStoneView.RemoveStone(x);
                _stoneContainer.RemovePlayerStone(x);
            }).AddTo(_compositeDisposable);
        }

        void OnArounded(List<Vector2Int> positionList) 
        {
            foreach (var position in positionList)
            {
                _stoneView.RemoveStone(position);
            }

        }
    }
}