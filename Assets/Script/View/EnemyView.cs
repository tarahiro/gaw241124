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
    public class EnemyView :MonoBehaviour, IEnemyView
    {
        [SerializeField] Transform _enemyViewRoot;
        
        public void InitializeView()
        {

        }
    }
}