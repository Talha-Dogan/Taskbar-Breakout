using UnityEngine;
using UnityEngine.InputSystem;

namespace TaskbarBreakout.Core
{
    [RequireComponent(typeof(Collider2D))]
    public class WallDragInput : MonoBehaviour
    {
        private WindowDragger _dragger;
        private Collider2D    _col;
        private bool          _pressing;

        public void Init(WindowDragger dragger)
        {
            _dragger = dragger;
            _col     = GetComponent<Collider2D>();
        }

        private void Update()
        {
            var mouse = Mouse.current;
            if (mouse == null || _dragger == null) return;

            if (mouse.leftButton.wasPressedThisFrame)
            {
                var screenPos = mouse.position.ReadValue();
                var worldPos  = (Vector2)Camera.main.ScreenToWorldPoint(screenPos);

                if (_col.OverlapPoint(worldPos))
                {
                    _pressing = true;
                    _dragger.BeginDrag();
                }
            }

            if (_pressing && mouse.leftButton.wasReleasedThisFrame)
            {
                _pressing = false;
                _dragger.EndDrag();
            }
        }
    }
}
