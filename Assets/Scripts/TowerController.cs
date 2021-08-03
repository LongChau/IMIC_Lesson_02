using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TowerController : MonoBehaviour
    {
        [SerializeField]
        private TowerAsset _towerAsset;
        [SerializeField]
        private CircleCollider2D _vision;

        private int _damage;
        private EnemyController _target;
        private List<EnemyController> _listTargets = new List<EnemyController>();

        private Coroutine _shootCoroutine;

        public bool IsLockTarget => _target != null;

        // Start is called before the first frame update
        void Start()
        {
            _towerAsset     = Instantiate(_towerAsset);
            _vision.radius  = _towerAsset.towerData.VisionRadius;
            _damage         = _towerAsset.towerData.Damage;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                var enemy = collision.gameObject.GetComponent<EnemyController>();
                var isContainsEnemy = _listTargets.Contains(enemy);
                if (!isContainsEnemy)
                    _listTargets.Add(enemy);
                if (!IsLockTarget)
                {
                    _target = enemy;
                    _target.Event_OnDeath += Handle_Event_OnDeath;
                    _shootCoroutine = StartCoroutine(IEShoot());
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy") && IsLockTarget)
            {
                Debug.Log($"Enemy {collision.gameObject.name} exit vision");
                bool isSameId = _target.gameObject.GetInstanceID() == collision.gameObject.GetInstanceID();
                if (isSameId)
                    RemoveTarget();
            }
        }

        private void Handle_Event_OnDeath()
        {
            Debug.Log("This target is killed");
            RemoveTarget();
        }

        private void RemoveTarget()
        {
            Debug.Log($"RemoveTarget() {_target.name}");
            _listTargets.Remove(_target);
            StopCoroutine(_shootCoroutine);
            _target.Event_OnDeath -= Handle_Event_OnDeath;
            _target = null;
            CheckNextTarget();
        }

        private void CheckNextTarget()
        {
            if (_listTargets.Count > 0)
            {
                _target = _listTargets[0];
                Debug.Log($"CheckNextTarget() {_target.name}");
                _target.Event_OnDeath += Handle_Event_OnDeath;
                _shootCoroutine = StartCoroutine(IEShoot());
            }
        }

        private IEnumerator IEShoot()
        {
            var wait = new WaitForSecondsRealtime(_towerAsset.towerData.ShootIntervalTime);
            while (IsLockTarget)
            {
                var projectile = Instantiate(_towerAsset.towerData.ProjectilePrefab,
                   transform.position, Quaternion.identity).GetComponent<ProjectileController>();
                projectile.SetupProjectile(_target, _damage);
                yield return wait;
            }
        }
    }
}
