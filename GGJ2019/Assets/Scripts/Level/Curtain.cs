using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ.Level
{
    class Curtain : MonoBehaviour
    {
        #region Variables

        [SerializeField]
        bool open = true;

        CurtainState state = CurtainState.Closed;

        float curtainState = 1;

        Vector3 startSize, startPosition;

        #region Serializefields

        [SerializeField]
        GameManager gameManager;

        [SerializeField]
        Transform curtain;

        [SerializeField]
        Vector3 sizeWhenOpen, PositionWhenOpen;

        [SerializeField]
        float closeSpeed;

        #endregion

        #endregion

        #region Properties



        #endregion

        #region Unity Methods

        private void Start()
        {
            gameManager.Update += GameManager_Update;
        }

        private void OnDestroy()
        {
            gameManager.Update -= GameManager_Update;
        }

        private void GameManager_Update()
        {
            switch(state)
            {
                case CurtainState.Closed:
                    state = Closed();
                    break;
                case CurtainState.Opening:
                    state = Opening();
                    break;
                case CurtainState.Open:
                    state = Opened();
                    break;
                case CurtainState.Closing:
                    state = Closing();
                    break;
            }
        }

        private void Awake()
        {
            startSize = transform.localScale;
            startPosition = transform.localPosition;
        }

        #endregion

        #region Public Methods

        public void Open()
        {
            open = true;
        }

        public void Close()
        {
            open = false;
        }

        #endregion

        #region Private Methods

        CurtainState Opened()
        {
            if(!open)
            {
                curtainState = 0;
                return CurtainState.Closing;
            }
            return CurtainState.Open;
        }

        CurtainState Closed()
        {
            if(open)
            {
                curtainState = 0;
                return CurtainState.Opening;
            }
            return CurtainState.Closed;
        }

        CurtainState Opening()
        {
            curtainState = Mathf.Min(curtainState + Time.deltaTime * closeSpeed, 1);
            curtain.localScale = Utility.VectorLerp(sizeWhenOpen, startSize, curtainState);
            curtain.localPosition = Utility.VectorLerp(PositionWhenOpen, startPosition, curtainState);
            if (curtainState >= 1)
            {
                return CurtainState.Open;
            }
            return CurtainState.Opening;
        }

        CurtainState Closing()
        {
            curtainState = Mathf.Min(curtainState + Time.deltaTime * closeSpeed, 1);
            curtain.localScale = Utility.VectorLerp(startSize, sizeWhenOpen, curtainState);
            curtain.localPosition = Utility.VectorLerp(startPosition, PositionWhenOpen, curtainState);
            if (curtainState >= 1)
            {
                return CurtainState.Closed;
            }
            return CurtainState.Closing;
        }

        #endregion

        #region Structs



        #endregion

        #region Enums

        enum CurtainState
        {
            Closed,
            Opening,
            Open,
            Closing
        }

        #endregion
    }
}
