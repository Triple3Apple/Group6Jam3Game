using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] GameObject flashlight;

    private bool isFlashlightOn = false;

    public static Inventory inventory { get; private set; }

    private void Awake()
    {
        inventory = new Inventory();
        inventoryUI.SetInventory(inventory);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!SearchForItem(new Item { itemType = Item.ItemType.Flashlight }))
            {
                return;
            }

            if (isFlashlightOn)
            {
                flashlight.SetActive(false);
                isFlashlightOn = false;
            }
            else
            {
                flashlight.SetActive(true);
                isFlashlightOn = true;
            }
        }
    }

    public static bool SearchForItem(Item item)
    {
        List<Item> _inv = inventory.GetItemList();

        if(_inv.Find(x => x.itemType == item.itemType) != null)
        {
            return true;
        }

        return false;
    }
}
