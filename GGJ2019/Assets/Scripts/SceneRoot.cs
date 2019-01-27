using GGJ.Level;
using GGJ.Lighting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ
{
    public class SceneRoot : MonoBehaviour
    {
        [SerializeField]
        private List<Curtain> curtains;


        [Header("Light Setup")]
        [SerializeField]
        private List<LightTarget> targets;

        [SerializeField]
        private Transform[] followTargets;

        [SerializeField]
        private Transform playerTarget;

        [SerializeField]
        private LightManager lightManager;

        public void OpenCurtain() => curtains.ForEach(c => c.Open());

        public void CloseCurtain() => curtains.ForEach(c => c.Close());

        public void PlayLighting() => lightManager.PauseOff();

        public void PauseLighting() => lightManager.PauseOn();

        public void FreeTargets() => targets.ForEach(t => t.Free());

        public void SetTargetsPlayer() => targets.ForEach(t => t.SetTarget(playerTarget));

        public void SetTargetsAnimation()
        {
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].SetTarget(followTargets[i]);
            }
        }
    }
}
