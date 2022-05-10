using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    GameObject playerRef;
    void Start()
    {
        playerRef = GameObject.Find("Player").gameObject;
        transform.position = playerRef.transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    void Update()
    {
        Vector3 playerDir = playerRef.transform.position - transform.position;
        playerDir = new Vector3(playerDir.x, playerDir.y, -10);
        transform.position += new Vector3(playerDir.x * Time.deltaTime * 2, playerDir.y * Time.deltaTime * 2, 0);
    }
}
