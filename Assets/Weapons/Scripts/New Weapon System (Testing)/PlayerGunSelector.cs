using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerGunSelector : MonoBehaviour
{
    [SerializeField]
    private GunType Gun;
    [SerializeField]
    private Transform GunParent;
    [SerializeField]
    private List<GunSC> Guns;

    [Space]
    [Header("Runtime Filled")]
    public GunSC ActiveGun;

    private void Start()
    {
        GunSC gun = Guns.Find(gun => gun.Type == Gun);
        if (gun == null)
        {
            Debug.LogError($"No GSO found for GunType: {gun}");
            return;
        }

        ActiveGun = gun;
        gun.Spawn(GunParent, this);
    }

    
}
