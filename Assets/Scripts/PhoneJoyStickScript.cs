using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneJoyStickScript : MonoBehaviour
{
    public Transform player;
    private Weapon weapon;
    public float speed = 5.0f;
    bool touchStart = false;
    Vector2 pointA;
    Vector2 pointB;
    Vector2 pointC;
    Vector2 pointD;
    Vector2 check;

    public Transform inCircle;
    public Transform inCircle2;
    public Transform outCircle;
    public Transform outCircle2;
    public float minX, maxX;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GameObject.FindObjectOfType<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!touchStart)
        {
            check = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0)) * -1;
        }

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0));
            inCircle.transform.position = pointA * -1;
            outCircle.transform.position = pointA;
            inCircle.GetComponent<SpriteRenderer>().enabled = true;
            outCircle.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0));
        }

        if (Input.GetTouch(1).phase == TouchPhase.Began)
        {
            pointC = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(1).position.x, Input.GetTouch(1).position.y, 0));
            inCircle2.transform.position = pointC * -1;
            outCircle2.transform.position = pointC;
            inCircle2.GetComponent<SpriteRenderer>().enabled = true;
            outCircle2.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetTouch(1).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Stationary)
        {
            touchStart = true;
            pointD = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(1).position.x, Input.GetTouch(1).position.y, 1));
        }
        else
        {
            touchStart = false;
        }
    }

    private void FixedUpdate()
    {
        if (touchStart && check.x > 0)
        {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            MovePlayer(direction * -1);
            inCircle.transform.position = new Vector2(outCircle.position.x + direction.x, outCircle.position.y + direction.y);
        }
        else if (touchStart && check.x < 0)
        {

            Vector2 offset = pointD - pointC;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            shootDirection(direction);
            inCircle2.transform.position = new Vector2(outCircle2.position.x + direction.x, outCircle2.position.y + direction.y);
        }
        else
        {
            inCircle.GetComponent<SpriteRenderer>().enabled = false;
            outCircle.GetComponent<SpriteRenderer>().enabled = false;
            inCircle2.GetComponent<SpriteRenderer>().enabled = false;
            outCircle2.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void MovePlayer(Vector2 direction)
    {
        player.Translate(direction * speed * Time.deltaTime * -1);

    }

    void shootDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        weapon.transform.rotation = rotation;
    }
}
