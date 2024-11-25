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
    public class EnemyStoneChain : IEnemyStoneChain
    {
        Subject<List<Vector2Int>> _aroundFilled = new Subject<List<Vector2Int>>();

        public List<Vector2Int> StonePositionList { get; set; } = new List<Vector2Int>();
        public List<Vector2Int> EmptyAroundList { get; set; } = new List<Vector2Int>();
        public IObservable<List<Vector2Int>> AroundFilled => _aroundFilled;

        public EnemyStoneChain(Vector2Int position)
        {
            StonePositionList.Add(position);
        }

        public void Initialize()
        {
            Log.DebugLog("Initialize");

            //üˆÍ‚ÌÎ‚ğæ“¾

            //EmptyAroundList‚ğæ“¾
        }

        public bool IsKilledIfStonePutted(Vector2Int position)
        {
            Log.DebugAssert("–¢À‘•");
            return false;
        }

        public void GetNoticeStoneOnAround(Vector2Int position)
        {
            Log.DebugAssert("–¢À‘•");
        }
    }
}