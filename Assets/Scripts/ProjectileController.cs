using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField]
        private float _speed;

        private EnemyController _enemy;
        private int _damage;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_enemy == null) return;
            // Di chuyển đến enemy...
            transform.position = Vector2.MoveTowards(transform.position, _enemy.transform.position, _speed * Time.deltaTime);
        }

        private void LateUpdate()
        {
            if (_enemy == null) return;
            // Tính toán góc quay enemy.
            var direction = _enemy.transform.position - transform.position;
            var newRotation = Quaternion.FromToRotation(transform.right, direction.normalized);
            transform.Rotate(transform.forward, newRotation.eulerAngles.z);
        }

        public void SetupProjectile(EnemyController target, int damage)
        {
            _enemy = target;
            _damage = damage;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                bool IsCorrectTarget = _enemy.gameObject.GetInstanceID() == collision.gameObject.GetInstanceID();
                if (IsCorrectTarget)
                {
                    _enemy.TakeDamage(_damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}
