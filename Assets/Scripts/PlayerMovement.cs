using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector2 moveDir = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDir.y++;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDir.y--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir.x++;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDir.x--;
        }
        //moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //moveDir = transform.TransformDirection(moveDir);
        moveDir *= (speed * Time.deltaTime);
        transform.position += (Vector3)moveDir;
    }
}
