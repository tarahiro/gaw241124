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

namespace gaw241124
{
    public class PlayerPresenter:IInitializable
    {
        [Inject] ITurnModel _turnModel;

        [Inject] IPlayerTurn _turn;
        [Inject] IPlayerInputView _inputView;
        [Inject] IPlayerTurnEnder _turnEnder;
        [Inject] IPlayerStonePutTryer _stonePutTryer;

        CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void Initialize()
        {
            _turn.TurnEntered.Subscribe(_ => _inputView.Enter()).AddTo(_compositeDisposable);
            _turnEnder.TurnEnded.Subscribe(_ => _turn.Exit()).AddTo(_compositeDisposable);

            _stonePutTryer.Successed.Subscribe(_ => _inputView.Exit()).AddTo(_compositeDisposable);
        }

    }
}