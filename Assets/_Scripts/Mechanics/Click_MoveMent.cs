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
        RaycastHit[] hits = Physics.RaycastAll(ray.origin, ray.direction, Mathf.Infinity, Mask).OrderBy(h => h.distance).ToArray();
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.tag == "Floor")
            {
                agent.SetDestination(hits[i].point);
                break;
            }
        }
        //if (Physics.Raycast(ray, out RaycastHit hit,Mask))
        //{
        //    Debug.Log("Hit" + hit.transform.gameObject);
        //    agent.SetDestination(hit.point);
        //}
    }
}
