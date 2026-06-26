using UnityEngine;

namespace TaskbarBreakout.Core
{
    public class GameFrame : MonoBehaviour
    {
        [SerializeField] private WindowDragger dragger;
        [SerializeField] private Color frameColor = Color.white;
        [SerializeField] private float thickness = 0.06f;

        private Camera _cam;

        private void Awake() => _cam = Camera.main;
        private void Start()  => RebuildFrame();

        public void RebuildFrame()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);

            float hs = _cam.orthographicSize;
            float w  = hs * 2f * _cam.aspect;
            float h  = hs * 2f;
            float t  = thickness;

            SpawnWall("TopWall",    new Vector2(0,            hs - t * 0.5f),     new Vector2(w, t));
            SpawnWall("BottomWall", new Vector2(0,           -hs + t * 0.5f),     new Vector2(w, t));
            SpawnWall("LeftWall",   new Vector2(-w * 0.5f + t * 0.5f, 0),         new Vector2(t, h));
            SpawnWall("RightWall",  new Vector2( w * 0.5f - t * 0.5f, 0),         new Vector2(t, h));
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
