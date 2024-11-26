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

namespace gaw241124.View
{
    public class EnemyInitialStoneView :MonoBehaviour, IEnemyInitialStoneView
    {
        [SerializeField] Transform _itemParent;

        Subject<IEnemyInitialStoneItemView> _finded = new Subject<IEnemyInitialStoneItemView>();
        public IObservable<IEnemyInitialStoneItemView> ItemFinded => _finded;

        public void InitializeView()
        {
            var v = _itemParent.GetComponentsInChildren<IEnemyInitialStoneItemView>();

            foreach (var item in v)
            {
                _finded.OnNext(item);
                Destroy(item.transform.gameObject);
            }
        }
    }
}