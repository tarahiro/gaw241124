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
    public class StoneAlertViewItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _prefixText;
        [SerializeField] Image _image;
        [SerializeField] TextMeshProUGUI _suffixText;
        const string c_prefix = "Ç†Ç∆";
        const string c_suffix = "å¬ÅI";
        const float c_waitTime = 2f;

        public void Construct(int number)
        {
            _prefixText.text = c_prefix;
            _suffixText.text = number.ToString() + c_suffix;

        }
        public async UniTask Enter()
        {
            await UniTask.WaitForSeconds(c_waitTime);
            Destroy(gameObject);
        }
    }
}