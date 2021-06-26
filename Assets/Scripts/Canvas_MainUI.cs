using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TowerDefense
{
    public class Canvas_MainUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _txtGold;
        [SerializeField]
        private TextMeshProUGUI _txtLife;

        // Start is called before the first frame update
        void Start()
        {
            _txtLife.SetText($"Life: {GameController.Instance.LevelData.Life}");
            _txtGold.SetText($"Gold: {GameController.Instance.LevelData.Gold}");

            GameController.Instance.Event_UpdateGold += Handle_Event_UpdateGold;
            GameController.Instance.Event_UpdateLife += Handle_Event_UpdateLife;
        }

        private void Handle_Event_UpdateLife(int life)
        {
            _txtLife.SetText($"Life: {life}");
        }

        private void Handle_Event_UpdateGold(int gold)
        {
            _txtGold.SetText($"Gold: {gold}");
        }
    }
}
