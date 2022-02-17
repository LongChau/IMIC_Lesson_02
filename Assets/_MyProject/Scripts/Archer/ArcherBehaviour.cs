using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(TowerController))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class ArcherBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float _attackRange;
        [SerializeField]
        private TowerController _towerCtrl;
        [SerializeField]
        private GameObject _arrowPrefab;
        [SerializeField]
        private CircleCollider2D _circleCollider;

        private EnemyController _target;

        private Dictionary<int, EnemyController> _dictEnemies = new Dictionary<int, EnemyController>();

        private float _time;

        private void OnValidate()
        {
            _towerCtrl = GetComponent<TowerController>();
            _circleCollider = GetComponent<CircleCollider2D>();
            _circleCollider.radius = _attackRange;
            _circleCollider.isTrigger = true;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_target == null) return;
            _time += Time.deltaTime;
            if (_time >= 1.0f)
            {
                // TODO: Instantiate bullet. 
                var arrow = Instantiate(_arrowPrefab, transform.position, Quaternion.identity);
                var direction = (_target.transform.position - transform.position).normalized;
                var damageObject = arrow.GetComponent<DamageObject>();
                damageObject.Direction = direction;
                damageObject.SpriteChild.right = direction;
                damageObject.Damage = _towerCtrl.TowerConfig.AttackDamage;
                //var angle = CalculateAngle(damageObject);
                //Debug.Log(angle);
                _time = 0f;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Enemy")) return;
            if (_dictEnemies.ContainsKey(collision.gameObject.GetInstanceID())) return;
            _dictEnemies.Add(collision.gameObject.GetInstanceID(), 
                collision.gameObject.GetComponent<EnemyController>());
            _target = _dictEnemies.First().Value;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Enemy")) return;

            _dictEnemies.Remove(collision.gameObject.GetInstanceID());
            _target = null;
            if (_dictEnemies.Count == 0) return;
            _target = _dictEnemies.First().Value;
        }

        private float CalculateAngle(DamageObject dmgObj, bool low = false)
        {
            var targetDir = (_target.transform.position - transform.position).normalized;
            float y = targetDir.y;
            y = 0f;
            float x = targetDir.magnitude;
            float gravity = 9.81f;
            float square = dmgObj.Speed * dmgObj.Speed;
            float underSquareRoot = (float)(Math.Pow(dmgObj.Speed, 4) - gravity * (gravity * Math.Pow(x, 2) + 2 * y * Math.Pow(dmgObj.Speed, 2)));
            if (underSquareRoot >= 0f)
            {
                float root = Mathf.Sqrt(underSquareRoot);
                float highAngle = square + root;
                float lowAngle = square - root;

                if (low) return lowAngle;
                else return highAngle;
            }
            else
                return 0f;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }
}
