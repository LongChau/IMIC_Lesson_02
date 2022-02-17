using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense
{
    /// <summary>
    /// Using singleton pattern.
    /// This is lazy singleton pattern.
    /// </summary>
    public class EventManager : MonoBehaviour
    {
        [SerializeField]
        private bool _isDontDestroyOnLoad;

        private static EventManager _instance;

        public static EventManager Instance
        {
            get
            {
                //if (_instance == null)
                //{
                //    _instance = FindObjectOfType<EventManager>();
                //}

                //if (_instance == null)
                //{
                //    GameObject newGameObj = new GameObject();
                //    _instance = newGameObj.AddComponent<EventManager>();
                //}

                return _instance;
            }
        }

        [NonSerialized] public UnityEvent<GameDefine.ETowerType> Event_OnBtnBuildTowerClicked = new UnityEvent<GameDefine.ETowerType>();
        [NonSerialized] public UnityEvent<long> Event_OnTouchSomething = new UnityEvent<long>();
        [NonSerialized] public UnityEvent Event_OnUserGoldChanged = new UnityEvent();

        private void Awake()
        {
            gameObject.name = "EventManager";

            if (_instance != null)
            {
                Destroy(gameObject);
                _instance = null;
            }
            else
            {
                _instance = this;
            }

            if (_isDontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
    }
}
