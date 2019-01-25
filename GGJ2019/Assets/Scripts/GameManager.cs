using GGJ.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ
{
    [CreateAssetMenu]
    public class GameManager : ScriptableObject
    {
        public EventScheduler schedulerPrefab;

        public delegate void UpdateEventHandler();

        public event UpdateEventHandler Update;
        public event UpdateEventHandler FixedUpdate;

        public void OnUpdate() => Update?.Invoke();

        public void OnFixedUpdate() => FixedUpdate?.Invoke();
    }
}
