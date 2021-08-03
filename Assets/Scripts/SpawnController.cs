using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField]
        private WaypointController _waypointController;
        [SerializeField]
        private EnemyAsset _enemyAsset;

        // Start is called before the first frame update
        void Start()
        {
            var enemy = Instantiate(_enemyAsset.EnemyPrefab, transform.position, Quaternion.identity);
            enemy.name += enemy.gameObject.GetInstanceID();
            enemy.Waypoint = _waypointController.ListWaypoints[0];
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
