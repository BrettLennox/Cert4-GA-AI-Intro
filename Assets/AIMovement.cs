using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public GameObject position0;
    public GameObject position1
        ;
    private void Update()
    {
        //Vector2 AIPosition = transform.position;
        //if (transform.position.x < position0.transform.position.x)
        //{
        //    AIPosition.x += (1 * Time.deltaTime);
        //}
        //else
        //{
        //    AIPosition.x -= (1 * Time.deltaTime);
        //}
        //if (transform.position.y < position0.transform.position.y)
        //{
        //    AIPosition.y += (1 * Time.deltaTime);
        //}
        //else
        //{
        //    AIPosition.y -= (1 * Time.deltaTime);
        //}
        //transform.position = AIPosition;


        //transform.position = 
        //    Vector2.MoveTowards(transform.position, position0.transform.position, 1 * Time.deltaTime);

        Vector2 directionToPos0 = (Vector2)(position0.transform.position - transform.position);
        directionToPos0.Normalize();
        transform.position += (Vector3)directionToPos0 * 1 * Time.deltaTime;
    }
}
