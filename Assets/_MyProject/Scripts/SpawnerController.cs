using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class SpawnerController : MonoBehaviour
    {
        [SerializeField]
        private float _spawnTime;
        [SerializeField]
        private EnemyController _enemyPrefab;
        [SerializeField]
        private PathController _path;

        private void OnValidate()
        {
            _path = GetComponentInChildren<PathController>();
        }

        //private void Awake()
        //{
        //    _path = GetComponentInChildren<PathController>();
        //}

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(IESpawnWithTime(_spawnTime));
        }

        private IEnumerator IESpawnWithTime(float time)
        {
            var wait = new WaitForSecondsRealtime(time);
            while (true)
            {
                var enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
                enemy.Paths = _path.Paths;
                yield return wait;
            }
        }
    }
}
