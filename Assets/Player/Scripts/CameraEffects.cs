using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CameraEffects : MonoBehaviour
{
    [SerializeField] private float swayAmount = 1f;        // Amount of sway to apply
    [SerializeField] private float maxSwayAngle = 10f;     // Maximum sway angle in degrees
    [SerializeField] private float swaySmoothness = 5f;    // Smoothness of the sway motion
    private float targetSwayAngle;       // Target sway angle based on player movement
    [SerializeField] private VisualEffect SpeedLineVFX;
    public Transform cameraTransform;   // Reference to the camera's transform
    float SpeedThreshold = 20f;
    private MainCharacter.Q3PlayerController P_Values;

    //Return Player Speed
    private float PlayerSpeed {get { return P_Values.Speed;}}
    bool _isPlaying;

    private void Start() 
    {
        P_Values = GetComponent<MainCharacter.Q3PlayerController>();
        _isPlaying = true;
    }

    private void Update()
    {
        CamSway();
        SpeedLines();
        // Debug.Log(P_Values.Speed);
    }

    public void CamSway()
    {
        float HoriMovement = Input.GetAxisRaw("Horizontal");
        targetSwayAngle = Mathf.Clamp(HoriMovement * swayAmount, -maxSwayAngle, maxSwayAngle);
        
        // Apply sway rotation to the camera transform
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, -targetSwayAngle);
        cameraTransform.localRotation = Quaternion.Lerp(cameraTransform.localRotation, targetRotation, Time.deltaTime * swaySmoothness);
        //Debug.Log(cameraTransform.localRotation);
    }

    public void SpeedLines()
    {
        if(PlayerSpeed >= SpeedThreshold && !_isPlaying)
        {
            SpeedLineVFX.Play();
            _isPlaying = true;
            //Debug.Log(_isPlaying);
        }
        else if(_isPlaying && PlayerSpeed < SpeedThreshold)
        {
            SpeedLineVFX.Stop();
            _isPlaying = false;
            //Debug.Log(_isPlaying);
        }
    }
}
