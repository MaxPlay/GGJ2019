using GGJ.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ
{
    class CheckWorldActivity: MonoBehaviour
    {
        [SerializeField]
        GameManager gameManager;

        [SerializeField]
        WorldObject obj;

        [SerializeField]
        Rigidbody2D rigid;

        private void Start()
        {
            if(!rigid)
            {
                rigid = GetComponent<Rigidbody2D>();
            }
            gameManager.Update += GameManager_Update;
            gameManager.FixedUpdate += GameManager_FixedUpdate;
        }

        private void OnDestroy()
        {
            gameManager.Update -= GameManager_Update;
            gameManager.FixedUpdate -= GameManager_FixedUpdate;
        }

        private void GameManager_FixedUpdate()
        {

        }

        private void GameManager_Update()
        {
            if (obj.State == WorldObjectState.IdleUp)
            {
                rigid.bodyType = RigidbodyType2D.Dynamic;
            }
            else if (obj.State == WorldObjectState.Down)
            {
                rigid.bodyType = RigidbodyType2D.Kinematic;
                rigid.velocity = Vector2.zero;
            }
        }
    }
}
