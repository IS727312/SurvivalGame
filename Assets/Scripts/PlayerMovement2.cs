using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerMovement2 : MonoBehaviour
{
    private Rigidbody2D myBody;
    public float speed = 25f;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private FixedJoystick joystick;
    public int health;
    SceneTransitions sceneTransitions;

    private void Awake()
    {
        myBody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        joystick = GameObject.FindWithTag("Joystick").GetComponent<FixedJoystick>();
        sceneTransitions = FindObjectOfType<SceneTransitions>();
    }

    private void Update()
    {
        myBody.velocity = new Vector2(joystick.Horizontal * speed, joystick.Vertical * speed);
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

    public void ChangeWeapon(aimJoystick weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }

    void UpdateHeatlhUI(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
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
        if (health + healAmount >= 5)
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
