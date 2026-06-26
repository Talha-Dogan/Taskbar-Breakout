using UnityEngine;

namespace TaskbarBreakout.Core
{
    public class FullscreenMode : IWindowMode
    {
        private readonly NativeWindowManager _window;

        public Rect GameBounds { get; private set; }

        public FullscreenMode(NativeWindowManager window)
        {
            _window = window;
        }

        public void Activate(WindowTransitionContext ctx)
        {
            _window.SetAlwaysOnTop(false);
            _window.SetTransparent(false);
            Screen.fullScreen = true;

            GameBounds = new Rect(0, 0, Screen.width, Screen.height);
        }

        public void Deactivate()
        {
            Screen.fullScreen = false;
        }
    }
}
