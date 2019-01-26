using GGJ.Internal;
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

        public override void Setup()
        {
            upPosition = transform.position.y;
            downPosition = transform.position.y - (Timeline.Stage.Stage.Area.height * (moveUp ? -1 : 1));
        }

        public override void UpdateEntity()
        {
            switch (state)
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
            transform.position = new Vector2(transform.position.x, Mathf.Lerp(downPosition, upPosition, animation.Evaluate(animationTimer)));
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
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }

    [CreateAssetMenu(menuName = Constants.SCENE_OBJECTS_STRING + "World Object Animation Settings")]
    public class WorldObjectAnimationSettings : ScriptableObject
    {
        public float Speed;
        public AnimationCurve UpAnimation;
        public AnimationCurve DownAnimation;
    }

    public enum WorldObjectState
    {
        IdleUp,
        Up,
        IdleDown,
        Down
    }
}