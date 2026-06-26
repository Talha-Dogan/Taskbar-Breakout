using UnityEngine;

namespace TaskbarBreakout.Core
{
    public class TaskbarMode : IWindowMode
    {
        private readonly NativeWindowManager _window;
        private TaskbarInfo _taskbarInfo;

        public Rect GameBounds { get; private set; }

        public TaskbarMode(NativeWindowManager window)
        {
            _window = window;
        }

        public void Activate(WindowTransitionContext ctx)
        {
            _taskbarInfo = TaskbarDetector.Detect();

            _window.SetTransparent(true);
            _window.SetAlwaysOnTop(true);
            _window.EnableRaycastHitTest();

            _window.SetSize(new Vector2Int(832, 200));
            _window.SetPosition(new Vector2Int(200, 400));

            GameBounds = new Rect(0, 0, 832, 200);
        }

        public void Deactivate()
        {
            _window.SetAlwaysOnTop(false);
        }
    }
}
