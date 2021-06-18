using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class TouchController : MonoBehaviour
    {
        private Camera _mainCam;

        private void Awake()
        {
            _mainCam = Camera.main;
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.currentSelectedGameObject) return;

                RaycastHit2D hit = Physics2D.Raycast(_mainCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    // Do something with the object that was hit by the raycast.
                    Debug.Log($"Hit: {hit.collider.name}");

                }
            }
        }
    }
}
