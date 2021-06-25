using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class BuildPlaceController : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private TowerUIController _towerUIController;

        [SerializeField]
        private ETowerType _towerType;
        [SerializeField]
        private GameObject _sale;

        [SerializeField]
        private List<GameObject> _listTowers = new List<GameObject>();

        private bool _toggleActive;
        private GameObject _curTower;
        #endregion

        #region Property
        public bool IsBuilt { get; private set; }

        public ETowerType TowerType 
        { 
            get => _towerType;
            set
            {
                _towerType = value;
                if (_towerType == ETowerType.None)
                {
                    IsBuilt = false;
                    RemoveTower();
                }
                else
                {
                    IsBuilt = true;
                }
            }
        }
        #endregion

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
            // Listen to on touch object.
            TouchController.Event_TouchObject += Handle_Event_TouchObject;
            // Listen on touch UI...
            TouchController.Event_TouchUI += Handle_Event_TouchUI;
            TouchController.Event_TouchBackground += Handle_Event_TouchBackground;
        }

        private void RemoveTower()
        {
            Destroy(_curTower);
            _sale.SetActive(true);
            HideTowerUI();
        }

        private void Handle_Event_TowerSold()
        {
            TowerType = ETowerType.None;
        }

        private void Handle_Event_TouchBackground()
        {
            HideTowerUI();
        }

        private void Handle_Event_TouchUI()
        {
            HideTowerUI();
        }

        private void HideTowerUI()
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
            Debug.Log($"{name} Select this tower...");
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
            _towerUIController.ShowTowerUI(TowerType);
        }

        private void Handler_Event_TowerSelected(int towerIndex)
        {
            // Check if already built a tower.
            if (TowerType != ETowerType.None) return;  // Not allow to build another one.

            // Convert int to ETowerType
            TowerType = (ETowerType)towerIndex;
            Debug.Log($"{name} Build tower {TowerType}");

            // Instantiate tower.
            var towerPrefab = _listTowers[towerIndex - 1];
            _curTower = Instantiate(towerPrefab, transform.position, Quaternion.identity);

            // Set tower's parent to this.
            _curTower.transform.SetParent(transform);

            // Hide tower build UI.
            if (_toggleActive)
                ToggleSelect();

            // Hide sale object.
            _sale.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            _towerUIController.Event_TowerSelected -= Handler_Event_TowerSelected;
            _towerUIController.Event_TowerSold -= Handle_Event_TowerSold;
            TouchController.Event_TouchObject -= Handle_Event_TouchObject;
            TouchController.Event_TouchUI -= Handle_Event_TouchUI;
            TouchController.Event_TouchBackground -= Handle_Event_TouchBackground;
        }
    }
}
