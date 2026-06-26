using UnityEngine;

namespace TaskbarBreakout.Core
{
    public interface IWindowPositionable
    {
        void SetPosition(Vector2Int position);
        Vector2Int GetPosition();
    }
}
