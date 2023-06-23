using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Functions : MonoBehaviour
{
    public Weapon_SO W_Ref;
    [HideInInspector] public Rigidbody PlayerRb;
    [HideInInspector] public Camera PlayerCam;
    [HideInInspector] public Transform GunDir;
    GameObject W_Bullet;
    GameObject W_MuzzleFlash;
    bool RTS, reloading; //Checks if we're ready to shoot
    int BulletsLeft, BulletsShot, CurrAmmo, CurrReserves; 
    public bool allowInvoke = true;
    bool shooting;
    //private TextMeshProUGUI PlayerInfo;
    AudioSource ShotSound;
    Vector2 MPos;
    
    
    private void Awake()
    {
        ShotSound = GetComponent<AudioSource>();
        PlayerCam = GameObject.Find("PlayerCam").GetComponent<Camera>();
        GunDir = GameObject.Find("GunDir").GetComponent<Transform>();
        PlayerRb = GetComponentInParent<Rigidbody>();
        //PlayerInfo = GameObject.Find("Ammo Text").GetComponent<TextMeshProUGUI>();
        BulletsLeft = W_Ref.W_Ammo_Capacity;
        RTS = true;
    }

    private void Update()
    {
        MyInput();
        //PlayerInfo.SetText((BulletsLeft/W_Ref.W_BPT).ToString());
        MPos = Input.mousePosition;
        //Debug.Log(GunDir.transform.rotation.z);
        //Debug.Log(PlayerCam.transform.rotation.z);

        
    }

    private void MyInput()
    {
        
        if (W_Ref.FullAutoToggle) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (RTS && shooting && BulletsLeft > 0)
        {
            BulletsShot = 0;

            Shoot();
        //     if(W_Ref.shootSounds != null && W_Ref.shootSounds.Length > 1)
        //     {
        //         int rSound = Random.Range(0,W_Ref.shootSounds.Length);
        //         ShotSound.clip = W_Ref.shootSounds[rSound];
        //         ShotSound.Play();
        //     }
        //     else{
        //         ShotSound.clip = W_Ref.shootSounds[0];
        //         ShotSound.Play();
        //     }
        
        // ShotSound.Play();
        //     Debug.Log("Shot Fired");
        }
    }

    public void Shoot()
    {

        RTS = false;
        
        RaycastHit hit;
        
        Vector3 targetPoint;
        if (Physics.Raycast(PlayerCam.transform.position, PlayerCam.transform.forward, out hit))
            targetPoint = hit.point;
        else
            targetPoint = PlayerCam.transform.position + PlayerCam.transform.forward * 75;

        Debug.Log(hit.point);
        Vector3 directionWithoutSpread = targetPoint - GunDir.position;

        
        float x = Random.Range(-W_Ref.W_Spread, W_Ref.W_Spread);
        float y = Random.Range(-W_Ref.W_Spread, W_Ref.W_Spread);
        float z = Random.Range(-W_Ref.W_Spread, W_Ref.W_Spread);
        

        
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, z);

        
        CallBullet();
        W_Bullet.transform.SetPositionAndRotation(PlayerCam.transform.position, Quaternion.identity);
        W_Bullet.transform.forward = directionWithSpread.normalized;
        W_Bullet.transform.parent = null;
        
        W_Bullet.GetComponent<Rigidbody>().velocity = directionWithSpread.normalized * W_Ref.ShootForce;
        W_Bullet.GetComponent<Rigidbody>().AddForce(PlayerCam.transform.up * W_Ref.UpwardForce, ForceMode.Impulse);
        
        

        //Debug.Log(directionWithoutSpread.normalized);
        
        // if (W_MuzzleFlash != null)
        //     W_MuzzleFlash.transform.SetPositionAndRotation(GunDir.position, Quaternion.identity);
        //     W_MuzzleFlash.GetComponent<ParticleSystem>().Play();
        //     if(W_MuzzleFlash.GetComponent<ParticleSystem>().IsAlive())
        //         BulletBehavior.Takeobj(W_MuzzleFlash);


        

        BulletsLeft--;
        BulletsShot++;

        

        if (allowInvoke)
        {
            Invoke("ResetShot", W_Ref.W_TBShooting);
            allowInvoke = false;
        }

        
        if (BulletsShot < W_Ref.W_BPT && BulletsLeft > 0)
            Invoke("Shoot", W_Ref.W_TBShots);
  
        
    }

    private void ResetShot()
    {
        RTS = true;
        allowInvoke = true;
    }

    public void CallBullet()
    {
        W_Bullet = ObjectPooling.GiveObj(0);
        if (W_Bullet != null)
        {
            W_Bullet.transform.SetPositionAndRotation(transform.position, transform.rotation);
            W_Bullet.SetActive(true);
            //W_Bullet.GetComponent<BulletMechanics>().ApplyEffect(W_Ref.W_DMG, W_Ref.W_Effect);
        }

    }
}
