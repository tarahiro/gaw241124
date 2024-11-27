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
    public class GameEndPresenter : IInitializable
    {
        [Inject] IGameOverable _gameOverable;
        [Inject] IGameClearable _gameClearable;
        [Inject] ITurnModel _turnModel;
        [Inject] ICameraFollowObjectView _cameraFollowObjectView;

        CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void Initialize()
        {
            Log.DebugLogComment("GameEndPresenter‰Šú‰»");

            _gameOverable.GameOvered.Subscribe(_ => _turnModel.Exit(false)).AddTo(_compositeDisposable);
            _gameClearable.GameCleared.Subscribe(_ => _turnModel.Exit(true)).AddTo(_compositeDisposable);
            _turnModel.Exited.Subscribe(_ => _cameraFollowObjectView.Exit()).AddTo(_compositeDisposable);
            _turnModel.Entered.Subscribe(_ => _cameraFollowObjectView.Enter()).AddTo(_compositeDisposable);
        }
    }
}