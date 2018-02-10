using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireItem : MonoBehaviour {

    public float maxHeat;
    public float addWoodTreshold;
    float heat;

	void Start () {
        heat = maxHeat;
	}
	
	void Update () {
        heat -= Time.deltaTime;
	}

    public bool ShouldAddWood() {
        return heat < addWoodTreshold;
    }

    public void BurnWood(CollectibleItem item) {
        heat = maxHeat;
        Debug.Log("Heat is now " + heat);
    }
}
