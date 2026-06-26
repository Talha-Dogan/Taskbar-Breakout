using UnityEngine;

namespace TaskbarBreakout.Core
{
    public interface IInputStrategy
    {
        bool IsActivated { get; }
        bool IsDragging { get; }
        float DragDeltaX { get; }
    }
}
