using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : InteractableObject
{
    Rigidbody Object;
    bool CanAttack = true;
    bool flying;
    [SerializeField]
    private float Cooldown, Damage, Range, ThrowPower;
    private void Start()
    {
        Object = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (flying)
        {
            transform.Rotate(Time.deltaTime * 1000 ,0f,0f);
        }
    }
    public override void OnStartInteraction()
    {
        if (PlayerCombat.Weapon == null)//when the weapon is picked up its in the players hands
        {
            PlayerCombat.Weapon = gameObject;
            GetComponent<Collider>().enabled = false;
            Object.isKinematic = true;
        }
    }
    public override void AttackInteraction()
    {
        if (CanAttack)//the attacking mechanic
        {
            RaycastHit Hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out Hit, Range))
            {
                if (Hit.collider.tag == "Enemy")
                {
                    Debug.Log("Enemy hit");
                    Hit.collider.GetComponent<Health>().HealthFunction(Damage);
                }
            }
            CanAttack = false;
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown() // controlls the cooldown of the attack
    {
      yield return new WaitForSeconds(Cooldown);
      CanAttack = true;
    }

    public override void OnEndInteraction()//when the weapon is dropped by picking up a new weapon
    {
        GetComponent<Collider>().enabled = true;
        Object.isKinematic = false;
        PlayerCombat.Weapon.transform.parent = null;
        PlayerCombat.Weapon = null;
    }
    public override void OnSecondaryStartInteraction()//the throwing mechanic
    {
        GetComponent<Collider>().enabled = true;
        Object.isKinematic = false;
        PlayerCombat.Weapon.transform.parent = null;
        PlayerCombat.Weapon = null;
        Object.AddForce(transform.forward * ThrowPower, ForceMode.Impulse);
        flying = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        flying = false;
        if (collision.collider.tag == "Enemy")
        {
           collision.collider.GetComponent<Health>().HealthFunction(Damage);
           transform.parent = collision.transform;
           Object.isKinematic = true;
        }
    }
}
