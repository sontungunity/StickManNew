using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WayGetItem
{
    public PriceType Type;
    public ItemStack ItemStackDetail;
    public int LevelDetail;

    public enum PriceType {
        NONE,
        ITEMSTACK,
        ADS,
        SPIN,
        LEVEL,
        SPECIAL,
        PRESTIGE
    }
}
