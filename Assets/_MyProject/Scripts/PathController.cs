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
    }
}
