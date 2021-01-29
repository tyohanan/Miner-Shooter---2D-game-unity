using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    private enum State
    {
        Moving,
        Knockback,
        Attack,
        Dead
    }

    private State currentState;
    private Rigidbody2D Enemyrb;
    private Animator EnemyAnim;

    private int facingDirection;
    private bool wallDetected;
    private bool PlayerDetected;

    [Header("Enemy Moving Data")]
    public float wallCheckDistance;
    public LayerMask whatIsWall;
    public float enemySpeed;
    public Vector2 movement;

    [Header("Enemy Attack Data")]
    public float PlayerRadiusDistance;
    public LayerMask whatIsPlayer;

    private void Start()
    {
        Enemyrb = GetComponent<Rigidbody2D>();
        EnemyAnim = GetComponent<Animator>();
        facingDirection = 1;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
            case State.Attack:
                UpdateAttackState();
                break;
        }
    }

    //moving state --------------------------------------------------------

    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        EnemyAnim.SetBool("Moving", true);
        PlayerDetected = Physics2D.OverlapCircle(transform.position, PlayerRadiusDistance, whatIsPlayer);
        wallDetected = Physics2D.Raycast(transform.position, Vector2.right*facingDirection, wallCheckDistance, whatIsWall);
        if (wallDetected)
        {
            Flip();
        }
        else
        {
            Enemyrb.MovePosition(Enemyrb.position + movement * enemySpeed *facingDirection*Time.deltaTime);
        }

        if (PlayerDetected)
        {
            SwitchState(State.Attack);
        }
    }

    private void ExitMovingState()
    {
        EnemyAnim.SetBool("Moving", false);
    }

    //knockback state -----------------------------------------------------------
    
    private void EnterKnockbackState()
    {

    }

    private void UpdateKnockbackState()
    {

    }

    private void ExitKnockbackState()
    {

    }

    //dead state --------------------------------

    private void EnterDeadState()
    {

    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    //attack state------------------------------------
    private void EnterAttackState()
    {
        
    }

    private void UpdateAttackState()
    {
        EnemyAnim.SetBool("Attack", true);
        SwitchState(State.Moving);
    }

    private void ExitAttackState()
    {
        EnemyAnim.SetBool("Attack", false);
    }

    //---------------------------
    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
            case State.Attack:
                ExitAttackState();
                break;
        }

        switch (state)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
            case State.Attack:
                EnterAttackState();
                break;
        }

        currentState = state;
    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0, 180, 0);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2 (transform.position.x+wallCheckDistance, transform.position.y));
        Gizmos.DrawWireSphere(transform.position, PlayerRadiusDistance);
    }
}
