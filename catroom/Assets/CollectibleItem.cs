using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour {
    [System.Serializable]
    public enum ItemType {
        Firewood,
        ShroomItem,
    };

    public ItemType itemType;
    public float ShroomGrowth;
}
