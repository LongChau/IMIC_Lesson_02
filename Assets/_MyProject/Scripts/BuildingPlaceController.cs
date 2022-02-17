using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class BuildingPlaceController : MonoBehaviour
    {
        [Header("UI:")]
        [SerializeField]
        private RectTransform _pnlBuildTower;
        [SerializeField]
        private RectTransform _pnlTowerUI;

        [Space(30)]

        [SerializeField]
        private Transform _place;

        private TowerController _currentTowerController;
        private bool _isSelected;

        public bool HasTower => _currentTowerController != null;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;

                if (_isSelected)
                {
                    if (HasTower)
                    {
                        _pnlBuildTower.gameObject.SetActive(false);
                        _pnlTowerUI.gameObject.SetActive(true);
                    }
                    else
                    {
                        _pnlBuildTower.gameObject.SetActive(true);
                        _pnlTowerUI.gameObject.SetActive(false);
                    }
                }
                else
                {
                    _pnlBuildTower.gameObject.SetActive(false);
                    _pnlTowerUI.gameObject.SetActive(false);
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            EventManager.Instance.Event_OnTouchSomething.AddListener(Handle_Event_OnTouchSomething);
            EventManager.Instance.Event_OnBtnBuildTowerClicked.AddListener(Handle_Event_OnBtnBuildTowerClicked);
            MapController.Event_TouchMap.AddListener(Handle_Event_TouchMap);
        }

        private void Handle_Event_OnTouchSomething(long objectId)
        {
            IsSelected = gameObject.GetInstanceID() == objectId;
        }

        private void Handle_Event_TouchMap()
        {
            Debug.Log("Handle_Event_TouchMap()");
            IsSelected = false;
        }

        private void Handle_Event_OnBtnBuildTowerClicked(GameDefine.ETowerType towerType)
        {
            if (HasTower)
            {
                Debug.Log("<color=cyan> Already has tower. </color>");
                return;
            }
            if (!_isSelected) return;
            BuildTower(towerType);
        }

        private void BuildTower(GameDefine.ETowerType towerType, int level = 1)
        {
            Debug.Log($"Build {towerType}");
            // Check tower type to build.\
            // Lấy tower config tương ứng.
            var towerConfig = GameManager.Instance.GetTowerConfig(towerType, level);

            // Check coi có build dc toewr hay không?
            bool enoughMoney = GameManager.Instance.UserData.Gold >= towerConfig.BuildCost;
            if (!enoughMoney)
            {
                Debug.Log("Not enough money!");
                return;
            }

            var towerObject = Instantiate(towerConfig.TowerPrefab, _place);
            _currentTowerController = towerObject.GetComponent<TowerController>();
            _currentTowerController.TowerConfig = towerConfig;
            GameManager.Instance.ModifyGold(-_currentTowerController.TowerConfig.BuildCost);
            IsSelected = false;
        }

        private void OnDestroy()
        {
            if (EventManager.Instance != null)
            {
                EventManager.Instance?.Event_OnTouchSomething.RemoveListener(Handle_Event_OnTouchSomething);
                EventManager.Instance?.Event_OnBtnBuildTowerClicked.RemoveListener(Handle_Event_OnBtnBuildTowerClicked);
            }
            MapController.Event_TouchMap?.RemoveListener(Handle_Event_TouchMap);
        }

        public void OnBtnSellTowerClicked()
        {
            Debug.Log("OnBtnSellTowerClicked()");
            _currentTowerController.Sell();
            IsSelected = false;
        }

        public void OnBtnTowerSkillClicked()
        {
            Debug.Log("OnBtnTowerSkillClicked()");

            IsSelected = false;
        }

        public void OnBtnUpgradeTowerClicked()
        {
            Debug.Log("OnBtnUpgradeTowerClicked()");
            var nextTowerConfig = _currentTowerController.TowerConfig.UpgradedTower;

            // Check coi có build dc toewr hay không?
            bool enoughMoney = GameManager.Instance.UserData.Gold >= nextTowerConfig.BuildCost;
            if (!enoughMoney)
            {
                Debug.Log("Not enough money!");
                return;
            }

            if (nextTowerConfig != null)
            {
                Debug.Log($"<color=green>{nextTowerConfig.Level}</color>");
                _currentTowerController.TowerConfig = nextTowerConfig;
                GameManager.Instance.ModifyGold(- _currentTowerController.TowerConfig.BuildCost);
            }
            else
            {
                Debug.Log($"<color=green>Tower has max level!</color>");
            }
            IsSelected = false;
        }
    }
}
