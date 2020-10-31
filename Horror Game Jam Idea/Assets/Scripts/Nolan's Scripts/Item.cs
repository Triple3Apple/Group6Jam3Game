using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType
    {
        Flashlight,
        BronzeKey,
        SilverKey,
        GoldKey
    }

    public ItemType itemType;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Flashlight: return ItemAssets.Instance.FlashlightSprite;
            case ItemType.BronzeKey: return ItemAssets.Instance.BronzeKeySprite;
            case ItemType.SilverKey: return ItemAssets.Instance.SilverKeySprite;
            case ItemType.GoldKey: return ItemAssets.Instance.GoldKeySprite;
        }
    }
}
