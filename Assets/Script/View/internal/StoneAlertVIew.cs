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
    public class StoneAlertVIew : MonoBehaviour, IStoneAlertView
    {
        [SerializeField] StoneAlertViewItem _prefab;
        [SerializeField] Transform parent;

        public void ShowAlert(int stoneNumber)
        {
            var v = Instantiate(_prefab, parent);
            v.Construct(stoneNumber);
            v.Enter().Forget();
        }
    }
}