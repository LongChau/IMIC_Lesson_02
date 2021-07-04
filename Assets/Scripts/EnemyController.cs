using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyController : MonoBehaviour
    {
        private Waypoint _waypoint;

        // Enemy data.
        [SerializeField]
        private EnemyAsset _enemyAsset;

        private int _attackDamage;
        private float _speed;
        private float _closeRange = 0.01f;
        private int _health;
        //

        public Waypoint Waypoint { get => _waypoint; set => _waypoint = value; }
        public int Health 
        { 
            get => _health;
            private set
            {
                _health = value;
                if (_health <= 0)
                {
                    Event_OnDeath?.Invoke();
                    Destroy(gameObject);
                }
            }
        }

        public event Action Event_OnDeath;

        // Start is called before the first frame update
        void Start()
        {
            _attackDamage       = _enemyAsset.AttackDamage;
            _speed              = _enemyAsset.Speed;
            _closeRange         = _enemyAsset.CloseRange;
            Health             = _enemyAsset.Health;
        }

        // Update is called once per frame
        void Update()
        {
            // Lấy khoảng cách với waypoint.
            var distance = Vector2.Distance(transform.position, _waypoint.transform.position);
            var isClose = distance <= _closeRange;
            // Tui có gần waypoint đó không?
            if (isClose)
            {
                // Xet xem waypoint đó có phải waypoint cuối cùng không?
                if (_waypoint.IsEndWaypoint)
                {
                    // Dừng...
                    // Trừ life và destroy enemy đi.
                    GameController.Instance.UpdateLife(-_attackDamage);
                    Destroy(gameObject);
                }
                else
                {
                    // Chuyển sang waypoint kế tiếp.
                    _waypoint = _waypoint.NextWaypoint;
                }    
            }
            else
            {
                // Tiếp tục di chuyển.
                var direction = _waypoint.transform.position - transform.position;
                transform.Translate(_speed * direction.normalized * Time.deltaTime);
            }
        }

        [ContextMenu("Test_TakeDamage")]
        private void TestTakeDamage()
        {
            TakeDamage(1);
        }

        public void TakeDamage(int damage)
        {
            Debug.Log($"Take damage: {damage}");
            Health -= damage;
            Debug.Log($"current health = {Health}");
        }
    }
}
