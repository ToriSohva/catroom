using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour {

    public Inventory inventory;

    void OnTriggerEnter2D (Collider2D collider) {
        var collItem = collider.GetComponent<CollectibleItem>();
        if (collItem != null) {
            if (inventory.Pick(collItem)) {
                collider.gameObject.SetActive(false);
                Debug.Log("Picked up an item");
            }
            else {
                Debug.Log("Failed to pick up an item; inventory is full");
            }
        }

        var collShroom = collider.GetComponent<MushroomItem>();
        if (collShroom != null) {
            inventory.DropAllOfType(CollectibleItem.ItemType.ShroomItem, collShroom.Shroomerge);
            Debug.Log("Shroomerged magic items");
        }

        var collFire = collider.GetComponent<FireItem>();
        if (collFire != null) {
            if (collFire.ShouldAddWood() == true) {
                inventory.DropOneOfType(CollectibleItem.ItemType.Firewood, collFire.BurnWood);
                Debug.Log("Burned one firewood");
            }
            else {
                Debug.Log("Fire was already hot enough; didn't burn any wood");
            }
        }
    }
}
