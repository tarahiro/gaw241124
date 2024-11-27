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
using LitMotion;
using LitMotion.Extensions;
using System.Linq;

namespace gaw241124.View
{
    public class TileNotifyItemView : MonoBehaviour
    {
        Vector3 _offset = Vector3.one * .5f;
        Vector3 _moveVector = Vector3.up * 1f;
        float _waitTime = .2f;
        float _moveTime = .5f;

        [SerializeField] SpriteRenderer _spriteRenderer;

        public void Construct(Vector3 worldPosition)
        {
            transform.position = worldPosition + _offset;
        }

        public async UniTask Enter()
        {
            await UniTask.WaitForSeconds(_waitTime);
            
            List<MotionHandle> handleList = new List<MotionHandle>();
            handleList.Add(LMotion.Create(transform.position, transform.position + _moveVector, _moveTime).BindToPosition(transform));

            Color c = _spriteRenderer.color;
            c.a = 1f;
            Color fromColor = c;
            c.a = 0f;
            Color toColor = c;
            handleList.Add(LMotion.Create(fromColor, toColor, _moveTime).BindToColor(_spriteRenderer));

            await UniTask.WaitUntil(() => handleList.All(x => !x.IsActive()));

            Destroy(gameObject);
        }

    }
}