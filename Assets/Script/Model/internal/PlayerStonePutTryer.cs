using Cysharp.Threading.Tasks;
using gaw241124;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using Tarahiro.TGrid;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241124.Model
{
    public class PlayerStonePutTryer : IPlayerStonePutTryer
    {
        [Inject] IGridProvider _gridProvider;
        [Inject] IPlayerHoldStoneModel _holdStoneModel;
        [Inject] IPlayerStonePutterModel _putterModel;
        public void TryPutStone(Vector2Int position)
        {
            if (_gridProvider.IsPositionable(position, (int)Const.Positionable.Stone))
            {
                if (_holdStoneModel.HoldStoneNumber > 0)
                {
                    _putterModel.PutStone(position);
                }
            }
        }
    }
}