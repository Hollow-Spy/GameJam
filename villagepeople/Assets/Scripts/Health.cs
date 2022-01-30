using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hp = 20;
    public void HealthFunction(float damage)
    {
        hp -= damage;
        Debug.Log("aaah");
        if (hp < 1)
        {
            Debug.Log("Death");
            Destroy(gameObject);
        }
    }
}
