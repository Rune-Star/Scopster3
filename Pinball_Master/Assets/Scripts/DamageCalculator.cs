using System.Collections;
using UnityEngine;

public class DamageCalculator
{
    float _curTime;
    
    public static float CalculateDamage()
    {

        return 1;
    }

    IEnumerator SetTimer(float invulnerableTerm)
    {
        _curTime = 0;

        while (_curTime<invulnerableTerm) {
            _curTime += Time.deltaTime;
        }
        
        yield break;
    }

    public static float OnDamage(ref float damage)
    {
        return 1;
    }
}