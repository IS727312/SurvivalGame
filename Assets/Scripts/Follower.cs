using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed, minX, maxX, minY, maxY ;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        if( target != null)
        {
            float clampedX = Mathf.Clamp(target.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(target.position.y, minY, maxY);
            transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX, clampedY), smoothSpeed);

           // transform.LookAt(target);
        }
       
    }
}
