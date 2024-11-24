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
    public class EnemyPresenter : IPostInitializable
    {
        [Inject] IEnemyStoneContainer _stoneContainer;
        [Inject] IEnemyStoneView _stoneView;
        [Inject] IEnemyBrain _brain;
        [Inject] IEnemyTurn _turn;
        [Inject] IEyesightView _eyesightView;

        CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void PostInitialize()
        {
            _stoneContainer.Arounded.Subscribe(OnArounded).AddTo(_compositeDisposable);
            _stoneContainer.EyesightStarted.Subscribe(_eyesightView.PutEyesight).AddTo(_compositeDisposable);
            _turn.TurnEntered.Subscribe(_ => _brain.Enter().Forget()).AddTo(_compositeDisposable);
            _brain.BrainEnded.Subscribe(_=>_turn.Exit()).AddTo(_compositeDisposable);

            _stoneContainer.InitializeModel(_compositeDisposable);
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