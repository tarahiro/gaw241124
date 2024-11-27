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
    public class PlayerHoldStoneModel : IPlayerHoldStoneModel, IGameOverable
    {
        int _stoneNumber = 0;
        Subject<int> _stoneUpdated = new Subject<int>();
        Subject<int> _stoneAlerted = new Subject<int>();
        Subject<Unit> _gameOvered = new Subject<Unit>();
        public int HoldStoneNumber => _stoneNumber;
        
        public IObservable<int> StoneUpdated => _stoneUpdated;
        public IObservable<int> StoneAlerted => _stoneAlerted;

        public IObservable<Unit> GameOvered => _gameOvered;
        public void AddStone(int addedStoneNumber)
        {
            _stoneNumber += addedStoneNumber;
            _stoneUpdated.OnNext(_stoneNumber);
        }
        public void DeclineStone(int addedStoneNumber)
        {
            _stoneNumber -= addedStoneNumber;
            _stoneUpdated.OnNext(_stoneNumber);
            if (_stoneNumber <= Const.c_alertStoneNumber)
            {
                if (_stoneNumber > 0)
                {
                    _stoneAlerted.OnNext(_stoneNumber);
                }
                else
                {
                    _gameOvered.OnNext(Unit.Default);
                }
            }
        }
    }
}