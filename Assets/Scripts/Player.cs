using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public int   health;
    private Rigidbody2D rb;
    private Vector2 moveAmount;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    SceneTransitions sceneTransitions;
    Animator cameraAnim;
    void Start()
    {
        cameraAnim = Camera.main.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sceneTransitions = FindObjectOfType<SceneTransitions>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;
        if(health == 1)
        {
            cameraAnim.SetTrigger("Shake");
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
 
        UpdateHeatlhUI(health);

        if (health <= 0)
        {
            Destroy(this.gameObject);
            sceneTransitions.loadScene("Lost");
        }
    }

    public void ChangeWeapon(Weapon weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }

    void UpdateHeatlhUI(int currentHealth)
    {
        for (int i = 0; i<hearts.Length; i++)
        {
            if(i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void heal(int healAmount)
    {
        if(health + healAmount >= 5)
        {
            health = 5;
        }
        else
        {
            health += healAmount;
        }
        UpdateHeatlhUI(health);
       
    }
}
