using Cysharp.Threading.Tasks;
using gaw241124;
using LitMotion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using LitMotion;
using LitMotion.Extensions;

namespace gaw241124.View
{
    public abstract class ITileNotifyItemView : MonoBehaviour
    {
        protected Vector3 _offset = Vector3.one;
        protected Vector3 _moveVector = Vector3.up * 1f;
        protected float _waitTime = .5f;
        protected float _moveTime = .3f;

        [SerializeField] protected SpriteRenderer _spriteRenderer;

        public virtual void Construct(Vector3 worldPosition)
        {
            transform.position = worldPosition + _offset;
        }

        public virtual async UniTask Enter()
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