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
        private bool _isEndedPoint;

        public bool IsEndPoint { get => _isEndedPoint; set => _isEndedPoint = value; }
        public Waypoint NextWaypoint => _nextWaypoint;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetupNextWaypoint(Waypoint wp)
        {
            _nextWaypoint = wp;
        }

        private void OnDrawGizmos()
        {
            if (_nextWaypoint != null)
                Debug.DrawLine(this.transform.position, _nextWaypoint.transform.position, _color);
        }
    }
}
