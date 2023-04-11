using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    Transform playerTrans;
    void FixedUpdate()
    {
        this.transform.position = new Vector3(transform.position.x,
            transform.position.y, playerTrans.position.z);
    }
}
