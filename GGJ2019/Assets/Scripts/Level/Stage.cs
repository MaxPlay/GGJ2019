using GGJ.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ.Level
{
    [CreateAssetMenu(menuName = Constants.SCENE_OBJECTS_STRING + "Stage")]
    public class Stage : ScriptableObject
    {
        [SerializeField]
        private Rect area;

        public Rect Area { get =>area; set => area = value; }
    }
}
