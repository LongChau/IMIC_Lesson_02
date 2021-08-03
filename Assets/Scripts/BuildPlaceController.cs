using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class BuildPlaceController : MonoBehaviour
    {
        [SerializeField]
        private TowerUIController _towerUIController;

        [SerializeField]
        private ETowerType _towerType;
        [SerializeField]
        private GameObject _sale;
        [SerializeField]
        private List<TowerAsset> _listTowerAssets = new List<TowerAsset>();

        private bool _toggleActive;
        private GameObject _tower;
        private Tower _towerData;
        private TowerAsset _currentTowerAsset;

        private void Awake()
        {
            // Hide tower UI by default!
            _towerUIController.gameObject.SetActive(false);
        }

        // Start is called before the first frame update
        void Start()
        {
            // Listen to event TowerUIController.
            _towerUIController.Event_TowerSelected += Handler_Event_TowerSelected;
            _towerUIController.Event_TowerSold += Handle_Event_TowerSold;
            _towerUIController.Event_TowerUpgrade += Handle_Event_TowerUpgrade;
            // Listen to on touch object.
            TouchController.Event_TouchObject += Handle_Event_TouchObject;
            // Listen on touch UI...
            TouchController.Event_TouchUI += Handle_Event_TouchUI;
        }

        private void Handle_Event_TowerUpgrade()
        {
            UpgradeTower();
        }

        private void Handle_Event_TowerSold()
        {
            // bán tower đi...
            SellTower();
        }

        public void SellTower()
        {
            DeleteTower();

            // Add money cho player.
            GameController.Instance.UpdateGold(_towerData.SellCost);
        }

        private void DeleteTower()
        {
            // Bật lại bảng sale
            _sale.gameObject.SetActive(true);
            // Tắt UI build.
            ToggleSelect();
            // Destroy tower hiện tại.
            Destroy(_tower);
            // Set lai5 tower type
            _towerType = ETowerType.None;
        }

        private void Handle_Event_TouchUI()
        {
            if (_toggleActive)
            {
                Debug.Log($"{name} Unselect this tower...");
                ToggleSelect();
            }
        }

        /// <summary>
        /// Check xem đang touch vô cái object gì...
        /// </summary>
        /// <param name="instanceId"></param>
        private void Handle_Event_TouchObject(int instanceId)
        {
            // Check coi có phải đang touch vô tui hok???
            // Check for same instanceId.
            if (instanceId != gameObject.GetInstanceID())   // Hok phải tui :((
            {
                // Tắt đi...
                // Check to unselect this tower.
                if (_toggleActive)
                {
                    // nếu tui đang mở build tower UI thì tắt tui đi...
                    Debug.Log($"{name} Unselect this tower...");
                    ToggleSelect();
                }
                return;
            }

            // Bật tui lên nè...
            // Select this tower.
            ToggleSelect();
        }

        /// <summary>
        /// Đổi từ OnMouseDown sang...
        /// ToggleSelect
        /// </summary>
        private void ToggleSelect()
        {
            Debug.Log($"BuildPlaceController.Select() {gameObject.name}");
            // Toggle show/hide tower UI.
            // Bật/Tắt gameobject. '!' hoạt động như 1 công tắc tắt mở.
            _toggleActive = !_towerUIController.gameObject.activeSelf;
            _towerUIController.gameObject.SetActive(_toggleActive);
            // Show UI của tower được chọn.
            _towerUIController.ShowTowerUI(_towerType);
        }

        private void Handler_Event_TowerSelected(int towerIndex)
        {
            // Check if already built a tower.
            if (_towerType != ETowerType.None) return;  // Not allow to build another one.

            // Get the tower asset...
            _currentTowerAsset = _listTowerAssets[towerIndex - 1];
            _towerData = _currentTowerAsset.towerData;  // Cached tower data...
            SpawnTower();
        }

        private void SpawnTower()
        {
            _towerType = _towerData.TowerType;          // Get tower type.
            var towerPrefab = _towerData.TowerPrefab;   // Get tower prefab...

            // Instantiate tower.
            _tower = Instantiate(towerPrefab, transform.position, Quaternion.identity);

            // Set tower's parent to this.
            _tower.transform.SetParent(transform);

            // Hide tower build UI.
            if (_toggleActive)  // Nếu UI tower đang bật...
                ToggleSelect(); // Toggle lại cho nó tắt...

            // Hide sale object.
            _sale.gameObject.SetActive(false);

            // Update cost
            GameController.Instance.UpdateGold(-_towerData.Cost);
        }

        [ContextMenu("UpgradeTower")]
        public void UpgradeTower()
        {
            // Check xem có đủ tiền không?
            if (GameController.Instance.LevelData.Gold < _towerData.Cost)
            {
                Debug.LogError("Cannot upgrade... Not enought gold...");
                return;
            }

            // Check xem tower có upgrade được không.
            //if (!_towerData.CanUpgradable)
            if (_towerData.CanUpgradable == false)
            {
                Debug.LogError("Cannot upgrade... max level...");
                return;
            }
            // Xử lý...
            Debug.Log("Let's upgrade tower!");
            // Thay đổi towerData sang tower nâng cấp.
            _currentTowerAsset = _currentTowerAsset.towerData.UpgradeTower;
            _towerData = _currentTowerAsset.towerData;
            // Delete tower.
            DeleteTower();
            // Spawn new tower.
            SpawnTower();
            // TODO: Update tower UI...

            Debug.Log($"Upgrade to tower {_currentTowerAsset.name}");
        }

        private void OnDestroy()
        {
            _towerUIController.Event_TowerSelected -= Handler_Event_TowerSelected;
            _towerUIController.Event_TowerSold -= Handle_Event_TowerSold;
            _towerUIController.Event_TowerUpgrade -= Handle_Event_TowerUpgrade;
            TouchController.Event_TouchObject -= Handle_Event_TouchObject;
            TouchController.Event_TouchUI -= Handle_Event_TouchUI;
        }
    }
}
