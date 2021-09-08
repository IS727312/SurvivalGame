using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;

    [HideInInspector]
    public Transform player;

    public float speed;
    public float timeBetweenAttacks;
    public int damage;

    public int pickUpChance;
    public GameObject[] pickUps;

    public int healthPickUpChance;
    public GameObject healthPickUp;

    public GameObject deathEffect;
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if(health <= 0)
        {
            int randomNumber = Random.Range(0, 101);
            if(randomNumber < pickUpChance)
            {
                GameObject randomPickUp = pickUps[Random.Range(0, pickUps.Length)];
                Instantiate(randomPickUp, transform.position, transform.rotation);
            }

            int randomHealthNumber = Random.Range(0, 101);
            if(randomHealthNumber < healthPickUpChance)
            {
                Instantiate(healthPickUp, transform.position, transform.rotation);
            }

            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);


        }
    }
}
