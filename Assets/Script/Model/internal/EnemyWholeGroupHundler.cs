using Cysharp.Threading.Tasks;
using gaw241124;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241124.Model
{
    public class EnemyWholeGroupHundler : IEnemyWholeGroupHundler
    {
        [Inject]
        Func<string, IEnemyGroupInstanceProvider> _factory;


        List<IEnemyGroupInstanceProvider> _enemyGroupInstanceProviderList = new List<IEnemyGroupInstanceProvider>();
        List<IEnemyGroupInstanceProvider> _removableList;


        public void InitializeModel()
        {
            //_enemyGroupInstanceProviderList.Add(_factory.Invoke(0));
        }

        public void TryNoticePlayerStone(Vector2Int position)
        {
            _removableList = new List<IEnemyGroupInstanceProvider>();

            foreach (var enemyGroupInstanceProvider in _enemyGroupInstanceProviderList)
            {
                enemyGroupInstanceProvider.GetInstance<IEnemyGroupStoneContainer>().TryNoticePlayerStone(position);
            }

            foreach(var item in _removableList)
            {
                _enemyGroupInstanceProviderList.Remove(item);
            }
        }

        public void RegisterEnemyInitialStone(EnemyInitialStoneArgs args)
        {
            if(!_enemyGroupInstanceProviderList.Any(x => x.GroupId == args.Id))
            {
                Log.DebugLog(args.Id + "のグループ追加");
                var v = _factory.Invoke(args.Id);
                v.Disposed.Subscribe(RemoveEnemyGroup);
                v.GetInstance<IEnemyGroupDefeatable>().Defeated.Subscribe(_ => v.DisposeEnemyGroup());
                _enemyGroupInstanceProviderList.Add(v);
            }

            _enemyGroupInstanceProviderList.First(x => x.GroupId == args.Id).GetInstance<IEnemyGroupStonePutter>().PutStone(args.Position,args.EyesightDirection);
        }

        public List<T> GetInstanceOfAllGroup<T>()
        {
            List<T> list = new List<T>();
            foreach(var item in _enemyGroupInstanceProviderList)
            {
                list.Add(item.GetInstance<T>());
            }
            return list;
        }

        void RemoveEnemyGroup(string groupId)
        {
            _removableList.Add(_enemyGroupInstanceProviderList.First(x => x.GroupId == groupId));
        }
    }
}