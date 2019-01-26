using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ
{
    public static class Utility
    {
        public static Vector3 VectorLerp(Vector3 a, Vector3 b, float t)
        {
            if(t >= 1)
            {
                return a;
            }

            else if(t <= 0)
            {
                return b;
            }

            a.x = a.x * t + b.x * (1 - t);
            a.y = a.y * t + b.y * (1 - t);
            a.z = a.z * t + b.z * (1 - t);

            return a;
        }
    }
}
