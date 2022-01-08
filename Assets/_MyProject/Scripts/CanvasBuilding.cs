using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense
{
    public class CanvasBuilding : MonoBehaviour
    {
        [NonSerialized]
        public UnityEvent<int> OnBuildingSelected = new UnityEvent<int>();

        // Start is called before the first frame update
        void Start()
        {
        
        }

        public void OnBtnBuildingClicked(int buildingIndex)
        {
            Debug.Log($"OnBtnBuildingClicked buildingIndex = {buildingIndex}");
            OnBuildingSelected?.Invoke(buildingIndex);
        }
    }
}
