using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour
{
    public aimJoystick weaponToPick;
    public float lifeTime;
    public GameObject effect, pickUpeffect;

    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Instantiate(pickUpeffect, transform.position, Quaternion.identity);
            collision.GetComponent<PlayerMovement2>().ChangeWeapon(weaponToPick);
            Destroy(gameObject);
        }
    }

    private void DestroyProjectile()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
