using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum State
    {
        Attack,
        Defence,
        RunAway,
        BerryPicking
    }

    public State currentState;

    public AIMovement aiMovement;

    private void Start()
    {
        aiMovement = GetComponent<AIMovement>();
        NextState();
    }

    private void NextState()
    {
        switch (currentState)
        {
            case State.Attack:
                StartCoroutine(AttackState());
                break;
            case State.Defence:
                StartCoroutine(DefenceState());
                break;
            case State.RunAway:
                StartCoroutine(RunAwayState());
                break;
            case State.BerryPicking:
                StartCoroutine(BerryPickingState());
                break;
            default:
                break;
        }
    }

    private IEnumerator AttackState()
    {
        Debug.Log("Attack: Enter");
        while (currentState == State.Attack)
        {
            aiMovement.AIMoveTowards(aiMovement.player);

            if (!aiMovement.IsPlayerInRange())
            {
                currentState = State.BerryPicking;
            }

            yield return null;
        }
        Debug.Log("Attack: Exit");
        NextState();
    }

    private IEnumerator DefenceState()
    {
        Debug.Log("Defence: Enter");
        while (currentState == State.Defence)
        {
            aiMovement.NewWayPoint();

            if(aiMovement.position.Count >= 5)
            {
                currentState = State.BerryPicking;
            }
            Debug.Log("Currently defending");
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Defence: Exit");
        NextState();
    }

    private IEnumerator RunAwayState()
    {
        Debug.Log("RunAway: Enter");
        while (currentState == State.RunAway)
        {
            Debug.Log("Currently running away");
            yield return null;
        }
        Debug.Log("RunAway: Exit");
        NextState();
    }

    private IEnumerator BerryPickingState()
    {
        Debug.Log("BerryPicking: Enter");
        aiMovement.FindClosestWaypoint();

        while (currentState == State.BerryPicking)
        {
            aiMovement.AIMoveTowards(aiMovement.position[aiMovement.positionIndex].transform);

            if (aiMovement.IsPlayerInRange())
            {
                currentState = State.Attack;
            }
            if(aiMovement.position.Count <= 0)
            {
                currentState = State.Defence;
            }

            yield return null;
        }
        Debug.Log("BerryPicking: Exit");
        NextState();
    }

}
