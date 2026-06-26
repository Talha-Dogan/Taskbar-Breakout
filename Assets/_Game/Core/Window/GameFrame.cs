using System.Collections;
using UnityEngine;

namespace TaskbarBreakout.Core
{
    public class GameFrame : MonoBehaviour
    {
        [SerializeField] private WindowDragger dragger;
        [SerializeField] private Color frameColor = Color.white;
        [SerializeField] private float thickness = 0.06f;

        private const float PPU = 100f;
        private Camera _cam;
        private Rect _bounds;

        private void Awake() => _cam = Camera.main;

        public void SetBounds(Rect bounds)
        {
            _bounds = bounds;
            StartCoroutine(BuildWhenReady());
        }

        // Kamera tam adapte olana kadar bekle, sonra gerçek frustum'a göre çiz
        private IEnumerator BuildWhenReady()
        {
            float target = _bounds.height * 0.5f / PPU;
            while (Mathf.Abs(_cam.orthographicSize - target) > 0.005f)
                yield return null;
            yield return null; // bir frame daha: aspect ratio güncellensin
            RebuildFrame();
        }

        private void RebuildFrame()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);

            float hs = _cam.orthographicSize;
            float hw = hs * _cam.aspect;
            float t  = thickness;

            SpawnWall("TopWall",    new Vector2(0,          hs - t * 0.5f), new Vector2(hw * 2f, t));
            SpawnWall("BottomWall", new Vector2(0,         -hs + t * 0.5f), new Vector2(hw * 2f, t));
            SpawnWall("LeftWall",   new Vector2(-hw + t * 0.5f, 0),         new Vector2(t, hs * 2f));
            SpawnWall("RightWall",  new Vector2( hw - t * 0.5f, 0),         new Vector2(t, hs * 2f));
        }

        private void SpawnWall(string wallName, Vector2 localPos, Vector2 size)
        {
            var go = new GameObject(wallName);
            go.transform.SetParent(transform);
            go.transform.localPosition = localPos;
            go.transform.localScale    = new Vector3(size.x, size.y, 1f);

            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = WhiteSprite();
            sr.color  = frameColor;

            var col = go.AddComponent<BoxCollider2D>();
            col.size = Vector2.one;

            var input = go.AddComponent<WallDragInput>();
            input.Init(dragger);
        }

        private static Sprite WhiteSprite()
        {
            var tex = new Texture2D(1, 1) { filterMode = FilterMode.Point };
            tex.SetPixel(0, 0, Color.white);
            tex.Apply();
            return Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1f);
        }
    }
}
