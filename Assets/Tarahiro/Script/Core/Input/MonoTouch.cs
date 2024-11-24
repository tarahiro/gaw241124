using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UnityEngine;
using VContainer.Unity;
using static Tarahiro.TInput.InputPlatformUtility;
using static UnityEngine.UI.Image;

namespace Tarahiro.TInput
{
    public class MonoTouch : MonoBehaviour
    {
        float _beginTime;
        const int c_storedFrameCount = 100;
        List<Vector2> _prevPositionList = new List<Vector2>();
        List<float> _prevTimeList = new List<float>();
        Transform _clickedGameObject;
        RaycastHit2D _hit2D;

        public TouchConst.TouchState State { get; private set; } = TouchConst.TouchState.None;
        public Vector2 BeginScreenPoint { get; private set; }
        public Vector2 ScreenPointOnThisFrame => _prevPositionList[_prevPositionList.Count - 1];
        public Transform ClickedGameObject => _clickedGameObject;
        public RaycastHit2D Hit2D => _hit2D;
        public float TimeOnThisFrame => _prevTimeList[_prevTimeList.Count - 1];

        public float TimeFromBegin()
        {
            if (State == TouchConst.TouchState.None)
            {
                Log.DebugLog("不正なタイミングで時間が要求されています");
            }

            return Time.time - _beginTime;
        }

        void Update()
        {
            _prevPositionList.Add(TouchPosition());
            _prevTimeList.Add(Time.time);

            Ray ray = Camera.main.ScreenPointToRay(ScreenPointOnThisFrame);
            _hit2D = Physics2D.Raycast(ray.origin, ray.direction);

            if (_prevPositionList.Count > c_storedFrameCount)
            {
                _prevPositionList.RemoveAt(0);
            }
            if (_prevTimeList.Count > c_storedFrameCount)
            {
                _prevTimeList.RemoveAt(0);
            }

            switch (State)
            {
                case TouchConst.TouchState.None:
                    if (IsTouchDown())
                    {
                        ChangeTouchState(TouchConst.TouchState.Begin);
                    }
                    else if (IsTouch())
                    {
                        Log.DebugWarning("不正にタッチが継続しています");
                        ChangeTouchState(TouchConst.TouchState.Begin);
                    }
                    else if (IsTouchUp())
                    {
                        Log.DebugWarning("不正にタッチが終了しました");
                    }
                    break;

                case TouchConst.TouchState.Begin:
                    if (IsTouch())
                    {
                        ChangeTouchState(TouchConst.TouchState.Touching);
                    }
                    else if (IsTouchUp())
                    {
                        ChangeTouchState(TouchConst.TouchState.End);
                    }
                    else
                    {
                        Log.DebugWarning("不正にタッチが開始されました");
                        ChangeTouchState(TouchConst.TouchState.End);
                    }
                    break;

                case TouchConst.TouchState.Touching:
                    if (IsTouch())
                    {
                    }
                    else if (IsTouchUp())
                    {
                        ChangeTouchState(TouchConst.TouchState.End);
                    }
                    else if (IsTouchDown())
                    {
                        Log.DebugWarning("不正にタッチが開始されました");
                        ChangeTouchState(TouchConst.TouchState.Begin);
                    }
                    break;

                case TouchConst.TouchState.End:
                    if (IsTouch())
                    {
                        Log.DebugWarning("不正にタッチが継続しています");
                        ChangeTouchState(TouchConst.TouchState.Begin);
                    }
                    else if (IsTouchUp())
                    {
                        Log.DebugWarning("不正にタッチが終了しました");
                    }
                    else if (IsTouchDown())
                    {
                        ChangeTouchState(TouchConst.TouchState.Begin);
                    }
                    else
                    {
                        BeginScreenPoint = Vector2.zero;
                        ChangeTouchState(TouchConst.TouchState.None);
                    }
                    break;

                default:
                    Log.DebugWarning("不正なステートです");
                    break;
            }
        }
        public Vector2 PrevScreenPoint(int frameCount)
        {
            if (_prevPositionList.Count - 1 < frameCount)
            {
                Log.DebugLog("指定されたフレームのデータが存在しません");
                return _prevPositionList[0];
            }
            else
            {
                return _prevPositionList[_prevPositionList.Count - 1 - frameCount];
            }
        }
        public float PrevTime(int frameCount)
        {
            if (_prevTimeList.Count - 1 < frameCount)
            {
                Log.DebugLog("指定されたフレームのデータが存在しません");
                return _prevTimeList[0];
            }
            else
            {
                return _prevTimeList[_prevTimeList.Count - 1 - frameCount];
            }
        }

        void ChangeTouchState(TouchConst.TouchState state)
        {
            switch (state)
            {


                case TouchConst.TouchState.None:
                    NoneTouch();
                    break;

                case TouchConst.TouchState.Begin:
                    BeginTouch();
                    break;

                case TouchConst.TouchState.Touching:
                    Touching();
                    break;

                case TouchConst.TouchState.End:
                    EndTouch();
                    break;

                default:
                    Log.DebugWarning("不正なステートです");
                    break;
            }

        }

        void NoneTouch()
        {
            State = TouchConst.TouchState.None;
            BeginScreenPoint = Vector2.zero;
            _clickedGameObject = null;
        }

        void BeginTouch()
        {
            State = TouchConst.TouchState.Begin;
            _beginTime = Time.time;
            BeginScreenPoint = ScreenPointOnThisFrame;

            Ray ray = Camera.main.ScreenPointToRay(ScreenPointOnThisFrame);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider)
            {
                _clickedGameObject = hit.transform;
            }
        }

        void Touching()
        {
            State = TouchConst.TouchState.Touching;
            _clickedGameObject = null;

        }

        void EndTouch()
        {
            State = TouchConst.TouchState.End;
            _clickedGameObject = null;
        }
    }
}