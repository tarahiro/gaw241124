using Cysharp.Threading.Tasks;
using gaw241124;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using Tarahiro.TGrid;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241124.View
{
    public class PercieveNotifyView : TileNotifyView, IPercieveNotifyView
    {

        public override void InitializeView()
        {
            base.InitializeView();
            _prefabName = "Prefab/PercieveNotifyItemView";
        }
    }
}