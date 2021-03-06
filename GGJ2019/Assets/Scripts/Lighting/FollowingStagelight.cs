﻿using UnityEngine;

namespace GGJ.Lighting
{
    [ExecuteInEditMode]
    public class FollowingStagelight : StageLight
    {
        #region Private Fields

        [SerializeField]
        private Transform lookAt;

        public Transform LookAtTarget
        {
            get { return lookAt; }
            set { lookAt = value; }
        }

        #endregion Private Fields

        #region Private Methods

        protected override void Update()
        {
            if (lookAt)
                transform.LookAt(lookAt);
            base.Update();
        }

        #endregion Private Methods
    }
}