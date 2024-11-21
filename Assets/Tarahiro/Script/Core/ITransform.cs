﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;

namespace Tarahiro
{

    /// <summary>
    /// interfaceからTransformの操作をできるようにする、interface。
    /// </summary>
    public interface ITransform
    { 
        Transform transform { get; }
    }
}
