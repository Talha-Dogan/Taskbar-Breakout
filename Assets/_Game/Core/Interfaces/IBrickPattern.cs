using UnityEngine;

namespace TaskbarBreakout.Core
{
    public interface IBrickPattern
    {
        void Build(Rect bounds);
        void Reflow(Rect newBounds);
    }
}
