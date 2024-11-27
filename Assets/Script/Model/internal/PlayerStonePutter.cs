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
    public class PlayerStonePutter : IPlayerStonePutter,IPlayerTurnEnder
    {
        [Inject] IStonePutterModel _stonePutterModel;
        [Inject] ITreasureModel _treasureModel;
        [Inject] IHideModel _hideModel;
        [Inject] IPlayerHoldStoneModel _holdStoneModel;
        [Inject] IEnemyWholeGroupHundler _enemyModel;
        [Inject] Func<Const.Side, Vector2Int, StonePositionArgs> _factory;

        Subject<Unit> _turnEnded { get; } = new Subject<Unit>();

        public IObservable<Unit> TurnEnded => _turnEnded;


        public void PutStone(Vector2Int position)
        {
            Log.DebugLog("PlayerPutStone");

            _stonePutterModel.PutStone(_factory.Invoke(Const.Side.Player,position));

            _treasureModel.TryAchieveTreasure(position);
            _holdStoneModel.DeclineStone(1);
            _hideModel.ClearHide(position);
            _enemyModel.TryNoticePlayerStone(position);
            _turnEnded.OnNext(Unit.Default);
        }


    }
}