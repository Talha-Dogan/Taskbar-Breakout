using UnityEngine;

namespace TaskbarBreakout.Core
{
    public class WindowTransitionContext
    {
        public int Score;
        public int Lives;
        public int Level;
        public Vector2 BallPositionNormalized;
        public Vector2 BallVelocityDirection;
        public float PaddlePositionNormalized;
    }
}
