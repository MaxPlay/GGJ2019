using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ.Character
{
    class StickJoint: MonoBehaviour
    {
        #region Variables



        #region Serializefields

        [SerializeField]
        Transform topJoint, bottomJoint;

        [SerializeField]
        float bottomJointDistance;

        #endregion

        #endregion

        #region Properties



        #endregion

        #region Unity Methods

        private void Update()
        {
            if(topJoint && bottomJoint)
            {
                transform.position = topJoint.position * 0.5f + (bottomJoint.position + Vector3.right * bottomJointDistance) * 0.5f;
                transform.forward = topJoint.position - (bottomJoint.position + Vector3.right * bottomJointDistance);
                transform.Rotate(Vector3.right, 90);
            }
        }

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods



        #endregion

        #region Structs



        #endregion

        #region Enums



        #endregion
    }
}
