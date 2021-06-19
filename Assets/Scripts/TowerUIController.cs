using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TowerUIController : MonoBehaviour
    {
        public event Action<int> Event_TowerSelected;

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
    }
}
