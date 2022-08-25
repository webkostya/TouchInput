using UnityEngine.Events;
using TouchInput.Source;
using UnityEngine;

namespace TouchInput.Components
{
    [AddComponentMenu("Touch Input/Touch Input Button")]
    public class TouchInputButton : MonoBehaviour, ITouchInputPointer
    {
        [Space]
        public UnityEvent<bool> onTouchEvent;

        /// <summary>
        /// Touch Began Event
        /// </summary>
        /// <param name="position"></param>
        public void OnTouchBeganEvent(Vector2 position)
        {
            onTouchEvent?.Invoke(true);
        }

        /// <summary>
        /// Touch Moved Event
        /// </summary>
        /// <param name="position"></param>
        public void OnTouchMovedEvent(Vector2 position)
        {
        }

        /// <summary>
        /// Touch Ended Event
        /// </summary>
        /// <param name="position"></param>
        public void OnTouchEndedEvent(Vector2 position)
        {
            onTouchEvent?.Invoke(false);
        }
    }
}