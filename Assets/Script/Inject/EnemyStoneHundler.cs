using Cysharp.Threading.Tasks;
using gaw241124;
using gaw241124.Inject;
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
    public class EnemyStoneHundler : IEnemyStoneHundler
    {
        [Inject]
        Func<int, IEnemyStoneContainer> _factory;


        IEnemyStoneContainer _enemyStoneContainer;
        

        public void InitializeModel()
        {
            _enemyStoneContainer = _factory.Invoke(0);
        }

        public void TryNoticePlayerStone(Vector2Int position)
        {
            _enemyStoneContainer.TryNoticePlayerStone(position);
        }

        public void RegisterEnemyInitialStone(EnemyInitialStoneArgs args)
        {
            Log.DebugLog(args.Position);
        }
    }
}