using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TowerUIController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _pnlTowerSelect;
        [SerializeField]
        private RectTransform _pnlTower_Archer;

        public event Action<int> Event_TowerSelected;
        public event Action Event_TowerSold;
        public event Action Event_TowerUpgrade;

        // Start is called before the first frame update
        void Start()
        {

        }

        public void OnBtnTowerClicked(int towerIndex)
        {
            Debug.Log($"TowerUIController.OnBtnTowerClicked({towerIndex})");
            // Raise event tower select with param towerIndex => BuildPlaceController
            Event_TowerSelected?.Invoke(towerIndex);
        }

        public void HideAllMenus()
        {
            _pnlTowerSelect.gameObject.SetActive(false);
            _pnlTower_Archer.gameObject.SetActive(false);
        }

        public void ShowTowerUI(ETowerType towerType)
        {
            // Hide all UIs
            HideAllMenus();
            // Turn on tower type UI...
            switch (towerType)
            {
                case ETowerType.None:
                    _pnlTowerSelect.gameObject.SetActive(true);
                    break;
                case ETowerType.Archer:
                    _pnlTower_Archer.gameObject.SetActive(true);
                    break;
                case ETowerType.Soldier:
                    break;
                case ETowerType.Mage:
                    break;
                case ETowerType.Artilery:
                    break;
                case ETowerType.Ballista:
                    break;
                default:
                    break;
            }
        }

        public void OnBtnSkillClicked()
        {
            Debug.Log("OnBtnSkillClicked()");

        }

        public void OnBtnUpgradeClicked()
        {
            Debug.Log("OnBtnUpgradeClicked()");
            Event_TowerUpgrade?.Invoke();
        }

        public void OnBtnSellClicked()
        {
            Debug.Log("OnBtnSellClicked()");
            Event_TowerSold?.Invoke();
        }
    }
}
