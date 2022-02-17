using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class TouchInputController : MonoBehaviour
    {
        //public EUnitType unitType;

        [SerializeField]
        private float _rayLength;
        [SerializeField]
        private LayerMask _selectableMask;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            var touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log($"Touch pos: {touchPos}");
            //var hitInfo = Physics2D.Raycast(touchPos, Vector3.forward, _rayLength);
            if (Input.GetMouseButtonDown(0))
            {
                var hitInfos = Physics2D.RaycastAll(touchPos, Vector3.forward, _rayLength, _selectableMask);
                for (int hitIndex = 0; hitIndex < hitInfos.Length; hitIndex++)
                {
                    var hit = hitInfos[hitIndex];
                    if (hit.collider == null) continue;
                    int hitMask = 1 << hit.collider.gameObject.layer;
                    bool isContainSelectableMask = ((_selectableMask & hitMask) > 0);
                    if (!isContainSelectableMask) continue;
                    bool isPointingUI = EventSystem.current.IsPointerOverGameObject();
                    if (isPointingUI) return;
                    Debug.Log($"Hit {hit.collider.name} {hit.collider.gameObject.GetInstanceID()}");
                    //Debug.DrawRay(touchPos, Vector3.forward * _rayLength, Color.blue);
                    EventManager.Instance.Event_OnTouchSomething?.Invoke(hit.collider.gameObject.GetInstanceID());
                    break;
                }
            }
        }

        //[ContextMenu("CheckUnitType")]
        //private void CheckUnitType()
        //{
        //    //switch (unitType)
        //    //{
        //    //    case EUnitType.None:
        //    //        break;
        //    //    case EUnitType.Warrior:
        //    //        Debug.Log("Type: MeleeType");
        //    //        break;
        //    //    case EUnitType.SpearMan:
        //    //        Debug.Log("Type: MeleeType");
        //    //        break;
        //    //    case EUnitType.Archer:
        //    //        Debug.Log("Type: RangeType");
        //    //        break;
        //    //    case EUnitType.Gunner:
        //    //        Debug.Log("Type: RangeType");
        //    //        break;
        //    //    default:
        //    //        break;
        //    //}
        //    //if (unitType == EUnitType.Warrior || unitType == EUnitType.SpearMan)
        //    //{

        //    //}

        //    if (EUnitType.MeleeType.HasFlag(unitType))
        //    {
        //        Debug.Log("Type: MeleeType");
        //    }
        //    else if (EUnitType.RangeType.HasFlag(unitType))
        //    {
        //        Debug.Log("Type: RangeType");
        //    }
        //}
    }

    [Flags]
    public enum EUnitType
    {
        None = 1 << 0,
        Warrior = 1 << 1,
        SpearMan = 1 << 2,
        Archer = 1 << 3,
        Gunner = 1 << 4,

        MeleeType = Warrior | SpearMan,
        RangeType = Archer | Gunner,
    }
}
