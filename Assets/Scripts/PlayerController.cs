using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float sprintSpeed = 15.0f;
    public float runSpeed = 10.0f;

    private float currSpeed = 0f;
    private static Rigidbody rigid;
    private float horizontalX = 0f;
    private float horizontalZ = 0f;

    Health health;
    private enum MoveState
    {
        Running,
        Sprinting
    };
    private MoveState moveState = MoveState.Running;

    void Start()
    {
        rigid = transform.root.GetComponentInChildren<Rigidbody>();
        health = transform.root.GetComponentInChildren<Health>();
    }

    void Update()
    {
        _Move();
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(10,20,100, 20), "Health: " + health.healthPercent * 100 + "%");
    }

    private void _Move()
    {
        horizontalX = Input.GetAxis("Horizontal");
        horizontalZ = Input.GetAxis("Vertical");
        float speed = 0f;
        _MoveStateCheck();
        switch (moveState)
        {
            case MoveState.Running:
                speed = runSpeed;
                break;
            case MoveState.Sprinting:
                speed = sprintSpeed;
                break;
        }
        currSpeed = speed;
        Vector3 movement = new Vector3(speed * horizontalX, 0, speed * horizontalZ);
        rigid.transform.Translate(movement * Time.deltaTime);
    }

    private void _MoveStateCheck()
    {
        if (Input.GetKey(KeyCode.LeftShift) && horizontalZ > 0)
            moveState = MoveState.Sprinting;
        else
            moveState = MoveState.Running;
    }
}
