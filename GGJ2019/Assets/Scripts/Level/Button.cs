using GGJ.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static GGJ.GameManager;

namespace GGJ.Level
{
    class Button : MonoBehaviour
    {
        #region Variables

        bool activated, preActivated = false;

        public event UpdateEventHandler OnActivation;
        public event UpdateEventHandler OnDeactivation;

        #region Serializefields

        [SerializeField]
        GameManager gameManager;

        [SerializeField]
        SpringJoint2D spring;

        [SerializeField]
        float activationDistance;

        #endregion

        #endregion

        #region Properties

        public bool Activated
        {
            get { return activated; }
        }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if(!spring)
            {
                spring = GetComponent<SpringJoint2D>();
            }
        }

        private void Start()
        {
            gameManager.Update += GameManager_Update;
        }

        private void GameManager_Update()
        {
            CheckActivation();
            CheckEvents();
        }

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods

        void CheckActivation()
        {
            if ((spring.connectedBody.transform.position - transform.position).sqrMagnitude < (Mathf.Pow(activationDistance, 2)))
            {
                activated = true;
            }
            else
            {
                activated = false;
            }
        }

        void CheckEvents()
        {
            if(!preActivated && activated)
            {
                OnActivation.Invoke();
                preActivated = activated;
            }
            else if (preActivated && !activated)
            {
                OnDeactivation.Invoke();
                preActivated = activated;
            }
        }

        #endregion

        #region Structs



        #endregion

        #region Enums



        #endregion
    }
}
