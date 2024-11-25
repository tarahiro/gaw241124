using Cysharp.Threading.Tasks;
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
    public class GameManager : IPostStartable
    {
        [Inject] IAdapterManagerToModel _adapter;

        public void PostStart()
        {
            _adapter.EnterModel();
        }
    }
}