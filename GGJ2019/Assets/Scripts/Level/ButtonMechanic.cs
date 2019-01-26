using GGJ.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;
using static GGJ.GameManager;

namespace GGJ.Level
{
    class ButtonMechanic : MonoBehaviour
    {
        #region Variables

        bool activated, preActivated = false;

        float springDistance;

        public UnityEvent OnActivation;
        public UnityEvent OnDeactivation;

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
            OnActivation = new UnityEvent();
            OnDeactivation = new UnityEvent();

            if (!spring)
            {
                spring = GetComponent<SpringJoint2D>();
            }
            springDistance = spring.distance;
        }

        private void Start()
        {
            gameManager.Update += GameManager_Update;
        }

        private void GameManager_Update()
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
            //spring.distance = springDistance;
            CheckActivation();
            CheckEvents();
        }

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods

        void CheckActivation()
        {
            if ((spring.connectedBody.transform.position - spring.transform.position).sqrMagnitude < (Mathf.Pow(activationDistance, 2)))
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
