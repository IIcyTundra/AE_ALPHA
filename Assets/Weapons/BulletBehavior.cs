using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] protected int bulletLifeTime;
    SphereCollider B_Col;
    //bool _CorouCheck = true;

    void Awake()
    {
        
        B_Col = GetComponent<SphereCollider>();
        //Physics.IgnoreCollision(gameObject.GetComponent<SphereCollider>(), GetComponent<SphereCollider>());
    }

    void Update()
    {
        if(isActiveAndEnabled)
        {
            StartCoroutine(RemoveBullet(bulletLifeTime));
        }
    }
    // void OnCollisionEnter(Collision obj)
    // {
    //     if(obj.gameObject.tag != "Player")
    //     {
    //         ObjectPooling.Takeobj(gameObject);
    //     }    
    // }

    IEnumerator RemoveBullet(int timer)
    {
        yield return new WaitForSeconds(timer);
        //_CorouCheck = true;
        ObjectPooling.Takeobj(gameObject);
    }

    
}
