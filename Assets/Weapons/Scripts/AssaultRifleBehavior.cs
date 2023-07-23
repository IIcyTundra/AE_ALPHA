using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AssaultRifleBehavior : WeaponBaseBehavior
{
    [SerializeField] LineRenderer GrendadeTrajectory;
    [SerializeField] private GameObject TrajectoryMarker;

    protected virtual void  Update()
    {
        base.Update();
        if (_weaponState != WeaponState.AlternateFire)
        {
            GrendadeTrajectory.enabled = false;
            TrajectoryMarker.SetActive(false);
        }
    }
    protected override void PrimaryShoot()
    {
        base.PrimaryShoot();
        Debug.Log("Yeah we're shooting here");
    }

    protected override void AlternateShoot()
    {
        base.AlternateShoot();
        Debug.Log("Now we're about to splooge");
        GrendadeTrajectory.enabled = true;
        TrajectoryMarker.SetActive(true);
    }

    
}
