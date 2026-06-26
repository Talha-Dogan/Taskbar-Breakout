using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TaskbarBreakout.Core
{
    public class WindowDragger : MonoBehaviour, IWindowDraggable
    {
        [SerializeField] private NativeWindowManager windowManager;

        public bool IsDragging { get; private set; }

        private Vector2Int _prevCursor;

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out CursorPoint point);

        [StructLayout(LayoutKind.Sequential)]
        private struct CursorPoint { public int X, Y; }

        private Vector2Int GetCursorScreenPos()
        {
            GetCursorPos(out var p);
            return new Vector2Int(p.X, p.Y);
        }

        public void BeginDrag()
        {
            IsDragging = true;
            _prevCursor = GetCursorScreenPos();
            Debug.Log($"[WindowDragger] Drag started at {_prevCursor}");
        }

        public void EndDrag()
        {
            IsDragging = false;
        }

        private void Update()
        {
            var mouse = Mouse.current;
            if (mouse == null) return;

            if (mouse.rightButton.wasPressedThisFrame)  BeginDrag();
            if (mouse.rightButton.wasReleasedThisFrame) EndDrag();

            if (!IsDragging) return;

#if !UNITY_EDITOR
            var cursor = GetCursorScreenPos();
            var delta = cursor - _prevCursor;
            if (delta.x != 0 || delta.y != 0)
                windowManager.SetPosition(windowManager.GetPosition() + delta);
            _prevCursor = cursor;
#endif
        }
    }
}
