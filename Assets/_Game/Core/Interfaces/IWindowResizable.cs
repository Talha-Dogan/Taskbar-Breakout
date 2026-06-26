using UnityEngine;

namespace TaskbarBreakout.Core
{
    public interface IWindowResizable
    {
        void SetSize(Vector2Int size);
        Vector2Int GetSize();
    }
}
