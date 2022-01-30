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

    [SerializeField]
    bool FlyingRotate, DumbAxe,Dagger;

    [SerializeField]
    Vector3 HandTransform,HandRotation,HandScale;
  
    Transform Hand;
    [SerializeField] Animator animator;
    [SerializeField] GameObject SwingSFX, ThrowSFX;

    TrailRenderer trail;

    private void Start()
    {
        Object = GetComponent<Rigidbody>();
        Hand = GameObject.Find("Hand").transform;

        animator = Hand.GetComponentInParent<Animator>();

        trail = GetComponentInChildren<TrailRenderer>();
        trail.emitting = false;
    }

 
    private void Update()
    {
        if (flying)
        {
            trail.emitting = true;

            if (FlyingRotate)
            {
                transform.Rotate(Time.deltaTime * 1000, 0f, 0f);
            }
            else if (DumbAxe)
            {
                transform.Rotate(0f, -Time.deltaTime * 1000, 0f);
            }            

        }
        else
        {
            trail.emitting = false;
        }
    }
    public override void OnStartInteraction()
    {
        if (PlayerCombat.Weapon == null)//when the weapon is picked up its in the players hands
        {
            PlayerCombat.Weapon = gameObject;
            GetComponent<Collider>().enabled = false;
            Object.isKinematic = true;
            Hand.localPosition = HandTransform;
            Hand.localRotation = Quaternion.Euler(HandRotation);
            Hand.localScale = HandScale;
            transform.SetParent(Hand);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            transform.localScale = Vector3.one;
            
        }
    }
    public override void AttackInteraction()
    {
        if (CanAttack)//the attacking mechanic
        {
            animator.SetTrigger("swing");   
            Instantiate(SwingSFX,transform.position,Quaternion.identity);
            RaycastHit Hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out Hit, Range))
            {
                if (Hit.collider.tag == "Enemy" || Hit.collider.tag == "NPC")
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

        Instantiate(ThrowSFX, transform.position, Quaternion.identity);

        if (DumbAxe || FlyingRotate)
        {
            Object.AddForce(transform.forward * ThrowPower, ForceMode.Impulse);
        }
        else if(Dagger)
        {
            Object.AddForce(transform.up * ThrowPower, ForceMode.Impulse);
            transform.eulerAngles = new Vector3(180f, 0f, 0f);
        }
        else
        {
            Object.AddForce(-transform.forward * ThrowPower, ForceMode.Impulse);
        }

        flying = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        flying = false;
        if (collision.collider.tag == "Enemy" || collision.transform.CompareTag("NPC"))
        {

           collision.collider.GetComponent<Health>().HealthFunction(Damage);
           

            if(collision.collider.gameObject)
            {
                transform.parent = collision.transform;
                Object.isKinematic = true;
            }
          
        }
    }
}
