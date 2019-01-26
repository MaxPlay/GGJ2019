using UnityEngine;

namespace GGJ.Level
{
    [ExecuteInEditMode]
    public abstract class TimelineObject : MonoBehaviour
    {
        public WorldTimeline Timeline { get; set; }

        public bool OnStage { get; protected set; }

        public abstract void Setup();

        public void DoUpdate()
        {
            if (Timeline.IsVisible(this) != OnStage)
                if (OnStage)
                    ExitStage();
                else
                    EnterStage();
            UpdateEntity();
        }

        public virtual void EnterStage()
        {
            OnStage = true;
        }

        public virtual void ExitStage()
        {
            OnStage = false;
        }

        public abstract void UpdateEntity();

        public abstract void DrawGizmos(float yMin, float yMax);
        
    }
}