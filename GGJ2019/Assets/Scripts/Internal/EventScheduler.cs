using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ.Internal
{
    public class EventScheduler : MonoBehaviour
    {
        [SerializeField]
        private GameManager manager;

        public GameManager Manager { get => manager; set => manager = value; }

        private void Update() => manager?.OnUpdate();

        private void FixedUpdate() => manager?.OnFixedUpdate();
    }
}
