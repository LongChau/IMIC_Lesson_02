using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class BuildPlaceController : MonoBehaviour
    {
        [SerializeField]
        private TowerUIController _towerUIController;

        private void Awake()
        {
            // Hide tower UI by default!
            _towerUIController.gameObject.SetActive(false);

        }

        // Start is called before the first frame update
        void Start()
        {
            // Listen to event TowerUIController.
            TowerUIController.Event_TowerSelected += Handler_Event_TowerSelected;
        }

        private void Handler_Event_TowerSelected(int towerIndex)
        {
            Debug.Log($"Build tower {towerIndex}");

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnMouseDown()
        {
            Debug.Log($"BuildPlaceController.OnMouseDown() {gameObject.name}");
            // Toggle show/hide tower UI.
            bool toggleActive = !_towerUIController.gameObject.activeSelf;
            _towerUIController.gameObject.SetActive(toggleActive);
        }

        private void OnDestroy()
        {
            TowerUIController.Event_TowerSelected -= Handler_Event_TowerSelected;
        }
    }
}
