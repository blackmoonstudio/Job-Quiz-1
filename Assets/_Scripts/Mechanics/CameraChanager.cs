using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanager : MonoBehaviour
{
    public string Pos;
    public event Action<Collider, string> CallCameraChanger;
    // This method is called when another collider enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Call the event if it has subscribers
        if (other.tag == "Player")
            CallCameraChanger?.Invoke(other, Pos);
    }
    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.tag == "Player")
    //        CallCameraChanger?.Invoke(other, Pos);
    //}

}
