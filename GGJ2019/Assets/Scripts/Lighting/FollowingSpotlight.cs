using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

namespace GGJ.Lighting
{
    class FollowingSpotlight : MonoBehaviour
    {
        #region Variables



        #region Serializefields

        [SerializeField]
        Light spotlight;

        [SerializeField]
        Transform lookAt;

        #endregion

        #endregion

        #region Properties



        #endregion

        #region Unity Methods

        private void Awake()
        {
            if(!spotlight)
            {
                spotlight = GetComponent<Light>();
            }
        }

        private void Update()
        {
            if(lookAt && spotlight)
            {
                spotlight.transform.LookAt(lookAt);
            }
            else
            {
                Debug.Log("No Object to look at found");
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
