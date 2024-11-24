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
    public class StoneModel : IStoneModel
    {
        Subject<Vector2Int> _stonePutted = new Subject<Vector2Int>();
        Subject<int> _stoneUpdated = new Subject<int>();
        int _stoneNumber = 0;

        public void InitializeModel()
        {
            AddStone(5);
        }
        public IObservable<Vector2Int> StonePutted => _stonePutted;
        public IObservable<int> StoneUpdated => _stoneUpdated;

        public void PutStone(Vector2Int position)
        {
            if (_stoneNumber > 0)
            {
                _stonePutted.OnNext(position);
                _stoneNumber--;
                _stoneUpdated.OnNext(_stoneNumber);
            }
        }

        public void AddStone(int addedStoneNumber)
        {
            _stoneNumber += addedStoneNumber;
            _stoneUpdated.OnNext(_stoneNumber);
        }
    }
}