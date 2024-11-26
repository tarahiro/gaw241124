using Cysharp.Threading.Tasks;
using gaw241124;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tarahiro;
using Tarahiro.TGrid;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241124.Model
{
    public class EnemyStonePutTryer : IEnemyStonePutTryer
    {
         IGridProvider _gridProvider;
         IEnemyStonePutter _enemyStonePutter;

        [Inject]
        public EnemyStonePutTryer(IGridProvider gridProvider, IEnemyStonePutter enemyStonePutter)
        {
            _gridProvider = gridProvider;
            _enemyStonePutter = enemyStonePutter;
        }

        public bool TryPutStone(Vector2Int position)
        {
            if (_gridProvider.IsPositionable(position, (int)Const.Positionable.EnemyStone))
            {
                //�b�� 
                var v = GridUtil.GetDirectionList();
                if (v.Any(
                    w => _gridProvider.IsPositionable((w + position), (int)Const.Positionable.EnemyStone)))
                {
                    _enemyStonePutter.PutStone(position);
                    return true;
                }

            }
            return false;

        }
    }
}