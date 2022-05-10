using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmogusEasterEgg : MonoBehaviour
{
    GameObject amogus;

    void Start()
    {
        amogus = GameObject.Find("AmogusEasterEgg").gameObject;
        amogus.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            amogus.SetActive(true);
        }
    }
}
