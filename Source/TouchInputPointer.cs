using UnityEngine.InputSystem.Controls;
using TouchInput.Components;

namespace TouchInput.Source
{
    internal class TouchInputPointer
    {
        private TouchControl _touch;
        private ITouchInputPointer _pointer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="touch"></param>
        /// <param name="context"></param>
        public TouchInputPointer(TouchControl touch, TouchInputSystem context)
        {
            _touch = touch;

            var position = _touch.position.ReadValue();

            if (!context.HasTouchElement(position, out var node))
            {
                return;
            }

            _pointer = node.GetComponent<ITouchInputPointer>();

            _pointer?.OnTouchBeganEvent(position);
        }

        /// <summary>
        /// Update Event
        /// </summary>
        public void OnUpdate()
        {
            var position = _touch.position.ReadValue();

            _pointer?.OnTouchMovedEvent(position);
        }

        /// <summary>
        /// Destroy Event
        /// </summary>
        public void OnDestroy()
        {
            var position = _touch.position.ReadValue();

            _pointer?.OnTouchEndedEvent(position);

            _pointer = null;
            _touch = null;
        }
    }
}