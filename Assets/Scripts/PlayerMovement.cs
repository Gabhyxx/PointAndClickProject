using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 move;

    [SerializeField] Rigidbody rb;
    [SerializeField] float speed = 5;
    [SerializeField] float rotationSpeed = 1;
    [SerializeField] float maxForce;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if(move.x!=0 || move.y != 0)
        {
            Walking();
        }
        
    }

    private void Walking()
    {
        Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y);
        targetVelocity *= speed;


        Vector3 velocityChange = (targetVelocity - currentVelocity);

        Vector3.ClampMagnitude(velocityChange, maxForce);


        rb.AddForce(velocityChange, ForceMode.VelocityChange);


        if (currentVelocity != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(currentVelocity, Vector3.up);

            rb.rotation = Quaternion.RotateTowards(rb.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
