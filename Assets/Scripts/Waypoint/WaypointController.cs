using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TowerDefense
{
    public class WaypointController : MonoBehaviour
    {
        [SerializeField]
        private List<Waypoint> _waypoints = new List<Waypoint>();

        public List<Waypoint> Waypoints => _waypoints;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        [ContextMenu("SetupWaypoint")]
        private void SetupWaypoint()
        {
            _waypoints = GetComponentsInChildren<Waypoint>().ToList();

            for (int waypointIndex = 0; waypointIndex < _waypoints.Count - 1; waypointIndex++)
            {
                int nextIndex = waypointIndex + 1;
                _waypoints[waypointIndex].SetupNextWaypoint(_waypoints[nextIndex]);
            }
            // Setup last waypoint.
            _waypoints[_waypoints.Count - 1].IsEndPoint = true;
        }
    }
}
