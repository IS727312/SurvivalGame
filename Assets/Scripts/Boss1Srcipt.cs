using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1Srcipt : MonoBehaviour
{
    private GameObject[] patrolPoints;
    private GameObject player;
    public float speed, spawnOffset, chaseSpeed;
    public GameObject deathBoss;
    public int health;
    private float halfHealth;
    public Enemy[] enemies;
    public int damage;
    int randomPoint;
    public GameObject effect;
    private Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = FindObjectOfType<Slider>();
        halfHealth = health / 2;
        player = GameObject.FindGameObjectWithTag("Player");
        patrolPoints = GameObject.FindGameObjectsWithTag("patrolPoints");
        randomPoint = Random.Range(0, patrolPoints.Length);
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= halfHealth)
        {
            if (player != null)
            {

                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }

        }
        else
        {
            Patrol();
        }
    }


    void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[randomPoint].transform.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, patrolPoints[randomPoint].transform.position) < 0.1f)
        {
            randomPoint = Random.Range(0, patrolPoints.Length);
        }
    }
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthBar.value = health;
        if (health <= 0)
        {
            Instantiate(deathBoss, transform.position, Quaternion.identity);
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            healthBar.gameObject.SetActive(false);
        }
        Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }

    private void DestroyProjectile()
    {
        Destroy(deathBoss);
    }

}
