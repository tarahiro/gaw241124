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
        [Inject] ITurnModel _turnModel;

        public void EnterModel()
        {
            _turnModel.Enter();
        }
    }
}