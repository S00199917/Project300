using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPositions : NetworkBehaviour
{
    [SyncVar(hook = nameof(UpdatePosition))] [SerializeField] Vector3 position;


    public void CreatePosition(Vector3 position)
    {
        this.transform.position = position;

    }

    void UpdatePosition(Vector3 oldPos, Vector3 newPos)
    {
        this.transform.position = newPos;
    }
}
