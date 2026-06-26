using UnityEngine;

namespace TaskbarBreakout.Core
{
    public interface IWindowMode
    {
        Rect GameBounds { get; }
        void Activate(WindowTransitionContext ctx);
        void Deactivate();
    }
}
