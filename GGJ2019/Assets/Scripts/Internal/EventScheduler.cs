using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace GGJ.Internal
{
    public class EventScheduler : MonoBehaviour
    {
        [SerializeField]
        private GameManager manager;

        [SerializeField]
        UnityEvent startup;

        public GameManager Manager { get => manager; set => manager = value; }

        private void Update() => manager?.OnUpdate();

        private void FixedUpdate() => manager?.OnFixedUpdate();

        public void Start()
        {
            startup.Invoke();
        }
    }
}
