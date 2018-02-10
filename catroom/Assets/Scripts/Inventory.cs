using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    List<CollectibleItem> items = new List<CollectibleItem>();
    public int sizeLimit;

    public bool Pick (CollectibleItem item) {
        if (items.Count < sizeLimit) {
            items.Add(item);
            Debug.Log(items.Count);
            return true;
        }

        return false;
    }

    public delegate void DropHandler(CollectibleItem item);

    public bool Drop (CollectibleItem item, DropHandler handler = null) {
        if (item != null) {
            if (handler != null) {
                handler.Invoke(item);
            }

            items.Remove(item);
            return true;
        }
        else {
            return false;
        }
    }

    public bool Drop (int index, DropHandler handler = null) {
        CollectibleItem item = items[index];
        return Drop(item, handler);
    }

    public bool DropOneOfType (CollectibleItem.ItemType itemType, DropHandler handler = null) {
        CollectibleItem item = items.Find(i => i.itemType == itemType);
        return Drop(item, handler);
    }

    public void DropAllOfType (CollectibleItem.ItemType itemType, DropHandler handler = null) {
        if (handler != null) {
            foreach (CollectibleItem item in items.FindAll(i => i.itemType == itemType)) {
                Drop(item, handler);
            }
        }
    }
}
