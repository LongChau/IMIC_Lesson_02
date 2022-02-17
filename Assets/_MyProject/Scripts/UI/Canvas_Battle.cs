using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TowerDefense
{
    public class Canvas_Battle : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _txtGold;

        //void Start()
        //{
        //    // Có nguy cơ là userdata chưa được khởi tạo xong. 
        //    // Có thể tránh bằng việc chỉnh Execution Order trong Project setting.
        //    _txtGold.SetText($"Gold: {GameManager.Instance.UserData.Gold}");
        //    EventManager.Instance.Event_OnUserGoldChanged.AddListener(Handle_Event_OnUserGoldChanged);
        //}

        // Start is called before the first frame update
        IEnumerator Start()
        {
            // chờ đến khi nào data được khởi tạo xong.
            yield return new WaitUntil(() => GameManager.Instance.UserData.IsInitialized);
            _txtGold.SetText($"Gold: {GameManager.Instance.UserData.Gold}");
            EventManager.Instance.Event_OnUserGoldChanged.AddListener(Handle_Event_OnUserGoldChanged);
        }

        private void Handle_Event_OnUserGoldChanged()
        {
            _txtGold.SetText($"Gold: {GameManager.Instance.UserData.Gold}");
        }

        private void OnDestroy()
        {
            if (EventManager.Instance != null)
            {
                EventManager.Instance.Event_OnUserGoldChanged.RemoveListener(Handle_Event_OnUserGoldChanged);
            }
        }
    }
}
