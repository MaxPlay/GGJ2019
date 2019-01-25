using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ.Powerups
{
    abstract class House : MonoBehaviour
    {
        public abstract void Action();

        public abstract void Interact();
    }

    public enum HouseType
    {
        None,
        DShell,
        Heavy,
        Balloon,
        Lantern,
        Castle
    }
}
