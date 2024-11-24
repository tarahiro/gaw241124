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
    public class StoneModel : IStoneModel
    {
        [Inject] ITreasureModel _treasureModel;
        [Inject] IHideModel _hideModel;
        [Inject] IGridProvider _gridProvider;

        Subject<Vector2Int> _stonePutted = new Subject<Vector2Int>();
        Subject<int> _stoneUpdated = new Subject<int>();
        int _stoneNumber = 0;

        public void InitializeModel()
        {
            AddStone(5);
            ForcePutStone(Vector2Int.zero);
        }
        public IObservable<Vector2Int> StonePutted => _stonePutted;
        public IObservable<int> StoneUpdated => _stoneUpdated;

        public void TryPutStone(Vector2Int position)
        {
            if (_gridProvider.IsPositionable(position, (int)Const.Positionable.Stone)){

                if (_stoneNumber > 0)
                {
                    PutStone(position);
                }
            }
        }

        public void AddStone(int addedStoneNumber)
        {
            _stoneNumber += addedStoneNumber;
            _stoneUpdated.OnNext(_stoneNumber);
        }

        void ForcePutStone(Vector2Int position)
        {
            PutStone(position);
        }

        void PutStone(Vector2Int position)
        {
            _stonePutted.OnNext(position);
            _stoneNumber--;
            _stoneUpdated.OnNext(_stoneNumber);

            _treasureModel.TryAchieveTreasure(position);
            _hideModel.ClearHide(position);

        }
    }
}