using UnityEngine;
using Kirurobo;

namespace TaskbarBreakout.Core
{
    public class NativeWindowManager : MonoBehaviour,
        IWindowPositionable, IWindowResizable,
        IWindowStyleable, IWindowTopmost, IWindowHitTestable
    {
        [SerializeField] private UniWindowController uwc;

        private void Awake()
        {
            if (uwc == null)
                uwc = FindFirstObjectByType<UniWindowController>();
        }

        public void SnapToTaskbar(TaskbarInfo info)
        {
            SetPosition(new Vector2Int(info.Bounds.x, info.Bounds.y));
            SetSize(new Vector2Int(info.Bounds.width, info.Bounds.height));
        }

        public void SetPosition(Vector2Int position)
        {
            if (uwc == null) return;
            int screenH = Screen.currentResolution.height;
            uwc.windowPosition = new Vector2(position.x, screenH - position.y - GetSize().y);
        }

        public Vector2Int GetPosition()
        {
            if (uwc == null) return Vector2Int.zero;
            int screenH = Screen.currentResolution.height;
            var p = uwc.windowPosition;
            return new Vector2Int((int)p.x, screenH - (int)p.y - GetSize().y);
        }

        public void SetSize(Vector2Int size)
        {
            if (uwc == null) return;
            uwc.windowSize = new Vector2(size.x, size.y);
        }

        public Vector2Int GetSize()
        {
            if (uwc == null) return new Vector2Int(Screen.width, Screen.height);
            var s = uwc.windowSize;
            return new Vector2Int((int)s.x, (int)s.y);
        }

        public void SetTransparent(bool on)
        {
            if (uwc == null) return;
            uwc.isTransparent = on;
        }

        public void SetBorderless(bool on) { }

        public void SetAlwaysOnTop(bool on)
        {
            if (uwc == null) return;
            uwc.isTopmost = on;
        }

        public void SetClickThrough(bool on)
        {
            if (uwc == null) return;
            uwc.isClickThrough = on;
        }

        public void EnableRaycastHitTest()
        {
#if UNITY_EDITOR
            return;
#else
            if (uwc == null) return;
            uwc.isHitTestEnabled = true;
            uwc.hitTestType = UniWindowController.HitTestType.Raycast;
#endif
        }
    }
}
