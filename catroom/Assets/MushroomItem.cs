using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomItem : MonoBehaviour {

    public float initialSize;
    float size;

	void Start () {
        size = initialSize;
	}

    public void Shroomerge (CollectibleItem item) {
        size += item.ShroomGrowth;
        Debug.Log("Mushroom is now size " + size);
    }
}
