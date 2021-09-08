using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick2 : MonoBehaviour
{
    public Transform player;
    private Weapon weapon;
    public float speed = 5.0f;
    bool touchStart = false;
    Vector2 pointA;
    Vector2 pointB;
    Vector2 check;

    public Transform inCircle;
    public Transform outCircle;
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
            check = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)) * -1;
        }

        if (Input.GetMouseButtonDown(0))
        {
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            inCircle.transform.position = pointA * -1;
            outCircle.transform.position = pointA;
            inCircle.GetComponent<SpriteRenderer>().enabled = true;
            outCircle.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetMouseButton(0))
        {
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        }
        else
        {
            touchStart = false;
        }
    }

    private void FixedUpdate()
    {

       if (touchStart && check.x < 0)
        {

            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            shootDirection(direction);
            inCircle.transform.position = new Vector2(outCircle.position.x + direction.x, outCircle.position.y + direction.y);
        }
        else
        {
            inCircle.GetComponent<SpriteRenderer>().enabled = false;
            outCircle.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void shootDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        weapon.transform.rotation = rotation;
    }
}
