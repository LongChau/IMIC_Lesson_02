using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TowerDefense
{
    public class WaypointController : MonoBehaviour
    {
        [SerializeField]
        private List<Waypoint> _listWaypoints = new List<Waypoint>();

        public List<Waypoint> ListWaypoints => _listWaypoints;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        [ContextMenu("SetupWaypoints")]
        private void SetupWaypoints()
        {
            // GetComponentsInChildren: Lấy các component có kiểu Waypoint. 
            // Convert array đó sang list dựa vào LINQ.
            _listWaypoints = GetComponentsInChildren<Waypoint>().ToList();
            // _listWaypoints.Count - 1 do không duyệt đến waypoint cuối cùng. 
            // => Waypoint cuối cùng không có nextWaypoint.
            for (int waypointIndex = 0; waypointIndex < _listWaypoints.Count - 1; waypointIndex++)
            {
                // Lấy waypoint tiếp theo...
                var nextIndex = waypointIndex + 1;
                var nextWaypoint = _listWaypoints[nextIndex];
                var currentWaypoint = _listWaypoints[waypointIndex];
                currentWaypoint.NextWaypoint = nextWaypoint;
            }
            _listWaypoints[_listWaypoints.Count - 1].IsEndWaypoint = true;
        }
    }
}
