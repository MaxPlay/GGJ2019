using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ.Character
{
    class Snail : MonoBehaviour
    {
        #region Variables

        float velocity;

        #region Serializefields

        [SerializeField]
        float borderDistance;

        [SerializeField]
        float acceleration, maxVelocity;

        #endregion

        #endregion

        #region Properties



        #endregion

        #region Unity Methods

        private void Update()
        {
            HandleInput();
            transform.Translate(Vector3.right * velocity * Time.deltaTime);

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Vector3 borderVector = new Vector3(-borderDistance, transform.position.y - 2, transform.position.z);

            Gizmos.DrawLine(borderVector, borderVector + Vector3.up * 4);
            borderVector.x = borderDistance;
            Gizmos.DrawLine(borderVector, borderVector + Vector3.up * 4);
        }

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods

        void HandleInput()
        {
            if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                velocity -= acceleration * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                velocity += acceleration * Time.deltaTime;
            }

            if(transform.position.x > borderDistance)
            {
                 velocity = Mathf.Min(0, velocity);
            }
            else if (transform.position.x < -borderDistance)
            {
                velocity = Mathf.Max(0, velocity);
            }
        }

        #endregion

        #region Structs



        #endregion

        #region Enums



        #endregion
    }
}
