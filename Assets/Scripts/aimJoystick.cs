using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimJoystick : MonoBehaviour
{
    private FixedJoystick joystick;
    public GameObject projectile;
    public Transform shotPoint;
    public float timeBetweenShots;
    private float shotTime;

    // Start is called before the first frame update
    void Awake()
    {
        joystick = GameObject.FindWithTag("aimJoystick").GetComponent<FixedJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
        if(joystick.Horizontal != 0f || joystick.Vertical != 0f)
        {
            if (Time.time >= shotTime)
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                shotTime = Time.time + timeBetweenShots;
            }
        }
       
    }
}
