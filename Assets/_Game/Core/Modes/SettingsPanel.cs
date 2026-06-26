using UnityEngine;

namespace TaskbarBreakout.Core
{
    public class SettingsPanel : MonoBehaviour, ISettingsView
    {
        [SerializeField] private RectTransform panelRect;

        public bool IsVisible { get; private set; }

        private void Awake() => panelRect.gameObject.SetActive(false);

        public void SetDirection(bool openedUpward)
        {
            panelRect.anchorMin = openedUpward ? new Vector2(0f, 0.5f) : new Vector2(0f, 0f);
            panelRect.anchorMax = openedUpward ? new Vector2(1f, 1f)   : new Vector2(1f, 0.5f);
            panelRect.offsetMin = Vector2.zero;
            panelRect.offsetMax = Vector2.zero;
        }

        public void Show() { IsVisible = true;  panelRect.gameObject.SetActive(true); }
        public void Hide() { IsVisible = false; panelRect.gameObject.SetActive(false); }
    }
}
