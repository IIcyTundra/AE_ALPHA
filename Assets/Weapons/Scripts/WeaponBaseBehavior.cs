using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class WeaponBaseBehavior : MonoBehaviour
{
    protected enum WeaponState{

        PrimaryFire,
        AlternateFire,
        Idle
    }
    [SerializeField] protected WeaponState _weaponState;
    [SerializeField] protected int magSize;
    
    // protected int bulletsLeft;
    // protected bool _nextShotReady;
    // protected int bulletsShot;



    // Start is called before the first frame update
    protected virtual void Start()
    {
        //bulletsLeft = magSize;
        // WeaponCam = GameObject.Find("WeaponCamera").GetComponent<Camera>();
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        WeaponInputHandler();
    }

    #region Weapon Function
    protected virtual void WeaponInputHandler()
    {
        if (Input.GetMouseButton(0))
        {
            _weaponState = WeaponState.PrimaryFire;
        }
        else if(Input.GetMouseButton(1))
        {
            _weaponState = WeaponState.AlternateFire;
        }
        else
        {
            _weaponState = WeaponState.Idle;
        }
        WeaponStateMachine();
    }

    protected virtual WeaponState WeaponStateMachine()
    {
        switch (_weaponState)
        {
            case WeaponState.PrimaryFire:
                PrimaryShoot();
                break;
            case WeaponState.AlternateFire:
                AlternateShoot();
                break;
            case WeaponState.Idle:
                break;
        }

        return _weaponState;
    }

    protected virtual void PrimaryShoot()
    {
        return;
    }
    
    protected virtual void AlternateShoot()
    {
        return;
    }
    #endregion

   

}
