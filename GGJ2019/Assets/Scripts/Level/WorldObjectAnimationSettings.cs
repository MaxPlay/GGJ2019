using GGJ.Internal;
using UnityEngine;

namespace GGJ.Level
{
    [CreateAssetMenu(menuName = Constants.SCENE_OBJECTS_STRING + "World Object Animation Settings")]
    public class WorldObjectAnimationSettings : ScriptableObject
    {
        public float Speed;
        public AnimationCurve UpAnimation;
        public AnimationCurve DownAnimation;
    }
}