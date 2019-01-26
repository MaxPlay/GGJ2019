﻿using UnityEngine;

namespace GGJ.Level
{
    [ExecuteInEditMode]
    public abstract class TimelineObject : MonoBehaviour
    {
        public WorldTimeline Timeline { get; set; }

        public bool OnStage { get; protected set; }

        public abstract void Setup();

        public virtual void EnterStage()
        {
            OnStage = true;
        }

        public virtual void ExistStage()
        {
            OnStage = false;
        }

        public abstract void UpdateEntity();

        public abstract void DrawGizmos(float yMin, float yMax);
        
    }
}