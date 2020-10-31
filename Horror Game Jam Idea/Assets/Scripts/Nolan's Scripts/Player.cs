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
            if (SearchForItem(new Item { itemType = Item.ItemType.Flashlight }))
            {
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
    }

    public static bool SearchForItem(Item item)
    {
        foreach(Item element in inventory.GetItemList())
        {
            if(element.itemType == item.itemType)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
