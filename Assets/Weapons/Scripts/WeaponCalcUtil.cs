using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCalcUtil : MonoBehaviour
{
    //Calculates the spread of the weapon
    public static Vector3 RandomSpread(Vector3 targetPoint, Camera viewPoint, float wSpreadStat)
    {
        Vector3  dirWithoutSpread = targetPoint - viewPoint.transform.position;

        float x = Random.Range(-wSpreadStat, wSpreadStat);
        float y = Random.Range(-wSpreadStat, wSpreadStat);
        float z = Random.Range(-wSpreadStat, wSpreadStat);

        Vector3 dirWithSpread = dirWithoutSpread + new Vector3(x, y, z);

        return dirWithSpread;
    }

    
}
