using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Movement : NetworkedBehaviour
{
    void Update()
    {
        if (IsOwner) {
            transform.position += 2 * Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime;

        }
    }
}
