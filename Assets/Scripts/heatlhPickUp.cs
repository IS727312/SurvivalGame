using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heatlhPickUp : MonoBehaviour
{
    //Player playerScript;
    public int healAmount;
    public GameObject effect;
    public float lifeTime;
    

    private void Start()
    {
       // playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Invoke("DestroyProjectile", lifeTime);
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement2>().heal(healAmount);
            Destroy(gameObject);
        }
    }
    private void DestroyProjectile()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
