using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ.Lighting
{
    public class SpriteFade : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        [SerializeField]
        AnimationCurve easing;

        [SerializeField]
        float speed = 1;

        private float target;
        private float timer = 1;
        private float origin;

        private void Update()
        {
            if (timer >= 1)
                return;

            timer += Time.deltaTime * speed;

            Color c = image.color;
            c.a = Mathf.Min(Mathf.Lerp(origin, target, easing.Evaluate(timer)), 1);
            image.color = c;
        }

        public void FadeIn()
        {
            target = 0;
            timer = 0;
            origin = image.color.a;
        }

        public void FadeOut()
        {
            target = 1;
            timer = 0;
            origin = image.color.a;
        }
    }
}
