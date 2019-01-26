using GGJ.Level;
using GGJ.Powerups;
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

        House currentHouse;

        float housePos = 1;

        #region Serializefields

        [SerializeField]
        GameManager gameManager;

        [SerializeField]
        Stage stage;

        [SerializeField]
        Rigidbody2D rigid;

        [SerializeField]
        Transform model;

        [SerializeField]
        Transform housePivot;

        [SerializeField]
        float borderDistance;

        [SerializeField]
        House starthouse;

        [SerializeField]
        float acceleration, maxVelocity;

        [SerializeField, Range(0, 1)]
        float drag, rotationSpeed;

        #endregion

        #endregion

        #region Properties



        #endregion

        #region Unity Methods

        private void Start()
        {
            gameManager.Update += Snail_Update;
            currentHouse = starthouse;
        }

        private void OnDestroy()
        {
            gameManager.Update -= Snail_Update;
        }

        void Snail_Update()
        {
            HandleInput();
            CorrectSpeed();
            HandleModel();
            HandleHouse();
            rigid.velocity = Vector2.up * rigid.velocity.y + Vector2.right * velocity;
            //transform.Translate(Vector3.right * velocity * Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Vector3 borderVector = new Vector3(-borderDistance, transform.position.y - 2, transform.position.z);

            Gizmos.DrawLine(borderVector, borderVector + Vector3.up * 4);
            borderVector.x = borderDistance;
            Gizmos.DrawLine(borderVector, borderVector + Vector3.up * 4);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                House levelHouse = collision.GetComponent<House>();
                if(levelHouse)
                {
                    CollectHouse(levelHouse);
                }
            }
        }

        private void CollectHouse(House levelHouse)
        {
            if(currentHouse)
            {
                currentHouse = null;
            }
            currentHouse = levelHouse;
            housePos = 1;
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        private void HandleModel()
        {
            if(velocity > 0)
            {
                model.transform.eulerAngles = Utility.VectorLerp(model.transform.eulerAngles, Vector3.up * 180, 1 - rotationSpeed);
            }
            else if (velocity < 0)
            {
                model.transform.eulerAngles = Utility.VectorLerp(model.transform.eulerAngles, Vector3.zero, 1 - rotationSpeed);
            }
        }

        private void HandleHouse()
        {
            if(currentHouse)
            {
                housePos = Mathf.Max(0, housePos - 0.1f);
                currentHouse.transform.position = Utility.VectorLerp(currentHouse.transform.position, housePivot.transform.position, housePos);
                currentHouse.transform.rotation = housePivot.rotation;
            }
        }

        void CorrectSpeed()
        {
            velocity = Mathf.Min(maxVelocity, velocity);
            velocity = Mathf.Max(-maxVelocity, velocity);

            if (transform.position.x > borderDistance)
            {
                velocity = Mathf.Min(0, velocity);
            }
            else if (transform.position.x < -borderDistance)
            {
                velocity = Mathf.Max(0, velocity);
            }
        }

        void HandleInput()
        {
            MovementInput();
            HouseInput();
        }

        void HouseInput()
        {
            if(currentHouse)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    currentHouse.Action(this);
                }
                else if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    DropShell();
                }
            }
        }

        private void DropShell()
        {
            currentHouse = null;
        }

        void MovementInput()
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                velocity -= acceleration * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                velocity += acceleration * Time.deltaTime;
            }
            else
            {
                velocity = velocity * drag;
            }
        }

        #endregion

        #region Structs



        #endregion

        #region Enums



        #endregion
    }
}
