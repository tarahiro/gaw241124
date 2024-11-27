using Cysharp.Threading.Tasks;
using gaw241124.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;


namespace gaw241124.Presenter
{
    public class GameOverPresenter : MonoBehaviour
    {
        [Inject] IGameOverModel _model;

        [SerializeField] GameObject _root;
        [SerializeField] Button _button;

        void Start()
        {
            Log.DebugAssert(_button != null);
            _model.Entered.Subscribe(_ =>
            {
                _button.interactable = true;
                _root.SetActive(true);
            }
            );
            _button.onClick.AddListener(() =>
            {
                _root.SetActive(false);
                _model.Exit();
            });

            _root.SetActive(false);
        }

    }
}