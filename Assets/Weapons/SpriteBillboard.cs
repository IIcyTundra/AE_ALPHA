using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{

    Camera PlayerOrbit;
    // Start is called before the first frame update
    void Start()
    {
        PlayerOrbit = GameObject.Find("PlayerCam").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerOrbit.transform);
        transform.Rotate(0,180,0);
    }
}
