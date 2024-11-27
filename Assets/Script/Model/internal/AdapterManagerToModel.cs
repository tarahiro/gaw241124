using Cysharp.Threading.Tasks;
using gaw241124.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace gaw241124
{
    public class AdapterManagerToModel : IAdapterManagerToModel
    {
        [Inject] ITitleModel _titleModel;
        [Inject] ITurnModel _turnModel;
        [Inject] IGameOverModel _gameOverModel;
        [Inject] IGameClearModel _gameClearModel;

        public void EnterModel()
        {
            _titleModel.Exited.Subscribe(_ => _turnModel.Enter());
            _turnModel.Exited.Subscribe(b =>
            {
                if (!b)
                {
                    _gameOverModel.Enter();
                }
                else
                {
                    _gameClearModel.Enter();
                }
            });
            _gameOverModel.Exited.Subscribe(_ => SceneManager.LoadScene("Main"));
            _gameClearModel.Exited.Subscribe(_ => SceneManager.LoadScene("Main"));

            _titleModel.Enter();
        }
    }
}