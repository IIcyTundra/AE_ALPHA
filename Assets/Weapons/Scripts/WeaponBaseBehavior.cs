using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBaseBehavior : MonoBehaviour
{
    [SerializeField] protected int magSize;
    [SerializeField] protected float rateOfFire;
    [SerializeField] protected int bulletsPerShot;
    [SerializeField] protected Weapon_SO W_Ref;
    
    protected int bulletsLeft;
    protected bool _nextShotReady;
    

    Camera WeaponCam;
    GameObject weaponProjectile;
    RaycastHit Hit;
    Vector3 weaponSpread;
    Vector3 targetPoint;
    


    // Start is called before the first frame update
    protected virtual void Start()
    {
        bulletsLeft = magSize;
        _nextShotReady = true;
        WeaponCam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        RaycastTrack();
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
            
        }
        
    }

    public virtual void Shoot()
    {
            if(bulletsLeft != 0 && _nextShotReady == true)
            {
                CallBullet();
                ///Instantiate(W_Ref.ImpactEffect.GetComponent<ParticleSystem>(), Hit.transform.position, Hit.transform.rotation);
                bulletsLeft--;
                _nextShotReady = false;
                Invoke("NextShot", rateOfFire);
                Debug.Log(bulletsLeft);
            }
    }

    public virtual void NextShot()
    {
        _nextShotReady = true;
        Debug.Log("Next shot ready!");
    }

    public virtual void RaycastTrack()
    {
        
        if (Physics.Raycast(WeaponCam.transform.position, WeaponCam.transform.forward, out Hit))
            targetPoint = Hit.point;
        else
            targetPoint = WeaponCam.transform.position + WeaponCam.transform.forward * 75;

        weaponSpread = WeaponCalcUtil.RandomSpread(targetPoint,WeaponCam,W_Ref.W_Spread);

    }

    public void CallBullet()
    {
        weaponProjectile = ObjectPooling.GiveObj(0);
        if (weaponProjectile != null)
        {
            weaponProjectile.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            weaponProjectile.SetActive(true);
        }

        weaponProjectile.transform.forward = weaponSpread.normalized;
        weaponProjectile.transform.parent = null;

        weaponProjectile.GetComponent<Rigidbody>().velocity = weaponSpread.normalized * W_Ref.ShootForce;
        weaponProjectile.GetComponent<Rigidbody>().AddForce(WeaponCam.transform.up * W_Ref.UpwardForce, ForceMode.Impulse);

    }


}
