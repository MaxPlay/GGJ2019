using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ.Level
{
    class DoorMechanic : MonoBehaviour
    {
        #region Variables

        #region Serializefields

        [SerializeField]
        GameManager gameManager;

        [SerializeField]
        Rigidbody2D rigid;

        [SerializeField]
        ButtonMechanic button;

        [SerializeField]
        float targetHeight;

        [SerializeField]
        float moveSpeed;

        #endregion

        #endregion

        #region Properties



        #endregion

        #region Unity Methods

        private void Start()
        {
            gameManager.Update += GameManager_Update;
        }

        private void GameManager_Update()
        {
            Checkbutton();
        }

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods

        void Checkbutton()
        {
            if(button.Activated)
            {
                if(rigid.bodyType == RigidbodyType2D.Dynamic)
                {
                    rigid.bodyType = RigidbodyType2D.Kinematic;
                }
                RaiseDoor();
            }
            else
            {
                if (rigid.bodyType == RigidbodyType2D.Kinematic)
                {
                    rigid.bodyType = RigidbodyType2D.Dynamic;
                }
            }
        }

        void RaiseDoor()
        {
            if (transform.position.y < targetHeight)
                rigid.velocity = Vector2.up * moveSpeed;
            else
            {
                rigid.velocity = Vector2.zero;
            }
        }

        #endregion

        #region Structs



        #endregion

        #region Enums



        #endregion
    }
}
