using Cysharp.Threading.Tasks;
using gaw241124.Model;
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
    public class AdapterManagerToModel : IAdapterManagerToModel
    {
        [Inject] ITitleModel _titleModel;
        [Inject] ITurnModel _turnModel;

        public void EnterModel()
        {
            _titleModel.Exited.Subscribe(_ => _turnModel.Enter());

            _titleModel.Enter();
        }
    }
}