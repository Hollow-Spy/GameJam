using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickRand : MonoBehaviour
{
    [SerializeField] GameObject[] objs;
    void Start()
    {
        int rand = Random.Range(0, objs.Length);
        objs[rand].SetActive(true);
        
    }

   
}
