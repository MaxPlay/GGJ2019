using GGJ.Internal;
using GGJ.Level;
using System;
using UnityEngine;

namespace GGJ.Level
{
    public class WorldObject : MonoBehaviour
    {
        private WorldObjectState state;
        private float animationTimer;

        public bool IsVisible { get; private set; }

        [SerializeField]
        private WorldObjectAnimationSettings animationSettings;
        private float upPosition;
        private float downPosition;

        [SerializeField]

        public WorldTimeline Timeline { get; set; }

        public void Setup()
        {
            upPosition = transform.position.y;
            downPosition = transform.position.y - Timeline.Stage.Stage.Area.height;
        }

        public virtual void UpdateEntity()
        {
            IsVisible = Timeline.IsVisible(this);
            switch (state)
            {
                case WorldObjectState.IdleUp:
                    Idle(!IsVisible, WorldObjectState.Down);
                    break;
                case WorldObjectState.Up:
                    Transition(WorldObjectState.IdleUp, animationSettings.UpAnimation);
                    break;
                case WorldObjectState.IdleDown:
                    Idle(IsVisible, WorldObjectState.Up);
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

        private void OnDrawGizmos()
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