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
    public class PlayerStonePutterModel : IPlayerStonePutterModel
    {
        [Inject] IStonePutterModel _stonePutterModel;
        [Inject] ITreasureModel _treasureModel;
        [Inject] IHideModel _hideModel;
        [Inject] IPlayerHoldStoneModel _holdStoneModel;
        [Inject] Func<Const.Side, Vector2Int, StonePositionArgs> _factory;


        public void PutStone(Vector2Int position)
        {
            _stonePutterModel.PutStone(_factory.Invoke(Const.Side.Player,position));

            _holdStoneModel.DeclineStone(1);
            _treasureModel.TryAchieveTreasure(position);
            _hideModel.ClearHide(position);
        }


    }
}