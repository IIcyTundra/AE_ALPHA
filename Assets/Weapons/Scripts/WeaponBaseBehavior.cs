using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class WeaponBaseBehavior : MonoBehaviour
{

    [SerializeField] protected int magSize;
    [SerializeField] protected float rateOfFire;
    [SerializeField] protected int bulletsPerShot;
    [SerializeField] protected ShootConfigSC ShootConfig;
    [SerializeField] protected TrailConfigSC TrailConfig;
    
    protected int bulletsLeft;
    protected bool _nextShotReady;

    private ObjectPool <TrailRenderer> TrailPool;
    private ObjectPool <GameObject> Bullet;
    private float LastShootTime;
    private ParticleSystem ShootSystem; 
    


    // Start is called before the first frame update
    protected virtual void Start()
    {
        bulletsLeft = magSize;
        ShootSystem = GameObject.Find("MuzzleFlash01").GetComponent<ParticleSystem>();
        TrailPool = new ObjectPool<TrailRenderer>(CreateTrail);
        // WeaponCam = GameObject.Find("WeaponCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
        
    }

    protected virtual void Shoot()
    {
        if(bulletsLeft != 0)
        {
            if(Time.time > ShootConfig.FireRate + LastShootTime)
            {
                LastShootTime = Time.time;
                ShootSystem.Play();
                Vector3 shootDir = ShootSystem.transform.forward+ new Vector3(
                    Random.Range(
                        -ShootConfig.Spread.x,
                        ShootConfig.Spread.x
                    ), 
                    Random.Range(
                        -ShootConfig.Spread.y,
                        ShootConfig.Spread.y
                    ), 
                    Random.Range(
                        -ShootConfig.Spread.z,
                        ShootConfig.Spread.z
                    )
                );

                shootDir.Normalize();

                if(Physics.Raycast(ShootSystem.transform.position,shootDir, out RaycastHit hit, float.MaxValue, ShootConfig.HitMask))
                {
                    StartCoroutine(
                        PlayTrail(
                            ShootSystem.transform.position,
                            hit.point,
                            hit
                        )
                    );
                }
                else
                {
                    StartCoroutine(
                        PlayTrail(
                            ShootSystem.transform.position,
                            ShootSystem.transform.position + (shootDir * TrailConfig.MissDistance),
                            new RaycastHit()
                        )
                    );
                }
            
            }
            bulletsLeft--;
            Debug.Log(bulletsLeft);
        }
    }

    protected TrailRenderer CreateTrail()
    {
        GameObject instance = new GameObject("Bullet Trail");
        TrailRenderer trail = instance.AddComponent<TrailRenderer>();
        trail.colorGradient = TrailConfig.Color;
        trail.material = TrailConfig.Material;
        trail.widthCurve = TrailConfig.WidthCurve;
        trail.time = TrailConfig.Duration;
        trail.minVertexDistance = TrailConfig.MinVertexDistance;
        
        trail.emitting = false;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        return trail;
    }

    protected IEnumerator PlayTrail(Vector3 StartPoint, Vector3 EndPoint, RaycastHit Hit)
    {
        TrailRenderer instance = TrailPool.Get();
        instance.gameObject.SetActive(true);
        instance.transform.position = StartPoint;
        yield return null; //Avoid position carry over

        instance.emitting = true;

        float distance = Vector3.Distance(StartPoint, EndPoint);
        float remainingDistance = distance;
        while(remainingDistance > 0)
        {
            instance.transform.position = Vector3.Lerp(
                StartPoint,
                EndPoint, 
                Mathf.Clamp01(1 - (remainingDistance / distance))
            );
            remainingDistance -= TrailConfig.SimulationSpeed * Time.deltaTime;

            yield return null;
        }

        instance.transform.position = EndPoint;

        yield return new WaitForSeconds(TrailConfig.Duration);
        yield return null;
        instance.emitting = false;
        instance.gameObject.SetActive(false);
        TrailPool.Release(instance);

    }


}
