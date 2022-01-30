using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public static GameObject Weapon = null; 
    public LayerMask mask;
    public bool active;
    private void Update()
    {      

        if(active)
        {

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 4f, mask))
            {
                if (hit.collider.GetComponent<InteractableObject>() != null)
                {
                    if (Input.GetButtonDown("Fire1"))//pickup new weapon
                    {
                        if (Weapon == null)
                        {
                            hit.collider.GetComponent<InteractableObject>().OnStartInteraction();
                        }
                        else
                        {
                            Weapon.GetComponent<InteractableObject>().OnEndInteraction();
                            hit.collider.GetComponent<InteractableObject>().OnStartInteraction();
                        }
                    }
                }
            }
            else
            {
                if (Weapon != null && Input.GetButtonDown("Fire1"))//attack
                {
                    Weapon.GetComponent<InteractableObject>().AttackInteraction();
                }
                if (Weapon != null && Input.GetButtonDown("Fire2"))//throw
                {
                    Weapon.GetComponent<InteractableObject>().OnSecondaryStartInteraction();
                }
            }
        }

    }
}