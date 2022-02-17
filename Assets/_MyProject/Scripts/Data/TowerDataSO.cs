using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TowerDefense.GameDefine;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "TowerDataSO", menuName = "TowerDefense/DataAsset/TowerDataSO")]
    public class TowerDataSO : ScriptableObject
    {
        [SerializeField]
        private int _id;
        [SerializeField]
        private ETowerType _towerType;
        [SerializeField]
        private int _attackDamage;
        [SerializeField]
        private int _level;
        [SerializeField]
        private int _buildCost;
        [SerializeField]
        private int _sellCost;
        [SerializeField]
        private GameObject _towerPrefab;
        [SerializeField]
        private Sprite _icon;
        [SerializeField]
        private TowerDataSO _upgradedTower;

        public int Id { get => _id; set => _id = value; }
        public ETowerType TowerType { get => _towerType; set => _towerType = value; }
        public int AttackDamage { get => _attackDamage; set => _attackDamage = value; }
        public int Level { get => _level; set => _level = value; }
        public int BuildCost { get => _buildCost; set => _buildCost = value; }
        public int SellCost { get => _sellCost; set => _sellCost = value; }
        public GameObject TowerPrefab { get => _towerPrefab; set => _towerPrefab = value; }
        public Sprite Icon { get => _icon; set => _icon = value; }
        public TowerDataSO UpgradedTower { get => _upgradedTower; set => _upgradedTower = value; }
    }

    public class TowerData
    {
        [SerializeField]
        private int _id;
        [SerializeField]
        private ETowerType _towerType;
        [SerializeField]
        private int _attackDamage;
        [SerializeField]
        private int _level;
        [SerializeField]
        private int _buildCost;
        [SerializeField]
        private int _sellCost;
        [SerializeField]
        private GameObject _towerPrefab;
        [SerializeField]
        private Sprite _icon;
        [SerializeField]
        private TowerDataSO _upgradedTower;

        public int Id { get => _id; set => _id = value; }
        public ETowerType TowerType { get => _towerType; set => _towerType = value; }
        public int AttackDamage { get => _attackDamage; set => _attackDamage = value; }
        public int Level { get => _level; set => _level = value; }
        public int BuildCost { get => _buildCost; set => _buildCost = value; }
        public int SellCost { get => _sellCost; set => _sellCost = value; }
        public GameObject TowerPrefab { get => _towerPrefab; set => _towerPrefab = value; }
        public Sprite Icon { get => _icon; set => _icon = value; }
        public TowerDataSO UpgradedTower { get => _upgradedTower; set => _upgradedTower = value; }

        public TowerData(TowerDataSO config)
        {

        }
    }
}
