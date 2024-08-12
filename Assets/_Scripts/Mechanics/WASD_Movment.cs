using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Input;
using UnityEngine.AI;

public class WASD_Movment : MonoBehaviour
{
    
    public float moveSpeed = 5f;
    private Vector3 moveInput;
    private Rigidbody rb;
    private PlayerInputSystem controls;
    private NavMeshAgent agent;
    private Camera mainCamera;

    private void Awake()
    {
        controls = new PlayerInputSystem();
        rb = GetComponent<Rigidbody>();
        
        mainCamera = GameObject.FindObjectOfType<Camera>();

        
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMove;
      
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMove;
        controls.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector3>();
    }

    private void FixedUpdate()
    {
        Vector3 move = moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
        
    }
    

}
