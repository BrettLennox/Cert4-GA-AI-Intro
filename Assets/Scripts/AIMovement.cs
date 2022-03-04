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
    //public GameObject[] position;
    public List<GameObject> position;
    public int positionIndex;
    public bool isChasing = false;
    public GameObject waypointPrefab;
    public GameObject waypointParent;

    [Header("Speed")]
    [Tooltip("The speed at which the AI moves")]
    public float speed = 1.5f;

    public void NewWayPoint()
    {
        float x = Random.Range(-9, 9.5f);
        float y = Random.Range(-5, 5.5f);

        GameObject newPoint = Instantiate(waypointPrefab, new Vector2(x,y), Quaternion.identity, waypointParent.transform);
        position.Add(newPoint);
    }

    public void FindClosestWaypoint()
    {
        float nearest = float.PositiveInfinity;
        int nearestIndex = 0;

        for(int i = 0; i <position.Count; i++)
        {
            float distance = Vector2.Distance(transform.position, position[i].transform.position);
            if(distance < nearest)
            {
                nearest = distance;
                nearestIndex = i;
            }
        }

        positionIndex = nearestIndex;
    }

    public void RemoveCurrentWaypoint()
    {
        GameObject current = position[positionIndex];
        position.Remove(current);
        Destroy(current);
    }

    public void AIMoveTowards(Transform goal)
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
            RemoveCurrentWaypoint();
            WaypointUpdate();
        }
    }

    private void WaypointUpdate()
    {
        if (positionIndex < position.Count - 1)
        {
            positionIndex++;
        }
        else
        {
            positionIndex = 0;
        }
    }

    public bool IsPlayerInRange()
    {
        return Vector2.Distance(transform.position, player.position) < chaseDistance;
    }
}
