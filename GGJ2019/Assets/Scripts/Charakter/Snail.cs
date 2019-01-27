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

        Vector2 velocity;

        House currentHouse;

        float housePos = 1;

        Rigidbody2D houseRigid;

        bool canPickUp = true;

        public bool freezeActions = false;

        bool canDrop = true;

        #region Serializefields

        [SerializeField]
        GameManager gameManager;

        [SerializeField]
        Stage stage;

        [SerializeField]
        Transform worldTimeLine;

        [SerializeField]
        Rigidbody2D rigid;

        [SerializeField]
        Transform model;

        [SerializeField]
        Transform housePivot;

        [SerializeField]
        float borderDistance, topBorder, botBorder;

        [SerializeField]
        House starthouse;

        [SerializeField]
        float acceleration, maxVelocity;

        [SerializeField, Range(0, 1)]
        float drag, rotationSpeed;

        #endregion

        #endregion

        #region Properties

        public House CurrentHouse { get => currentHouse; }

        #endregion

        #region Unity Methods

        private void Start()
        {
            gameManager.Update += Snail_Update;
            if(starthouse)
            {
                CollectHouse(starthouse);
            }
        }

        private void OnDestroy()
        {
            gameManager.Update -= Snail_Update;
        }

        void Snail_Update()
        {
            if(!freezeActions)
            {
                HandleInput();
                CorrectSpeed();
            }

            HandleModel();
            HandleHouse();
            UpdateRigid();

            //transform.Translate(Vector3.right * velocity * Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Vector3 temp1 = new Vector3(-borderDistance, botBorder, transform.position.z);
            Vector3 temp2 = new Vector3(-borderDistance, topBorder, transform.position.z);

            Gizmos.DrawLine(temp1, temp2);
            temp1.x = borderDistance;
            temp2.x = borderDistance;
            Gizmos.DrawLine(temp1, temp2);
            temp1.x = -borderDistance;
            temp1.y = topBorder;
            Gizmos.DrawLine(temp1, temp2);
            temp1.y = botBorder;
            temp2.y = botBorder;
            Gizmos.DrawLine(temp1, temp2);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if(canPickUp)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    House levelHouse = collision.GetComponent<House>();
                    if (levelHouse)
                    {
                        CollectHouse(levelHouse);
                    }
                }
            }

            if(collision.gameObject.tag == "NoShellZone")
            {
                if(currentHouse)
                {
                    if(transform.position.x - collision.transform.position.x > 0)
                    {
                        velocity.x = Mathf.Max(velocity.x, 0);
                    }
                    else if (transform.position.x - collision.transform.position.x <= 0)
                    {
                        velocity.x = Mathf.Min(velocity.x, 0);
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "NoShellZone")
            {
                canPickUp = false;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "NoShellZone")
            {
                canPickUp = true;
            }
        }

        #endregion

        #region Public Methods

        void UpdateRigid()
        {
            if (currentHouse)
            {
                if (currentHouse.houseType == House.HouseType.Fly)
                {
                    rigid.velocity = velocity;
                    return;
                }
            }

            rigid.velocity = Vector2.up * rigid.velocity.y + Vector2.right * velocity;

        }

        public void LockHouse(bool lockState)
        {
            canDrop = lockState;
        }

        public void DropShell()
        {
            if(canDrop)
            {
                if (currentHouse.houseType == House.HouseType.Fly)
                {
                    rigid.gravityScale = 1;
                }
                else if (currentHouse.houseType == House.HouseType.Heavy)
                {
                    rigid.mass = 1;
                }
                currentHouse = null;
                houseRigid.bodyType = RigidbodyType2D.Dynamic;
                houseRigid = null;
            }
        }

        #endregion

        #region Private Methods

        private void CollectHouse(House levelHouse)
        {
            if(!CurrentHouse)
            {
                currentHouse = levelHouse;
                houseRigid = CurrentHouse.GetComponent<Rigidbody2D>();
                houseRigid.bodyType = RigidbodyType2D.Kinematic;
                housePos = 1;
                if(currentHouse.houseType == House.HouseType.Fly)
                {
                    rigid.gravityScale = 0;
                }
                else if(currentHouse.houseType == House.HouseType.Heavy)
                {
                    rigid.mass = houseRigid.mass;
                }
            }
        }

        private void HandleModel()
        {
            if(velocity.x > 0)
            {
                model.transform.eulerAngles = Utility.VectorLerp(model.transform.eulerAngles, Vector3.up * 180, 1 - rotationSpeed);
            }
            else if (velocity.x < 0)
            {
                model.transform.eulerAngles = Utility.VectorLerp(model.transform.eulerAngles, Vector3.zero, 1 - rotationSpeed);
            }
        }

        private void HandleHouse()
        {
            if(CurrentHouse)
            {
                houseRigid.velocity = Vector2.zero;
                housePos = Mathf.Max(0, housePos - 0.1f);
                CurrentHouse.transform.position = Utility.VectorLerp(CurrentHouse.transform.position, housePivot.transform.position, housePos);
                CurrentHouse.transform.rotation = housePivot.rotation;
            }
        }

        void CorrectSpeed()
        {
            velocity.x = Mathf.Min(maxVelocity, velocity.x);
            velocity.x = Mathf.Max(-maxVelocity, velocity.x);

            if (transform.position.x > borderDistance)
            {
                if(velocity.x > 0)
                {
                    worldTimeLine.Translate(Vector3.left * maxVelocity * Time.deltaTime);
                }
                velocity.x = Mathf.Min(0, velocity.x);
            }
            else if (transform.position.x < -borderDistance)
            {
                if (velocity.x < 0)
                {
                    worldTimeLine.Translate(Vector3.right * maxVelocity * Time.deltaTime);
                }
                velocity.x = Mathf.Max(0, velocity.x);
            }

            if(currentHouse)
            {
                if(currentHouse.houseType == House.HouseType.Fly)
                {
                    if (transform.position.y > topBorder)
                    {
                        velocity.y = Mathf.Min(0, velocity.y);
                    }
                    else if (transform.position.y < botBorder)
                    {
                        velocity.y = Mathf.Max(0, velocity.y);
                    }
                }
            }
        }

        void HandleInput()
        {
            MovementInput();
            HouseInput();
        }

        void HouseInput()
        {
            if(CurrentHouse)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    DropShell();
                }
            }
        }

        void MovementInput()
        {
            if(CurrentHouse)
            {
                if(CurrentHouse.houseType == House.HouseType.Fly)
                {
                    if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                    {
                        velocity.y += acceleration * Time.deltaTime;
                    }
                    else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                    {
                        velocity.y -= acceleration * Time.deltaTime;
                    }
                    else
                    {
                        velocity.y = velocity.y * drag;
                    }
                }
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                velocity.x -= acceleration * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                velocity.x += acceleration * Time.deltaTime;
            }
            else
            {
                velocity.x = velocity.x * drag;
            }
        }

        #endregion

        #region Structs



        #endregion

        #region Enums



        #endregion
    }
}
