using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_Info", menuName = "Create Scriptable Object/Weapon_SO")]
public class Weapon_SO : ScriptableObject
{

    #region Variables

    //Weapon stats
    public Vector3 W_Spread = new Vector3(0.1f, 0.1f, 0.1f);
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

