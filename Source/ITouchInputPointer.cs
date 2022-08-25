using UnityEngine;

namespace TouchInput.Source
{
    internal interface ITouchInputPointer
    {
        void OnTouchBeganEvent(Vector2 position);
        void OnTouchMovedEvent(Vector2 position);
        void OnTouchEndedEvent(Vector2 position);
    }
}