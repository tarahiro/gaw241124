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
    public class EnemyStonePutter : IEnemyStonePutter
    {
        [Inject] IStonePutterModel _stonePutterModel;
        [Inject] Func<Const.Side, Vector2Int, StonePositionArgs> _factory;
        [Inject] IEnemyStoneContainer _container;

        public void PutStone(Vector2Int position)
        {
            _stonePutterModel.PutStone(_factory.Invoke(Const.Side.Enemy, position));

            _container.NoticeEnemyStone(position);
        }
    }
}