using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour, IPickupAble
{
    //int _healthToGive = 15;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerData>();
        if (player == null)
            return;

        PickedUp(player);
    }

    public void PickedUp(PlayerData player)
    {
        if (player.Health < player.MaxHealth)
        {
            player.Heal(15);
            Destroy(gameObject);
        }
        else
            return;
    }
}
