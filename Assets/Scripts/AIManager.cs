using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : BaseManager
{
    public enum State
    {
        HighHP,
        LowHP,
        Dead
    }
    public State currentState;
    protected PlayerManager _playerManager;
    [SerializeField] protected Animator _anim;

    protected override void Start()
    {
        base.Start();
        _playerManager = GetComponent<PlayerManager>();
        if(_playerManager == null)
        {
            Debug.LogError("PlayerManager '_playerManager' not found");
        }
    }
    public override void TakeTurn()
    {
        if(_health <= 0f)
        {
            currentState = State.Dead;
        }
        switch (currentState)
        {
            case State.HighHP:
                HighHPState();
                break;
            case State.LowHP:
                LowHPState();
                break;
            case State.Dead:
                DeadState();
                break;
            default:
                break;
        }
    }
    protected override void EndTurn()
    {
        StartCoroutine(WaitAndEndTurn());
    }
    private IEnumerator WaitAndEndTurn()
    {
        yield return new WaitForSecondsRealtime(2f);
        _playerManager.TakeTurn();
    }
    void HighHPState()
    {
        if(_health < 40f)
        {
            currentState = State.LowHP;
            LowHPState();
            return;
        }
        int randomAttack = Random.Range(0, 10);
        switch (randomAttack)
        {
            case int i when i >= 0 && i <= 1:
                Splash();
                break;
            case int i when i > 1 && i <= 8:
                IronTail();
                break;
            case int i when i > 8 && i <= 9:
                SelfDestruct();
                break;
        }
    }
    void LowHPState()
    {
        int randomAttack = Random.Range(0, 10);
        switch (randomAttack)
        {
            case int i when i >= 0 && i <= 1:
                Splash();
                break;
            case int i when i > 1 && i <= 8:
                Rest();
                break;
            case int i when i > 8 && i <= 9:
                IronTail();
                break;
        }
        if(_health > 60f)
        {
            currentState = State.HighHP;
        }
    }
    void DeadState()
    {
        Debug.Log("AI IS DEAD, YOU WIN");
    }
    public void Splash()
    {
        Debug.Log("AI Used Splash");
        _anim.SetTrigger("Splash");
        _playerManager.DealDamage(40.4f);
        EndTurn();
    }
    public void IronTail()
    {
        Debug.Log("AI Used Iron Tail");
        _playerManager.DealDamage(10f);
        EndTurn();
    }
    public void Rest()
    {
        Debug.Log("AI Used Rest");
        Heal(30f);
        EndTurn();
    }
    public void SelfDestruct()
    {
        Debug.Log("AI Used SelfDestruct");
        _playerManager.DealDamage(80f);
        DealDamage(_maxHealth);
        EndTurn();
    }
}
