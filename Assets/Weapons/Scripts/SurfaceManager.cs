using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceManager : MonoBehaviour
{
    private static SurfaceManager _instance;

    public static SurfaceManager Instance
    {
        get{return _instance;}
        private set{_instance = value;}
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Aye turn that other SM off!");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // [SerializeField]
    // private List<SurfaceType> Surface = new List<SurfaceType>();
    [SerializeField]
    private int DefaultPoolSizes = 10;
    [SerializeField]
    private SurfaceEffector2D DefaultSurface;

}
