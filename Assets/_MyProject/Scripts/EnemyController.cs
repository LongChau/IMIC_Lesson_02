using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private EnemySO _enemyData;

        private Transform _destination;

        private int _pathPointIndex;

        private int currentHealth; 

        public List<Transform> Paths { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            _pathPointIndex = 0;
            _destination = Paths[_pathPointIndex];
            currentHealth = _enemyData.health;
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        private void Move()
        {
            // Check destination distance.
            var distance = Vector2.Distance(transform.position, _destination.position);
            var isClosedToDistance = distance <= 0.1f;
            if (isClosedToDistance)
            {
                // Change to next point.
                if (_pathPointIndex < Paths.Count - 1)
                {
                    _pathPointIndex++;
                    _destination = Paths[_pathPointIndex];
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                // Go to destination.
                transform.position = Vector2.MoveTowards(transform.position, _destination.position, _enemyData.speed * Time.deltaTime);
            }
        }

        [ContextMenu("TakeFullDamage")]
        private void TakeFullDamage()
        {
            int damg = currentHealth;
            currentHealth -= damg;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        [ContextMenu("Take10Damage")]
        private void Take10Damage()
        {
            int damg = 10;
            currentHealth -= damg;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
