using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    float hp = 20;
    public void HealthFunction(float damage)
    {
        hp -= damage;

        if (hp < 1)
        {
            Debug.Log("Death");
        }
    }
}