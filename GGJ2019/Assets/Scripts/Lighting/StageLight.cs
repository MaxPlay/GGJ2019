using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Lighting
{
    public class StageLight : MonoBehaviour
    {
        [SerializeField]
        protected Light lightSource;

        private LightState current;
        private LightState target;
        private LightState offCache;
        private bool isOn;

        private AnimationProperties intensityProperties;
        private AnimationProperties colorProperties;

        public void Start()
        {
            target.Color = lightSource.color;
            target.Intensity = lightSource.intensity;
            isOn = lightSource.intensity <= 0.001f;
            intensityProperties.Timer = 1;
            colorProperties.Timer = 1;
        }

        public void FadeTo(Color color, float speed = 1)
        {
            current.Color = lightSource.color;
            target.Color = color;
            colorProperties.Speed = speed;
            colorProperties.Timer = 0;
        }

        public void FadeTo(float intensity, float speed = 1)
        {
            current.Intensity = lightSource.intensity;
            target.Intensity = intensity;
            intensityProperties.Speed = speed;
            intensityProperties.Timer = 0;
        }

        public void FadeTo(Color color, float intensity, float speed = 1)
        {
            FadeTo(color, speed);
            FadeTo(intensity, speed);
        }
        
        protected virtual void Update()
        {
            Animate(ref intensityProperties,
                () => { lightSource.intensity = target.Intensity; },
                (f) => { lightSource.intensity = Mathf.Lerp(current.Intensity, target.Intensity, f); });
            Animate(ref colorProperties,
                () => { lightSource.color = target.Color; },
                (f) => { lightSource.color = Color.Lerp(current.Color, target.Color, f); });
        }

        private void Animate(ref AnimationProperties properties, Action targetAssignment, Action<float> interpolation)
        {
            if (properties.Timer >= 1)
                return;
            properties.Timer += Time.deltaTime * properties.Speed;
            if (properties.Timer >= 1)
                targetAssignment();
            else
                interpolation(properties.Timer);
        }

        [Serializable]
        struct LightState
        {
            public Color Color;
            public float Intensity;
        }

        [Serializable]
        struct AnimationProperties
        {
            public float Speed;
            public float Timer;
        }
    }
}