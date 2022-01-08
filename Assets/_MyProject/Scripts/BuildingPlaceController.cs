using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class BuildingPlaceController : MonoBehaviour
    {
        [SerializeField]
        private CanvasBuilding _canvasBuilding;
        [SerializeField]
        private Transform _place;
        [SerializeField]
        private TowerController _towerPrefab;

        // Start is called before the first frame update
        void Start()
        {
            _canvasBuilding.OnBuildingSelected.AddListener(Handle_OnBuildingSelected);
        }

        private void Handle_OnBuildingSelected(int buildingIndex)
        {
            Debug.Log($"Build {buildingIndex}");
            Instantiate(_towerPrefab, _place);
            _canvasBuilding.gameObject.SetActive(false);
        }

        private void OnMouseDown()
        {
            bool isPointingUI = EventSystem.current.IsPointerOverGameObject();
            if (isPointingUI) return;
            Debug.Log($"Clicked on {gameObject.transform}");
            _canvasBuilding.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            _canvasBuilding.OnBuildingSelected.RemoveListener(Handle_OnBuildingSelected);
        }
    }
}
