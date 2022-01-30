using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hp = 20;
    [SerializeField] GameObject blood;
  
    public void HealthFunction(float damage)
    {
        hp -= damage;
        Instantiate(blood, transform.position, Quaternion.identity);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<HeadBio>().Shake(.0001f, .03f, 0.01f);
        Debug.Log("aaah");
        if (hp < 1)
        {
            FindObjectOfType<TrustLevel>().SilentAction(-3);
            FindObjectOfType<HungerValue>().GainHunger(15);
            Debug.Log("Death");
            Destroy(gameObject);
        }
    }
}
