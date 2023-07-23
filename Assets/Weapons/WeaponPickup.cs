using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Weapon_Handler WH_Ref;
    public GameObject SprObj;
    string scr;

    void Start()
    {
        WH_Ref = GameObject.Find("WeaponHolder").GetComponent<Weapon_Handler>();
        scr = "SpriteBillboard";
    }

    private void OnTriggerEnter(Collider Player)
    {
        Debug.Log(gameObject);
        if(Player.gameObject.tag == "Player")
        {
            (SprObj. GetComponent(scr) as MonoBehaviour).enabled = false;
            WH_Ref.AddWeapon(gameObject);
        }
    }
}
