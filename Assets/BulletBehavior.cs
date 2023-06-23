using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] protected int bulletLifeTime;
    SphereCollider B_Col;
    bool _CorouCheck = true;

    void Awake()
    {
        B_Col = GetComponent<SphereCollider>();
        Physics.IgnoreCollision(gameObject.GetComponent<SphereCollider>(), GetComponent<SphereCollider>());
    }

    void Update()
    {
        if(transform.parent == null && _CorouCheck)
        {
            StartCoroutine(RemoveBullet(bulletLifeTime));
            _CorouCheck = false;
        }
    }
    void OnCollisionEnter(Collision obj)
    {
        if(obj.gameObject.tag != "Player")
        {
            ObjectPooling.Takeobj(gameObject);
        }    
    }

    IEnumerator RemoveBullet(int timer)
    {
        Debug.Log("Bullet Restored!");

        yield return new WaitForSeconds(timer);
        _CorouCheck = true;
        ObjectPooling.Takeobj(gameObject);
    }

    
}
