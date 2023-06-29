using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBaseBehavior : MonoBehaviour
{

    [SerializeField] protected int magSize;
    [SerializeField] protected float rateOfFire;
    [SerializeField] protected int bulletsPerShot;
    //[SerializeField] protected Weapon_SO W_Ref;
    [SerializeField] protected GameObject weaponDir;
    
    protected int bulletsLeft;
    protected bool _nextShotReady;
    

    Camera WeaponCam;
    GameObject weaponProjectile;
    Vector3 targetPoint;
    Vector3 weaponSpread;
    


    // Start is called before the first frame update
    protected virtual void Start()
    {
        bulletsLeft = magSize;
        _nextShotReady = true;
        WeaponCam = GameObject.Find("WeaponCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
            RaycastCheck();
        }
        
    }

    public virtual void Shoot()
    {
            if(bulletsLeft != 0 && _nextShotReady == true)
            {
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

    public virtual void RaycastCheck()
    {
        RaycastHit Hit;

        Ray ray = WeaponCam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));

        if (Physics.Raycast(ray, out Hit))
            targetPoint = Hit.point;
        else
            targetPoint = WeaponCam.transform.position + WeaponCam.transform.forward * 75;

        Vector3  dirWithoutSpread = targetPoint - weaponDir.transform.forward;

        weaponSpread = dirWithoutSpread;
        Debug.Log("Og Target = " + targetPoint);
        Debug.Log("Weapon Dir = " + weaponDir.transform.forward);
        Debug.Log("Dir no Spread = " + dirWithoutSpread);
        Debug.Log("Weapon Spread = " + weaponSpread);

        
        CallBullet();
        weaponProjectile.transform.forward = weaponSpread;
        weaponProjectile.transform.parent = null;

        Debug.Log("Bullet Direction = " +  weaponProjectile.transform.forward);

        weaponProjectile.GetComponent<Rigidbody>().AddForce(weaponSpread * 200, ForceMode.Force);
        //weaponProjectile.GetComponent<Rigidbody>().AddForce(weaponDir.transform.up * 0, ForceMode.Impulse);

    }

    public void CallBullet()
    {
        weaponProjectile = ObjectPooling.GiveObj(0);
        if (weaponProjectile != null)
        {
            weaponProjectile.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            weaponProjectile.SetActive(true);
        }

        
    }


}
