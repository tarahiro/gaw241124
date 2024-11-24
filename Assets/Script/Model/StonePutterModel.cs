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
    public class StonePutterModel : IStonePutterModel
    {
        [Inject] ITreasureModel _treasureModel;
        [Inject] IHideModel _hideModel;
        [Inject] IGridProvider _gridProvider;
        [Inject] IPlayerHoldStoneModel _holdStoneModel;

        Subject<Vector2Int> _stonePutted = new Subject<Vector2Int>();

        public IObservable<Vector2Int> StonePutted => _stonePutted;



        public void PutStone(Vector2Int position)
        {
            _stonePutted.OnNext(position);
            _holdStoneModel.DeclineStone(1);

            _treasureModel.TryAchieveTreasure(position);
            _hideModel.ClearHide(position);

        }
    }
}