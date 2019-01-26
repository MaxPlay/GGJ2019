using GGJ.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ.Powerups
{
    class House : MonoBehaviour
    {
        public HouseType houseType;

        public enum HouseType
        {
            None,
            Default,
            Heavy,
            Fly,
            Light,
            Castle
        }
    }
}
