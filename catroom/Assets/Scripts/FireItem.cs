﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireItem : MonoBehaviour {

    public float maxHeat;
    public float addWoodThreshold;
    public float warningThreshold;
    public List<float> heatLevelThresholds;
    public float burnRate;
    float heat;
    List<float> thresholds = new List<float>();


    void Start () {
        heat = maxHeat;
        thresholds = new List<float>(heatLevelThresholds);
        thresholds.Sort();
    }
	
	void Update () {
        heat = Mathf.Clamp(heat - Time.deltaTime * burnRate, 0, maxHeat);
	}

    public bool ShouldAddWood() {
        return heat < addWoodThreshold;
    }

    // returns integer between 0 (coldest) and n (warmest)
    public int GetHeatLevel() {
        for (int i = 0; i < thresholds.Count; i++) {
            if (heat < thresholds[i]) {
                return i;
            }
        }
        return heatLevelThresholds.Count;
    }

    public int GetMaxHeat() {
        return thresholds.Count;
    }

    public bool GetWarningStatus() {
        return heat <= warningThreshold;
    }

    public void BurnWood(CollectibleItem item) {
        heat = maxHeat;
        Debug.Log("Heat is now " + heat);
    }

    public float DebugGetHeatValue() {
        return heat;
    }
}
