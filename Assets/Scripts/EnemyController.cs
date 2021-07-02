using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private Waypoint _waypoint;
        [SerializeField]
        private float _movementSpeed;
        [SerializeField]
        private float _closeRange = 0.01f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            var distance = Vector2.Distance(transform.position, _waypoint.transform.position);
            var isClose = distance <= _closeRange;
            if (!isClose)
            {
                // Move to that position.
                var direction = _waypoint.transform.position - transform.position;
                transform.Translate(_movementSpeed * direction.normalized * Time.deltaTime);
            }
            else
            {
                if (_waypoint.IsEndPoint)
                {
                    // Stop

                }
                else
                {
                    // Find next point
                    _waypoint = _waypoint.NextWaypoint;
                }
            }
        }

        public void SetupWaypoint(Waypoint waypoint)
        {
            _waypoint = waypoint;
        }
    }
}
