using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanager : MonoBehaviour
{
    public string Pos;
    public event Action<Collider, string> CallCameraChanger;

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
            CallCameraChanger?.Invoke(other, Pos);
    }

}
