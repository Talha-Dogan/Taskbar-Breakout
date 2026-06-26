using UnityEngine;

namespace TaskbarBreakout.Core
{
    public class SettingsController : MonoBehaviour
    {
        [SerializeField] private NativeWindowManager windowManager;
        [SerializeField] private SettingsPanel       settingsPanel;

        private static readonly Vector2Int GameSize     = new(832, 200);
        private static readonly Vector2Int ExpandedSize = new(832, 400);

        public bool IsOpen { get; private set; }
        private bool       _openedUpward;
        private Vector2Int _savedPos;

        public void Toggle()
        {
            if (IsOpen) Close();
            else        Open();
        }

        private void Open()
        {
            IsOpen    = true;
            _savedPos = windowManager.GetPosition();

            int screenH    = Screen.currentResolution.height;
            int winCenterY = _savedPos.y + GameSize.y / 2;
            _openedUpward  = winCenterY > screenH / 2;

            // Yukarı: sadece SetSize — alt kenar sabit, üst yukarı büyür
            // Aşağı:  SetSize sonra SetPosition — üst kenar sabit, alt aşağı büyür
            windowManager.SetSize(ExpandedSize);
            if (!_openedUpward)
                windowManager.SetPosition(_savedPos);

            Camera.main.rect = _openedUpward
                ? new Rect(0f, 0f, 1f, 0.5f)
                : new Rect(0f, 0.5f, 1f, 0.5f);

            settingsPanel.SetDirection(_openedUpward);
            settingsPanel.Show();
        }

        private void Close()
        {
            IsOpen = false;
            settingsPanel.Hide();
            Camera.main.rect = new Rect(0f, 0f, 1f, 1f);

            windowManager.SetSize(GameSize);
            if (!_openedUpward)
                windowManager.SetPosition(_savedPos);
        }
    }
}
