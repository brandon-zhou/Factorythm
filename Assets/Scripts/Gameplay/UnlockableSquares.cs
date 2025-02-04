using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LockedRoom))]
public class UnlockableSquares : MonoBehaviour
{
    public bool isActive;
    // public GameObject square;
    private SpriteRenderer _mySR;
    private BoxCollider2D _myCollider;

    [NonSerialized] public Machine[] Machines;
    [NonSerialized] public UnlockConveyorInner[] ConveyorInners;

    private LockedRoom _lockedRoom;

    public GameObject MachineBluePrintCreator;
    private BluePrintCreator _bluePrintCreator;

    void Awake() {
        _mySR = gameObject.GetComponent<SpriteRenderer>();
        _myCollider = GetComponent<BoxCollider2D>();
        Machines = GetComponentsInChildren<Machine>();
        ConveyorInners = GetComponentsInChildren<UnlockConveyorInner>();
        _lockedRoom = GetComponent<LockedRoom>();
        if (MachineBluePrintCreator != null) {
            _bluePrintCreator = MachineBluePrintCreator.GetComponent<BluePrintCreator>();
        }
    }
    
    void Update() {
        _mySR.enabled = isActive;
        _myCollider.enabled = isActive;

        if (isActive) {
            bool done = true;

            foreach (var c in ConveyorInners) {
                if (!c.Done) {
                    done = false;
                    break;
                }
            }
            if (done) {
                _lockedRoom.enabled = false;
                _mySR.enabled = false;
            
                foreach (var c in ConveyorInners) {
                    c.Unlock();
                }

                isActive = false;
                GetComponentInChildren<Canvas>().gameObject.SetActive(false);
                if (_bluePrintCreator != null) {
                    _bluePrintCreator.Unlock();
                }
            }
        }
    }
}
