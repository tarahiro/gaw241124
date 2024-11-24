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

namespace gaw241124.View
{
    public class TreasureView : MonoBehaviour,ITreasureView
    {
        [SerializeField] Transform _itemParent;

        ITreasureItemView[] _itemArray;
        Subject<ITreasureItemView> _itemRegistered = new Subject<ITreasureItemView>();
        public IObservable<ITreasureItemView> ItemRegistered => _itemRegistered;

        public void DestroyTreasure(int index)
        {
            _itemArray.First(x => x.Index == index).Destroy();
        }

        public void InitializeView()
        {
            _itemArray = _itemParent.GetComponentsInChildren<ITreasureItemView>();

            int index = 0;
            foreach (var item in _itemArray)
            {
                item.Index = index;
                index++;

                _itemRegistered.OnNext(item);
            }
        }
    }
}