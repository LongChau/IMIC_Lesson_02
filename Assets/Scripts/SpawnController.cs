using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField]
        private EnemyController _enemyPrefab;
        [SerializeField]
        private WaypointController _waypointController;

        // Start is called before the first frame update
        void Start()
        {
            var enemy = Instantiate(_enemyPrefab, transform);
            enemy.SetupWaypoint(_waypointController.Waypoints[0]);
        }
    }
}
