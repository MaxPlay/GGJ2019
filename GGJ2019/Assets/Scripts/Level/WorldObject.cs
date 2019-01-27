using GGJ.Level;
using System;
using UnityEngine;

namespace GGJ.Level
{
    public class WorldObject : TimelineObject
    {
        private WorldObjectState state;
        private float animationTimer;

        [SerializeField]
        private WorldObjectAnimationSettings animationSettings;
        private float upPosition;
        private float downPosition;

        [SerializeField]
        private bool moveUp;

        public WorldObjectState State { get => state; }

        public override void Setup()
        {
            upPosition = transform.position.y;
            downPosition = transform.position.y - (Timeline.Stage.Stage.Area.height * (moveUp ? -1 : 1));
        }

        public override void UpdateEntity()
        {
            switch (State)
            {
                case WorldObjectState.IdleUp:
                    Idle(!OnStage, WorldObjectState.Down);
                    break;
                case WorldObjectState.Up:
                    Transition(WorldObjectState.IdleUp, animationSettings.UpAnimation);
                    break;
                case WorldObjectState.IdleDown:
                    Idle(OnStage, WorldObjectState.Up);
                    break;
                case WorldObjectState.Down:
                    Transition(WorldObjectState.IdleDown, animationSettings.DownAnimation);
                    break;
            }
        }

        private void Transition(WorldObjectState target, AnimationCurve animation)
        {
            if (animationTimer >= 1)
                state = target;
            animationTimer += Time.deltaTime * animationSettings.Speed;
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(downPosition, upPosition, animation.Evaluate(animationTimer)), transform.position.z);
        }

        private void Idle(bool trigger, WorldObjectState targetState)
        {
            if (trigger)
            {
                state = targetState;
                animationTimer = 0;
            }
        }

        public override void DrawGizmos(float yMin, float yMax)
        {
            Gizmos.color = Timeline ? Timeline.IsVisible(this) ? Color.white : Color.gray : Color.gray;
            Gizmos.DrawSphere(transform.position, 0.1f);
        }
    }

    public enum WorldObjectState
    {
        IdleUp,
        Up,
        IdleDown,
        Down
    }
}