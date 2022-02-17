using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class BtnTower : MonoBehaviour
    {
        [SerializeField]
        private Button _btn;
        [SerializeField]
        private GameDefine.ETowerType _towerType;

        public Button Btn { get => _btn; }
        public GameDefine.ETowerType TowerType { get => _towerType; }

        private void OnValidate()
        {
            _btn = GetComponent<Button>();
        }

        // Start is called before the first frame update
        void Start()
        {
            Btn.onClick.AddListener(OnBtnBuildingClicked);
        }

        public void OnBtnBuildingClicked()
        {
            Debug.Log($"OnBtnBuildingClicked() {_towerType}");
            EventManager.Instance.Event_OnBtnBuildTowerClicked?.Invoke(_towerType);
        }
    }
}
