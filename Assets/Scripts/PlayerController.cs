using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float sprintSpeed = 15.0f;
    public float runSpeed = 10.0f;
    public float walkSpeed = 5.0f;

    private static Rigidbody rigid;
    private enum MoveState
    {
        Walking,
        Running,
        Sprinting
    };
    private MoveState moveState = MoveState.Running;

    void Start()
    {
        rigid = transform.root.GetComponentInChildren<Rigidbody>();
    }

    void Update()
    {
        _Move();
    }

    private void _Move()
    {
        float horizontalX = Input.GetAxis("Horizontal");
        float horizontalZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalX, 0, horizontalZ);
        _MoveStateCheck();
        switch (moveState)
        {
            case MoveState.Walking:
                movement *= walkSpeed;
                break;
            case MoveState.Running:
                movement *= runSpeed;
                break;
            case MoveState.Sprinting:
                movement *= sprintSpeed;
                break;
        }
        rigid.velocity = new Vector3(0, rigid.velocity.y,0);
        rigid.velocity += movement;
    }
    private void _MoveStateCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveState = MoveState.Sprinting;
        }
        else if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            moveState = MoveState.Walking;
        }
        else
            moveState = MoveState.Running;
    }
}
