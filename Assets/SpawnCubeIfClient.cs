using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class SpawnCubeIfClient : NetworkedBehaviour
{
    [SerializeField] GameObject m_cubePrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (IsOwner && !IsServer) {
            InvokeServerRpc(MakeCube, this.NetworkedObject.OwnerClientId);
        }
    }

    [ServerRPC]
    public void MakeCube(uint owner)
    {
        GameObject cube = Instantiate(m_cubePrefab);
        cube.GetComponent<NetworkedObject>().SpawnWithOwnership(owner);
    }
}
