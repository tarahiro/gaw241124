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
    public class TreasureModel : ITreasureModel
    {
        Dictionary<Vector2Int, TreasureData> _dictionary = new Dictionary<Vector2Int, TreasureData> ();
        Subject<int> _treasureAchieved = new Subject<int> ();
        Subject<int> _addStoneTreasureAchieved = new Subject<int>();
        Subject<Vector2Int> _addStoneTreasureAchievedVec = new Subject<Vector2Int> ();

        public IObservable<int> TreasureAchieved => _treasureAchieved;
        public IObservable<int> AddStoneTreasureAchieved => _addStoneTreasureAchieved;
        public IObservable<Vector2Int> AddStoneTreasureAchievedVec => _addStoneTreasureAchievedVec;

        public void TryAchieveTreasure(Vector2Int position)
        {

            if(_dictionary.TryGetValue (position, out var data))
            {
                switch (data.Id)
                {
                    case "AddStone":
                        _addStoneTreasureAchieved.OnNext(data.Arg);
                        _addStoneTreasureAchievedVec.OnNext(position);
                        break;

                    default:
                        Log.DebugAssert("éwíËÇµÇΩIdÇ™ïsê≥Ç≈Ç∑");
                        break;
                }

                _dictionary.Remove (position);
                _treasureAchieved.OnNext(data.InstanceIndex);
            }
        }

        public void RegisterTreasure(TreasureModelItemArgs args)
        {
            _dictionary.Add(args.Position, new TreasureData(args.InstanceIndex, args.Id,args.arg));
        }

        class TreasureData
        {
            public int InstanceIndex { get; set; }
            public string Id { get; set; }
            public int Arg { get; set; }

            public TreasureData(int instanceIndex, string id, int arg)
            {
                this.InstanceIndex = instanceIndex;
                this.Id = id;
                Arg = arg;
            }
        }
    }
}