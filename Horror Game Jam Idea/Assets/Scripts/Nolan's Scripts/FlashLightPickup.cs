using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightPickup : MonoBehaviour, IInteractable
{
    private Item itemIdentifier = new Item { itemType = Item.ItemType.Flashlight, amount = 1 };

    public void OnStartHover()
    {
        Debug.Log("You can now pickup the flashlight");
    }

    public void OnInteract()
    {
        Player.inventory.AddItem(itemIdentifier);
        Debug.Log("Picked up the flashlight");
        Destroy(this.gameObject);
    }

    public void OnEndHover()
    {
        Debug.Log("You can't pickup the flashlight anymore.");
    }
}
