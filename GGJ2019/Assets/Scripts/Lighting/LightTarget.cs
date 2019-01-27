using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ.Lighting
{
    public class LightTarget : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        [SerializeField]
        private LightTargetState state;
        private Vector3 origin;
        private float lerpTime;

        private void Update()
        {
            switch (state)
            {
                case LightTargetState.Following:
                    transform.position = target.position;
                    break;
                case LightTargetState.Targeting:
                    if (lerpTime < 1)
                    {
                        transform.position = Vector3.Lerp(origin, target.position, lerpTime);
                        lerpTime += Time.deltaTime;
                    }
                    else
                    {
                        transform.position = target.position;
                        state = LightTargetState.Following;
                    }

                    break;
            }
        }

        public void Free()
        {
            state = LightTargetState.Free;
            target = null;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
            state = LightTargetState.Targeting;
            if (!target)
            {
                state = LightTargetState.Free;
                return;
            }
            lerpTime = 0;
            origin = new Vector3(transform.position.x, transform.position.y, target.position.z);
        }

        public enum LightTargetState
        {
            Free,
            Following,
            Targeting
        }
    }
}
