using UnityEngine;
using UnityEngine.InputSystem;

namespace TaskbarBreakout.Core
{
    [RequireComponent(typeof(Collider2D))]
    public class SettingsButton : MonoBehaviour
    {
        [SerializeField] private SettingsController settings;

        private Collider2D _col;

        private void Awake() => _col = GetComponent<Collider2D>();

        private void Update()
        {
            var mouse = Mouse.current;
            if (mouse == null || !mouse.leftButton.wasPressedThisFrame) return;

            var worldPos = (Vector2)Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
            if (_col.OverlapPoint(worldPos))
                settings.Toggle();
        }
    }
}
