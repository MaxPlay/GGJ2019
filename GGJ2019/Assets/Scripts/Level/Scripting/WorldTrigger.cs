using GGJ.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace GGJ.Level.Scripting
{
    public class WorldTrigger : TimelineObject
    {
        public UnityEvent TriggerEnter;
        public UnityEvent TriggerLeave;

        [SerializeField]
        private bool once;

        [SerializeField]
        private bool onExit;

        [SerializeField]
        private bool onEntry;

        private bool wasTriggered;

        public override void EnterStage()
        {
            if (onEntry)
                FireTrigger(TriggerEnter);
            base.EnterStage();
        }

        private void FireTrigger(UnityEvent trigger)
        {
            if (once)
            {
                if (!wasTriggered)
                    trigger.Invoke();
                wasTriggered = true;
            }
            else
                trigger.Invoke();
        }

        public override void ExitStage()
        {
            if (onExit)
                FireTrigger(TriggerLeave);

            base.ExitStage();
        }

        public override void Setup()
        {

        }

        public override void UpdateEntity()
        {

        }

        public override void DrawGizmos(float yMin, float yMax)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(new Vector2(transform.position.x, yMin), new Vector2(transform.position.x, yMax));
        }
    }
}
