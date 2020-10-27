using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType
    {
        Flashlight,
        Crowbar,
        BlueKey,
        RedKey,
        GreenKey
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Flashlight: return ItemAssets.Instance.FlashlightSprite;
            case ItemType.Crowbar: return ItemAssets.Instance.CrowbarSprite;
            case ItemType.BlueKey: return ItemAssets.Instance.BlueKeySprite;
            case ItemType.RedKey: return ItemAssets.Instance.RedKeySprite;
            case ItemType.GreenKey: return ItemAssets.Instance.GreenKeySprite;
        }
    }
}
