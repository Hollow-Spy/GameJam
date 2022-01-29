using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Autodelete : MonoBehaviour
{
    Transform sun;

  

    [SerializeField] float Time;
    void Start()
    {
        Destroy(gameObject, Time);
    }

    private void Update()
    {
     

      
    }
}
