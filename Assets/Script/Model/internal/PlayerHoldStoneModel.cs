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
    public class PlayerHoldStoneModel : IPlayerHoldStoneModel
    {
        int _stoneNumber = 0;
        Subject<int> _stoneUpdated = new Subject<int>();
        public int HoldStoneNumber => _stoneNumber;
        public IObservable<int> StoneUpdated => _stoneUpdated;
        public void AddStone(int addedStoneNumber)
        {
            _stoneNumber += addedStoneNumber;
            _stoneUpdated.OnNext(_stoneNumber);
        }
        public void DeclineStone(int addedStoneNumber)
        {
            _stoneNumber -= addedStoneNumber;
            _stoneUpdated.OnNext(_stoneNumber);
        }
    }
}