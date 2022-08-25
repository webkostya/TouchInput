using TouchInput.Source;
using UnityEngine.Events;
using UnityEngine;

namespace TouchInput.Components
{
    [AddComponentMenu("Touch Input/Touch Input Joystick")]
    public class TouchInputJoystick : MonoBehaviour, ITouchInputPointer
    {
        public Transform container;
        public Transform pointer;

        [Space]
        public UnityEvent<Vector2> onTouchEvent;

        private Vector2 _position;
        private Vector2 _touchPoint;
        private Vector2 _originPoint;

        private RectTransform _rect;

        private const float Radius = 90f;

        private void Start()
        {
            _rect = container.GetComponent<RectTransform>();

            _originPoint = _rect.anchoredPosition;
        }

        /// <summary>
        /// Touch Began Event
        /// </summary>
        /// <param name="position"></param>
        public void OnTouchBeganEvent(Vector2 position)
        {
            _touchPoint = position;

            container.position = _touchPoint;
        }

        /// <summary>
        /// Touch Moved Event
        /// </summary>
        /// <param name="position"></param>
        public void OnTouchMovedEvent(Vector2 position)
        {
            _position = Vector2.ClampMagnitude(position - _touchPoint, Radius);

            pointer.localPosition = _position;

            onTouchEvent?.Invoke(_position.normalized);
        }

        /// <summary>
        /// Touch Ended Event
        /// </summary>
        /// <param name="position"></param>
        public void OnTouchEndedEvent(Vector2 position)
        {
            _position = Vector2.zero;

            _rect.anchoredPosition = _originPoint;

            pointer.localPosition = _position;

            onTouchEvent?.Invoke(_position.normalized);
        }
    }
}