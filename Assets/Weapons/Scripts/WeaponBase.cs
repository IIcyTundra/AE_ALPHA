using PlayerSettings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.InputSystem;
using UnityEditor;

public class WeaponBase : MonoBehaviour
{
    [SerializeField] private protected WeaponInfo m_weaponInfo;
    [SerializeField] private InputReader _inputReader;

    private AudioSource m_audioSource;
    private float m_nextShotTime;
    private bool m_triggerReleasedSinceLastShot;
    private int m_shotsRemainingInBurst;
    private int m_projectilesRemainingInMag;

    private Vector3 m_recoilSmoothDampVelocity;
    private float m_recoilRotSmoothDampVelocity;
    private float m_recoilAngle;
    private void Start()
    {
        _inputReader.PrimaryFireStartedEvent += HandlePrimaryFireStart;
        _inputReader.PrimaryFireEndedEvent += HandlePrimaryFireEnded;
        m_shotsRemainingInBurst = m_weaponInfo.BurstCount;
        m_projectilesRemainingInMag = m_weaponInfo.ProjectilesPerMag;
        m_audioSource = GetComponent<AudioSource>();
    }

    private void LateUpdate()
    {
        //animate recoil
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Vector3.zero, ref m_recoilSmoothDampVelocity, m_weaponInfo.RecoilMoveSettleTime);

        m_recoilAngle = Mathf.SmoothDamp(m_recoilAngle, 0, ref m_recoilRotSmoothDampVelocity, m_weaponInfo.RecoilRotationSettleTime);
        transform.localEulerAngles = Vector3.left * m_recoilAngle;
    }

    protected void Shoot()
    {
        if ( Time.time > m_nextShotTime && m_projectilesRemainingInMag > 0)
        {
            // Firemodes
            if (m_weaponInfo.FiringMode == FiringTypes.Burst)
            {
                Debug.Log("pass 1");
                if (m_shotsRemainingInBurst == 0)
                {
                    return;
                }
                m_shotsRemainingInBurst--;
            }
            else if (m_weaponInfo.FiringMode == FiringTypes.Single)
            {
                Debug.Log("pass 2");
                if (!m_triggerReleasedSinceLastShot) return;
            }


            m_nextShotTime = Time.time + m_weaponInfo.MsBetweenShots / 1000f;
            

            

            // Initiate Recoil
            transform.localPosition -= Vector3.forward * Random.Range(m_weaponInfo.KickMinMax.x, m_weaponInfo.KickMinMax.y);
            m_recoilAngle += Random.Range(m_weaponInfo.RecoilAngleMinMax.x, m_weaponInfo.RecoilAngleMinMax.y);
            m_recoilAngle = Mathf.Clamp(m_recoilAngle, 0, 30);

            m_audioSource.PlayOneShot(m_weaponInfo.ShootAudio, 1);
            Debug.Log("pass 3");

        }

    }

    public void HandlePrimaryFireStart(float value)
    {
        Debug.Log("is shooting");
            Shoot();
            m_triggerReleasedSinceLastShot = false;
          
    }

    public void HandlePrimaryFireEnded() 
    {
        Debug.Log("stopped shooting");
        m_triggerReleasedSinceLastShot = true;
        m_shotsRemainingInBurst = m_weaponInfo.BurstCount;
    }


}
