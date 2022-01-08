using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class PathController : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> _paths = new List<Transform>();

        public List<Transform> Paths => _paths;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        [ContextMenu("GetPath")]
        private void GetPath()
        {
            _paths.Clear();
            for (int pointIndex = 0; pointIndex < transform.childCount; pointIndex++)
            {
                var point = transform.GetChild(pointIndex);
                _paths.Add(point);
            }
        }

        // Vẽ gizmos trên editor. 
        // Không chạy khi runtime.
        private void OnDrawGizmos()
        {
            //Gizmos.DrawSphere(transform.position, 1f);
            Gizmos.color = Color.red;   
            if (_paths != null && _paths.Count > 0)
            {
                //Gizmos.DrawLine(_paths[0].position, _paths[1].position);
                //Gizmos.DrawLine(_paths[1].position, _paths[2].position);
                for (int pointIndex = 0; pointIndex < _paths.Count - 1; pointIndex++)
                {
                    Gizmos.DrawLine(_paths[pointIndex].position, _paths[pointIndex + 1].position);
                }
            }
        }
    }
}
