using UnityEngine;
using UnityEngine.InputSystem;

namespace TaskbarBreakout.Core
{
    public class ModeSwitcher : MonoBehaviour
    {
        [SerializeField] private NativeWindowManager windowManager;
        [SerializeField] private CameraAdapter cameraAdapter;
        [SerializeField] private GameFrame gameFrame;

        private IWindowMode _currentMode;
        private TaskbarMode _taskbarMode;
        private FullscreenMode _fullscreenMode;
        private bool _isTaskbar = true;

        private void Awake()
        {
            _taskbarMode = new TaskbarMode(windowManager);
            _fullscreenMode = new FullscreenMode(windowManager);
        }

        private void Start()
        {
            StartCoroutine(ActivateAfterFrame(_taskbarMode));
        }

        private System.Collections.IEnumerator ActivateAfterFrame(IWindowMode mode)
        {
            yield return null;
            Activate(mode);
        }

        private void Update()
        {
            if (Keyboard.current != null && Keyboard.current.f11Key.wasPressedThisFrame)
                Toggle();
        }

        public void Toggle()
        {
            if (_isTaskbar)
                Activate(_fullscreenMode);
            else
                Activate(_taskbarMode);

            _isTaskbar = !_isTaskbar;
        }

        private void Activate(IWindowMode mode)
        {
            var ctx = BuildContext();
            _currentMode?.Deactivate();
            _currentMode = mode;
            _currentMode.Activate(ctx);
            cameraAdapter.AdaptTo(mode.GameBounds);
            gameFrame?.SetBounds(mode.GameBounds);
        }

        private WindowTransitionContext BuildContext() => new WindowTransitionContext();
    }
}
