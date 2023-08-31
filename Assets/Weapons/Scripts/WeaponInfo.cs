using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon Info", menuName = "Weapon System/Weapon Info")]
public class WeaponInfo : ScriptableObject 
{
    [Header("Weapon Stats")]
    public FiringTypes FiringMode;
    public int BurstCount;
    public float MsBetweenShots;
    public int ProjectilesPerMag;

    [Header("Projectile")]
    //public GameObject Projectile;

    [Header("Effects")]
    //public Transform Shell;
    //public Transform ShellEjection;
    public AudioClip ShootAudio;
    //public AudioClip ReloadAudio;

    [Header("Recoil")]
    public Vector2 KickMinMax = new Vector2(.05f, .2f);
    public Vector2 RecoilAngleMinMax = new Vector2(3, 5);
    public float RecoilMoveSettleTime = .1f;
    public float RecoilRotationSettleTime = .1f;

    
}
