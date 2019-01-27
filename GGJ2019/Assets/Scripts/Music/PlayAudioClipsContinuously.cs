using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ.Music
{
    public class PlayAudioClipsContinuously : MonoBehaviour
    {
        [SerializeField]
        private AudioSource start;
        [SerializeField]
        private AudioSource loop;
        
        private void Start()
        {
            Play();
        }

        public void Play()
        {
            start.Play();
            loop.PlayDelayed(start.clip.length);
        }
    }
}
