using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class MapController : MonoBehaviour
    {
        public static UnityEvent Event_TouchMap = new UnityEvent();

        // Start is called before the first frame update
        void Start()
        {
            GameManager.Instance.MapInstanceId = gameObject.GetInstanceID();
        }
    }
}
