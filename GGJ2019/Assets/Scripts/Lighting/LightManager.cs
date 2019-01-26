using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ.Lighting
{
    [Serializable]
    public class LightManager : MonoBehaviour
    {
        [SerializeField]
        private StageLight[] stageLights;

        [SerializeField]
        private FollowerLights followers = new FollowerLights();

        public FollowerLights Followers { get { return followers; } }

        [SerializeField]
        private StageLight[] pauseLights;

        public void SetColor(Color color, params StageLight[] lights)
        {
            for (int i = 0; i < lights.Length; i++)
                lights[i].FadeTo(color);
        }

        public void SetIntensity(float intensity, params StageLight[] lights)
        {
            for (int i = 0; i < lights.Length; i++)
                lights[i].FadeTo(intensity);
        }

        public void TurnOff(params StageLight[] lights)
        {
            for (int i = 0; i < lights.Length; i++)
                lights[i].FadeTo(0);
        }

        public void TurnOn(params StageLight[] lights)
        {
            for (int i = 0; i < lights.Length; i++)
                lights[i].FadeTo(1);
        }

        public void PauseOn()
        {
            TurnOn(pauseLights);
            TurnOff(stageLights);
        }

        public void PauseOff()
        {
            TurnOff(pauseLights);
            TurnOn(stageLights);
        }

        public void FollowFront(Transform target) => Followers.SetTargetFront(target);

        [Serializable]
        public class FollowerLights
        {
            public FollowingStagelight SideLeft;
            public FollowingStagelight SideRight;
            public FollowingStagelight FrontLeft;
            public FollowingStagelight FrontRight;
            public FollowingStagelight TopCenter;
            public FollowingStagelight TopLeft;
            public FollowingStagelight TopRight;

            public void ClearTarget(params FollowingStagelight[] lights) => SetTarget(null, lights);

            public void SetTarget(Transform target, params FollowingStagelight[] lights)
            {
                for (int i = 0; i < lights.Length; i++)
                    lights[i].LookAtTarget = target;
            }

            public void SetTargetFront(Transform target) => SetTarget(target, FrontLeft, FrontRight);

            public void SetTargetTop(Transform target) => SetTarget(target, TopCenter, TopLeft, TopRight);

            public void SetTargetSide(Transform target) => SetTarget(target, SideLeft, SideRight);
        }
    }
}
