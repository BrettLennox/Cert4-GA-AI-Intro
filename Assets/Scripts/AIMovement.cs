using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [Header("Goal")]
    [Tooltip("The minimum distance to reach before stopping moving to the goal")]
    public float minGoalDistance = 0.05f;
    [Tooltip("The goal for the square to move towards")]
    public GameObject goalPos0;
    public Transform player;
    [Tooltip("")]
    public float chaseDistance = 3f;
    public GameObject[] position;
    public int positionIndex;

    [Header("Speed")]
    [Tooltip("The speed at which the AI moves")]
    public float speed = 1.5f;

    private void Start()
    {
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < chaseDistance)
        {
            AIMoveTowards(player);

        }
        else
        {
            var dist = Vector3.Distance(transform.position, position[positionIndex].transform.position);
            for(int i = 0; i < position.Length; i++)
            {
                var tempDistance = Vector3.Distance(transform.position, position[i].transform.position);
                if(tempDistance < dist)
                {
                    positionIndex = i;
                }
            }
            AIMoveTowards(position[positionIndex].transform);
        }
    }

    private void WaypointUpdate(int posIndex)
    {
        positionIndex = posIndex;
    }

    private void AIMoveTowards(Transform goal)
    {
        #region Commented Out
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
        #endregion
        Vector2 AIPosition = transform.position;

        //if we are not near the goal move towards
        if (Vector2.Distance(AIPosition, goal.position) > minGoalDistance)
        {
            Vector2 directionToGoal = (goal.position - transform.position);
            directionToGoal.Normalize();
            transform.position += (Vector3)directionToGoal * speed * Time.deltaTime;
        }
        else
        {
            if (positionIndex < position.Length - 1)
            {
                positionIndex++;
            }
            else
            {
                positionIndex = 0;
            }
        }
    }
}
