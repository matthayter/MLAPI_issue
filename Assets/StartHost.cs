using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class StartHost : MonoBehaviour
{
    public void DoStartHost()
    {
        NetworkingManager.Singleton.StartHost();
    }
}
