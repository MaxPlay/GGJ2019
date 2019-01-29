using GGJ.Character;
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

        [SerializeField]
        SpriteFade fade;

        [Header("Light Setup")]
        [SerializeField]
        private List<LightTarget> targets;

        [SerializeField]
        private Transform[] followTargets;

        [SerializeField]
        private Transform playerTarget;

        [SerializeField]
        private LightManager lightManager;

        [SerializeField]
        private Snail character;

        [SerializeField]
        private StageLight[] bubbleLights;

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

        public void FreezePlayer() => character.Freeze();

        public void UnfreezePlayer() => character.Unfreeze();

        public void BubbleLightOn()
        {
            for (int i = 0; i < bubbleLights.Length; i++)
            {
                bubbleLights[i].FadeTo(3);
            }
        }

        public void BubbleLightOff()
        {
            for (int i = 0; i < bubbleLights.Length; i++)
            {
                bubbleLights[i].FadeTo(0);
            }
        }

        public void Close()
        {
            Application.Quit();
        }

        public void FadeOut() => fade.FadeOut();
    }
}
