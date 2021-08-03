using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "EnemyAsset", menuName = "ConfigData/EnemyAsset")]
    public class EnemyAsset : ScriptableObject
    {
        [SerializeField]
        private int _attackDamage;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _closeRange = 0.01f;
        [SerializeField]
        private int _health;
        [SerializeField]
        private EnemyController _enemyPrefab;

        public int AttackDamage => _attackDamage;
        public float Speed => _speed;
        public float CloseRange => _closeRange;
        public int Health => _health;
        public EnemyController EnemyPrefab => _enemyPrefab;
    }
}
