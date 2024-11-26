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
        

        public void InitializeModel()
        {
            //_enemyGroupInstanceProviderList.Add(_factory.Invoke(0));
        }

        public void TryNoticePlayerStone(Vector2Int position)
        {
            foreach (var enemyGroupInstanceProvider in _enemyGroupInstanceProviderList)
            {
                enemyGroupInstanceProvider.GetInstance<IEnemyGroupStoneContainer>().TryNoticePlayerStone(position);
            }
        }

        public void RegisterEnemyInitialStone(EnemyInitialStoneArgs args)
        {
            if(!_enemyGroupInstanceProviderList.Any(x => x.GroupId == args.Id))
            {
                Log.DebugLog(args.Id + "‚ÌƒOƒ‹[ƒv’Ç‰Á");
                _enemyGroupInstanceProviderList.Add(_factory.Invoke(args.Id));
            }

            _enemyGroupInstanceProviderList.First(x => x.GroupId == args.Id).GetInstance<IEnemyGroupStonePutter>().PutStone(args.Position);
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
    }
}