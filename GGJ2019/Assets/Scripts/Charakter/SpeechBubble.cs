using GGJ.Level;
using System;
using TMPro;
using UnityEngine;

namespace GGJ.Charakter
{
    public class SpeechBubble : MonoBehaviour
    {
        [SerializeField]
        TextMeshPro textMesh;

        [SerializeField]
        private string currentKey;

        [SerializeField]
        private Stage stage;

        [SerializeField]
        private SceneRoot root;

        [Serializable]
        struct TextInput
        {
            public string Key;
            [TextArea]
            public string Value;
        }

        [SerializeField]
        private SpeechBubbleState state;

        [SerializeField]
        TextInput[] textInputs;
        private Vector3 upPos;
        private Vector3 downPos;
        [SerializeField]
        private AnimationCurve transition;
        private float timer;
        [SerializeField]
        private float speed;

        public void Show()
        {
            state = SpeechBubbleState.Up;
            root.BubbleLightOn();
        }

        private void Start()
        {
            upPos = transform.localPosition;
            transform.position -= (Vector3.up * stage.Area.height * 2);
            downPos = transform.localPosition;
            state = SpeechBubbleState.Down;
            timer = 0;
        }

        private void Update()
        {
            switch (state)
            {
                case SpeechBubbleState.Up:
                    timer += Time.deltaTime * speed;
                    if (timer >= 1)
                    {
                        timer = 1;
                        state = SpeechBubbleState.IdleUp;
                    }
                    break;
                case SpeechBubbleState.Down:
                    timer -= Time.deltaTime * speed;
                    if (timer <= 0)
                    {
                        timer = 0;
                        state = SpeechBubbleState.IdleDown;
                    }
                    break;
            }
            transform.localPosition = Vector3.Lerp(downPos, upPos, transition.Evaluate(timer));
        }

        public void Hide()
        {
            state = SpeechBubbleState.Down;
            root.BubbleLightOff();
        }

        public void SetText(string key)
        {
            currentKey = key;
            textMesh.SetText(GetValue(key));
        }

        private string GetValue(string key)
        {
            for (int i = 0; i < textInputs.Length; i++)
            {
                if (textInputs[i].Key == key)
                    return textInputs[i].Value;
            }

            return "not found";
        }

        public enum SpeechBubbleState
        {
            IdleUp,
            Up,
            IdleDown,
            Down
        }
    }
}
