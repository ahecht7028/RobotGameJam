using UnityEngine;

public class AmmoPickUp : MonoBehaviour, IPickupAble
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerData>();
        if (player == null)
            return;

        PickedUp(player);
    }

    public void PickedUp(PlayerData player)
    {
        if (player.Ammo < player.MaxAmmo)
        {
            player.Reload();
            player.AmmoBar.UpdateAmmoBar(player.Ammo, player.MaxAmmo);
            Destroy(gameObject);
        }
        else
            return;
    }
}
