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
    public class EnemyGroupStonePutter : IEnemyGroupStonePutter
    {
       IStonePutterModel _stonePutterModel;
        IEnemyGroupStoneContainer _container;
        Func<Const.Side, Vector2Int, StonePositionArgs> _factory;

        [Inject]
        public EnemyGroupStonePutter(IStonePutterModel stonePutterModel,
IEnemyGroupStoneContainer container, Func<Const.Side, Vector2Int, StonePositionArgs> factory)
        {
            _stonePutterModel = stonePutterModel;
            _container = container;
            _factory = factory;
        }


        public void PutStone(Vector2Int position)
        {
            _stonePutterModel.PutStone(_factory.Invoke(Const.Side.Enemy, position));

            _container.NoticeEnemyStone(position);
        }
    }
}