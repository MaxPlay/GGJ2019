using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ
{
    class SlightMovement : MonoBehaviour
    {
        [SerializeField]
        GameManager gameManager;

        Vector3 defaultPosition;

        Vector3 addedPosition;

        float rand;

        [SerializeField]
        float speed ,speed1, speed2, speed3;

        [SerializeField]
        Vector3 moveScale;

        private void Start()
        {
            defaultPosition = transform.localPosition;
            gameManager.Update += GameManager_Update;
            rand = Random.value;
        }

        private void OnDestroy()
        {
            gameManager.Update -= GameManager_Update;
        }

        private void GameManager_Update()
        {
            addedPosition.x = Mathf.Sin((Time.time * speed1 + Time.time * speed2 + rand) * (1 - rand * rand * rand) * speed) * moveScale.x;
            addedPosition.y = Mathf.Sin((Time.time * speed2 + Time.time * speed3 + rand) * (1 - rand * rand * rand) * speed) * moveScale.y;
            addedPosition.z = Mathf.Sin((Time.time * speed1 + Time.time * speed3 + rand) * (1 - rand * rand * rand) * speed) * moveScale.z;
            transform.localPosition = defaultPosition + addedPosition;
        }
    }
}
