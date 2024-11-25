using Cysharp.Threading.Tasks;
using gaw241124;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using VContainer;
using VContainer.Unity;

namespace gaw241124.View
{
    public class StoneNumberUiView: MonoBehaviour,IStoneNumberUiView
    {
        [SerializeField] TextMeshProUGUI _stoneNumber;

        const string c_prefix = "Å~";

        public void UpdateStoneNumber(int StoneNumber)
        {
            _stoneNumber.text = c_prefix + StoneNumber.ToString();
        }
    }
}