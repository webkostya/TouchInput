using UnityEngine.InputSystem.Controls;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TouchInput.Source;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace TouchInput.Components
{
    [AddComponentMenu("Touch Input/Touch Input System")]
    public class TouchInputSystem : MonoBehaviour
    {
        private PointerEventData _pointer;
        private GraphicRaycaster _raycast;

        private Dictionary<int, TouchInputPointer> _touches;

        private void Awake()
        {
            _touches = new Dictionary<int, TouchInputPointer>();
            _pointer = new PointerEventData(EventSystem.current);
            _raycast = GetComponentInParent<GraphicRaycaster>();
        }

        private void LateUpdate()
        {
            if (Touchscreen.current is null)
            {
                return;
            }

            foreach (var touch in Touchscreen.current.touches)
            {
                switch (touch.phase.ReadValue())
                {
                    case TouchPhase.Began:
                        TouchPhaseBegan(touch);
                        break;
                    case TouchPhase.Moved:
                        TouchPhaseMoved(touch);
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        TouchPhaseEnded(touch);
                        break;
                    case TouchPhase.Stationary:
                    case TouchPhase.None:
                    default: break;
                }
            }
        }

        /// <summary>
        /// Touch Began Action
        /// </summary>
        /// <param name="touch"></param>
        private void TouchPhaseBegan(TouchControl touch)
        {
            var touchId = touch.touchId.ReadValue();

            if (_touches.ContainsKey(touchId))
            {
                return;
            }

            _touches.Add(touchId, new TouchInputPointer(touch, this));
        }

        /// <summary>
        /// Touch Moved Action
        /// </summary>
        /// <param name="touch"></param>
        private void TouchPhaseMoved(TouchControl touch)
        {
            var touchId = touch.touchId.ReadValue();

            if (_touches.ContainsKey(touchId))
            {
                _touches[touchId].OnUpdate();
            }
        }

        /// <summary>
        /// Touch Ended Action
        /// </summary>
        /// <param name="touch"></param>
        private void TouchPhaseEnded(TouchControl touch)
        {
            var touchId = touch.touchId.ReadValue();

            if (!_touches.ContainsKey(touchId))
            {
                return;
            }

            _touches[touchId].OnDestroy();
            _touches.Remove(touchId);
        }

        /// <summary>
        /// Has Element From Canvas Raycast
        /// </summary>
        /// <param name="position"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool HasTouchElement(Vector2 position, out GameObject node)
        {
            _pointer.position = position;

            var output = new List<RaycastResult>();

            _raycast.Raycast(_pointer, output);

            return node = output.FirstOrDefault().gameObject;
        }
    }
}