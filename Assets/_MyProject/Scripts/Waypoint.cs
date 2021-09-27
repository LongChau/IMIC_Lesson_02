using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField]
        private Waypoint _nextWaypoint;
        [SerializeField]
        private Color _color;

        [SerializeField]
        private bool _isEndWaypoint;

        public Waypoint NextWaypoint { get => _nextWaypoint; set => _nextWaypoint = value; }
        public bool IsEndWaypoint { get => _isEndWaypoint; set => _isEndWaypoint = value; }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Chỉ chạy trện editor.
        private void OnDrawGizmos()
        {
            if (_nextWaypoint == null) return;
            Debug.DrawLine(transform.position, _nextWaypoint.transform.position, _color);
        }
    }
}
