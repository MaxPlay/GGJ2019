using GGJ.Character;
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
        public abstract void Action(Snail character);

        public abstract void Interact(Snail character);
    }
}
