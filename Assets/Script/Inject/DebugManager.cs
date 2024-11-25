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

namespace gaw241124.Inject
{
#if ENABLE_DEBUG
    public class DebugManager : ITickable
    {
        [Inject] IPlayerHoldStoneModel _playerHoldStoneModel;

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _playerHoldStoneModel.AddStone(10);
            }
        }
    }
#endif
}