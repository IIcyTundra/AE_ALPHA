using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_Info", menuName = "Create Scriptable Object/Weapon_SO")]
public class Weapon_SO : ScriptableObject
{

    #region Variables

    //Weapon Image
    public Sprite W_Sprite;

    //Weapon Audio
    public AudioClip[] shootSounds, reloadSounds;

    //All related prefabs to the Weapon
    public GameObject W_Obj;


    //String info (Name and Elemental Type)--
    public string W_Effect, W_Name; 

    //ID in weapon dictionary
    public int W_ID; 

    //Weapon stats
    [Range(0.0f, 1.0f)] public float W_Spread;
    public float W_Range, W_DMG, W_Reload_Speed;
    public float W_TBShots, W_TBShooting; 
    public int W_BPT; 
    public int Ammo_Added;
    public int W_Ammo_Mag, W_Ammo_Capacity; //Max ammo in mag and the total ammount of ammo it can carry
    
    public bool FullAutoToggle; //If we can full auto or not
    public float ShootForce, UpwardForce; //For projectile rounds;
    public float RecoilForce;


    #endregion
}
