using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    /// <summary>
    /// Detect clicked gameobject.
    /// </summary>
    public class TouchController : MonoBehaviour
    {
        private Camera _mainCam;

        public static event Action<int> Event_TouchObject;
        public static event Action Event_TouchUI;

        // Start is called before the first frame update
        void Start()
        {
            _mainCam = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            // Lấy vị trí touch/click trên screen space, convert sang world space. 
            var origin = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            var direction = Vector2.zero;

            // Detect touch/click. 0 => Chuột trái, tương đương touch lên màn hình.
            if (Input.GetMouseButtonDown(0))
            {
                // Selecting UI?
                if (EventSystem.current.currentSelectedGameObject != null)
                {
                    Event_TouchUI?.Invoke();
                    return;
                }

                // Physic2D.Raycast nó bắn 1 tia từ chỗ touch đến các gameobject trên màn hình.
                // Trả về 1 RaycastHit2D. 
                RaycastHit2D hit = Physics2D.Raycast(origin, direction);
                if (hit.collider != null)
                {
                    Debug.Log($"Hit {hit.collider.name}");
                    // GetInstanceID là 1 unique id do unity tự tạo ra gắn liền với object đó.
                    Event_TouchObject?.Invoke(hit.collider.gameObject.GetInstanceID());
                }
            }

            // Vẽ tia để test.
            transform.position = origin;
            Debug.DrawRay(transform.position, Vector3.forward, Color.green);
        }
    }
}
