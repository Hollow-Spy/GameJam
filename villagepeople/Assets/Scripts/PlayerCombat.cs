using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform Eyes;

    [SerializeField]
    private float Cooldown;

    [SerializeField]
    private float Damage;

    private bool CanAttack = true;
    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        if (CanAttack)
        {
            RaycastHit Hit;
            if (Physics.Raycast(Eyes.transform.position, Eyes.transform.forward, out Hit, 5f))
            {
                if (Hit.collider.tag == "Enemy")
                {
                    Hit.collider.GetComponent<Health>().HealthFunction(Damage);
                }
            }
            CanAttack = false;
            StartCoroutine(AttackCooldown());
        }
    }
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(Cooldown);
        CanAttack = true;
    }


}
