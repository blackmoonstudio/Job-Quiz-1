using Input;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Click_MoveMent : MonoBehaviour
{
    private Camera mainCamera;
    private NavMeshAgent agent;
    private PlayerInputSystem controls;
    public LayerMask Mask;

    private void Awake()
    {
        controls = new PlayerInputSystem();
        mainCamera = FindObjectOfType<Camera>();
        agent = GetComponent<NavMeshAgent>();
        controls.Player.Click.performed += ctx => Move();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Move()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, Mask)
            .Where(hit => hit.transform.CompareTag("Floor"))
            .OrderBy(hit => hit.distance)
            .ToArray();

        if (hits.Length > 0)
        {
            agent.SetDestination(hits[0].point);
        }
    }
}
