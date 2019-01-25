using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ.Level
{
    public class WorldTimeline : MonoBehaviour
    {
        [SerializeField]
        private float min;
        [SerializeField]
        private float max;

        public bool IsVisible(WorldObject worldObject)
        {
            return Mathf.Abs(worldObject.transform.position.x - Position) < stage.Stage.Area.width * 0.5f;
        }

        private float Position => stage.transform.position.x;

        [SerializeField]
        private GameManager manager;

        [SerializeField]
        private StageComponent stage;

        public StageComponent Stage { get => stage; }

        private List<WorldObject> entities;

        private void Start()
        {
            entities = new List<WorldObject>(GetComponentsInChildren<WorldObject>());
            entities.ForEach((e) => { e.Timeline = this; e.Setup(); });
            manager.Update += Manager_Update;
        }

        private void OnDestroy()
        {
            manager.Update -= Manager_Update;
        }

        private void Manager_Update()
        {
            entities.ForEach((e) => { e.UpdateEntity(); });
        }

        private void OnDrawGizmos()
        {
            if (!stage) return;
            Gizmos.color = Color.green;
            Rect stageArea = stage.Stage.Area;
            float origin = transform.position.x;
            Gizmos.DrawLine(new Vector3(min + origin, stageArea.yMin), new Vector3(max + origin, stageArea.yMin));
            Gizmos.DrawLine(new Vector3(min + origin, stageArea.yMax), new Vector3(max + origin, stageArea.yMax));

            Gizmos.color = new Color(0, 1, 0, 0.3f);
            for (int f = (int)min / 10 * 10; f < max; f += 10)
                Gizmos.DrawLine(new Vector3(f + origin, stageArea.yMin), new Vector3(f + origin, stageArea.yMax));

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(new Vector3(stageArea.xMin + Position, stageArea.yMin), new Vector3(stageArea.xMin + Position, stageArea.yMax));
            Gizmos.DrawLine(new Vector3(stageArea.xMax + Position, stageArea.yMin), new Vector3(stageArea.xMax + Position, stageArea.yMax));
        }
    }
}
