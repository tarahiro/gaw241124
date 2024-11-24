using Cysharp.Threading.Tasks;
using gaw241124;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241124.Model
{
    public class PlayerStoneInitializer:IPlayerStoneInitializer
    {
        [Inject] IPlayerHoldStoneModel _holdStoneModel;
        [Inject] IStonePutterModel _putterModel;

        readonly static Vector2Int _initialStonePosition = new Vector2Int(-3, 1);
        public void InitializeModel()
        {
            _holdStoneModel.AddStone(5);
            _putterModel.PutStone(_initialStonePosition);
        }
    }
}