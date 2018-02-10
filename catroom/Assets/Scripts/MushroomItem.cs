using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomItem : MonoBehaviour {

    [System.Serializable]
    public struct ThresholdMultiplier{
        public float threshold;
        public float multiplier;

        public ThresholdMultiplier(float threshold, float multiplier) {
            this.threshold = threshold;
            this.multiplier = multiplier;
        }
    }

    public float initialSize;
    public List<FireItem> fireItems = new List<FireItem>();
    public List<ThresholdMultiplier> healthThresholdMultipliers = new List<ThresholdMultiplier>();
    public float maxHealth;
    public float minHealthMultiplier;
    float size;
    float health;

	void Start () {
        size = initialSize;
        health = maxHealth;
    }

    void Update () {
        health += Time.deltaTime * GetMultiplier();
        if (health > maxHealth) {
            health = maxHealth;
        }

        if (health <= 0) {
            Debug.Log("Mushroom is dead, game over");
        }
    }

    float GetTotalHeat() {
        float total = 0f;
        foreach (FireItem item in fireItems) {
            total += item.GetHeatLevel();
        }

        return total;
    }

    float GetMultiplier() {
        float totalHeat = GetTotalHeat();
        List<ThresholdMultiplier> thresholdMultipliers = new List<ThresholdMultiplier>(healthThresholdMultipliers);
        thresholdMultipliers.Sort((a, b) => a.threshold.CompareTo(b.threshold));
        thresholdMultipliers.Reverse();

        foreach (ThresholdMultiplier tm in thresholdMultipliers) {
            if (totalHeat > tm.threshold) {
                return tm.multiplier;
            }
        }
        return minHealthMultiplier;
    }

    public void Shroomerge (CollectibleItem item) {
        size += item.ShroomGrowth;
        Debug.Log("Mushroom is now size " + size);
    }
}
