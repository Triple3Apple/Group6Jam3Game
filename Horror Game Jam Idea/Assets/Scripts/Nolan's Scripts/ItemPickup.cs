using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private Item.ItemType itemIdentifier = Item.ItemType.BronzeKey;

    public void OnEndHover()
    {
        return;
    }

    public void OnInteract()
    {
        Player.inventory.AddItem(new Item { itemType = itemIdentifier });
        Debug.Log($"Picked up the {itemIdentifier}");
        Destroy(this.gameObject);
    }

    public void OnStartHover()
    {
        return;
    }
}
