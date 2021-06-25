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
        private RectTransform _pnlTower_Archer_Menu;
        [SerializeField]
        private RectTransform _pnlTower_Soldier_Menu;
        [SerializeField]
        private RectTransform _pnlTower_Mage_Menu;
        [SerializeField]
        private RectTransform _pnlTower_Artilery_Menu;
        [SerializeField]
        private RectTransform _pnlTower_Catapult_Menu;

        public event Action<int> Event_TowerSelected;
        public event Action Event_TowerSold;

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

        public void Hide()
        {
            gameObject.SetActive(false);
            HideAllMenus();
        }

        public void HideAllMenus()
        {
            _pnlTowerSelect.gameObject.SetActive(false);
            _pnlTower_Archer_Menu.gameObject.SetActive(false);
            _pnlTower_Soldier_Menu.gameObject.SetActive(false);
            _pnlTower_Mage_Menu.gameObject.SetActive(false);
            _pnlTower_Artilery_Menu.gameObject.SetActive(false);
            _pnlTower_Catapult_Menu.gameObject.SetActive(false);
        }

        public void ShowBuildTowerMenu()
        {
            _pnlTowerSelect.gameObject.SetActive(true);
        }

        public void ShowTowerUI(ETowerType towerType)
        {
            // Hide all menus.
            HideAllMenus();
            // Show UI.
            switch (towerType)
            {
                case ETowerType.None:
                    ShowBuildTowerMenu();
                    break;
                case ETowerType.Archer:
                    ShowArcherUI();
                    break;
                case ETowerType.Soldier:
                    ShowSoldierUI();
                    break;
                case ETowerType.Mage:
                    ShowMageUI();
                    break;
                case ETowerType.Artilery:
                    ShowArtileryUI();
                    break;
                case ETowerType.Ballista:
                    ShowBallistaUI();
                    break;
            }
        }

        public void ShowArcherUI()
        {
            _pnlTower_Archer_Menu.gameObject.SetActive(true);
        }

        public void ShowSoldierUI()
        {
            _pnlTower_Soldier_Menu.gameObject.SetActive(true);
        }

        public void ShowMageUI()
        {
            _pnlTower_Mage_Menu.gameObject.SetActive(true);
        }

        public void ShowArtileryUI()
        {
            _pnlTower_Artilery_Menu.gameObject.SetActive(true);
        }

        public void ShowBallistaUI()
        {
            _pnlTower_Catapult_Menu.gameObject.SetActive(true);
        }

        public void OnBtnSellClicked()
        {
            Event_TowerSold?.Invoke();
        }
    }
}
