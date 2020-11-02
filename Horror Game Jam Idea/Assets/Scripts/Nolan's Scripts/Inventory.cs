using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;

    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        // remove below
        /*
        AddItem(new Item { itemType = Item.ItemType.GoldKey});
        AddItem(new Item { itemType = Item.ItemType.SilverKey});
        AddItem(new Item { itemType = Item.ItemType.BronzeKey});
        */
        //Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
        if (item.itemType != Item.ItemType.Flashlight)
        {
            GameManager.FoundKey();
        }

        itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item)
    {
        foreach (Item inventoryItem in itemList)
        {
            if(inventoryItem.itemType == item.itemType)
            {
                itemList.Remove(item);
            }
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
