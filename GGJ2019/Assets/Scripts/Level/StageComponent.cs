﻿using GGJ.Lighting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ.Level
{
    public class StageComponent : MonoBehaviour
    {
        [SerializeField]
        private Stage stage;

        public Stage Stage { get => stage; }

        [SerializeField]
        private LightManager lights;

        public LightManager Lights
        {
            get { return lights; }
        }

        private void OnDrawGizmosSelected()
        {
            if (!stage)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(stage.Area.center + (Vector2)transform.position, stage.Area.size);
        }
    }
}
