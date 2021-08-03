using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    [Serializable]
    public class Tower
    {
        [SerializeField]
        private ETowerType _towerType;
        [SerializeField]
        private int _cost;
        [SerializeField]
        private int _sellCost;
        [SerializeField]
        private int _level;
        [SerializeField]
        private int _damage;
        [SerializeField]
        private float _visionRadius;
        [SerializeField]
        private float _shootIntervalTime;
        [SerializeField]
        private Sprite _towerIcon;
        [SerializeField]
        private TowerAsset _upgradeTower;
        [SerializeField]
        private GameObject _towerPrefab;
        [SerializeField]
        private GameObject _projectilePrefab;
        //[SerializeField]
        //private Skill _skill;

        public ETowerType TowerType { get => _towerType; set => _towerType = value; }
        public int Cost { get => _cost; set => _cost = value; }
        public int SellCost { get => _sellCost; set => _sellCost = value; }
        public int Level { get => _level; set => _level = value; }
        public int Damage { get => _damage; set => _damage = value; }
        public Sprite TowerIcon { get => _towerIcon; set => _towerIcon = value; }
        public TowerAsset UpgradeTower { get => _upgradeTower; set => _upgradeTower = value; }
        public GameObject TowerPrefab { get => _towerPrefab; set => _towerPrefab = value; }

        //public bool CanUpgradable => _upgradeTower != null;
        /// <summary>
        /// Check upgrade tower được không dựa trên _upgradeTower có khác null không.
        /// </summary>
        public bool CanUpgradable
        {
            get
            {
                return _upgradeTower != null;
            }
        }

        public float VisionRadius { get => _visionRadius; set => _visionRadius = value; }
        public GameObject ProjectilePrefab { get => _projectilePrefab; set => _projectilePrefab = value; }
        public float ShootIntervalTime { get => _shootIntervalTime; set => _shootIntervalTime = value; }
    }
}
