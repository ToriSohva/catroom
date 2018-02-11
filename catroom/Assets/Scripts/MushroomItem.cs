using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

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

    [System.Serializable]
    public struct DebugStats {
        public TMP_Text MushroomHealthLabel;
        public TMP_Text MushroomTemperatureLabel;
        public TMP_Text FireTemperaturesLabel;
        public TMP_Text FireHeatLabel;

        public DebugStats(TMP_Text MHLabel, TMP_Text MTLabel, TMP_Text FTLabel, TMP_Text FHLabel) {
            this.MushroomHealthLabel = MHLabel;
            this.MushroomTemperatureLabel = MTLabel;
            this.FireTemperaturesLabel = FTLabel;
            this.FireHeatLabel = FHLabel;
        }
    }

    public float initialSize;
    public List<FireItem> fireItems = new List<FireItem>();
    public List<ThresholdMultiplier> healthThresholdMultipliers = new List<ThresholdMultiplier>();
    public float maxHealth;
    public float minHealthMultiplier;
    public Transform hudNeedle;
    public GameObject fireWarning;
    public bool showDebug;
    public DebugStats debugStats;
    float size;
    float health;

	void Start () {
        size = initialSize;
        health = maxHealth;
    }

    void Update () {
        health = Mathf.Clamp(health + Time.deltaTime * GetMultiplier(), 0, maxHealth);

        if (health <= 0) {
            Debug.Log("Mushroom is dead, game over");
        }

        float needleRotation = Mathf.Clamp((GetTotalHeat() / GetMaxHeat()) * -180f, -180, 0);
        hudNeedle.rotation = Quaternion.Euler(0, 0, needleRotation);

        int warnings = GetFireWarnings();

        if (warnings > 0 && fireWarning.active == false) {
            fireWarning.SetActive(true);
        }
        else if (warnings <= 0 && fireWarning.active == true) {
            fireWarning.SetActive(false);
        }

        if (showDebug == true) {
            SetDebugLabels();
        }
    }

    float GetTotalHeat() {
        float total = 0;
        foreach (FireItem item in fireItems) {
            total += item.GetHeatLevel();
        }

        return total;
    }

    float GetMaxHeat() {
        int total = 0;
        foreach (FireItem item in fireItems) {
            total += item.GetMaxHeat();
        }

        return total;
    }

    int GetFireWarnings () {
        int result = 0;
        foreach (FireItem item in fireItems) {
            if (item.GetWarningStatus() == true) {
                result += 1;
            }
        }

        return result;
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

    void SetDebugLabels () {
        string mhText, mtText, ftText, fhText;
        List<FireItem> clonedFireItems;

        mhText = (int) health + "/" + (int) maxHealth;
        mtText = GetTotalHeat() + "/" + GetMaxHeat();

        clonedFireItems = new List<FireItem>(fireItems);
        ftText = string.Join(", ", new List<string>(clonedFireItems.Select(i => FormatFTItem(i))).ToArray());

        clonedFireItems = new List<FireItem>(fireItems);
        fhText = string.Join(", ", new List<string>(clonedFireItems.Select(i => FormatFHItem(i))).ToArray());

        debugStats.MushroomHealthLabel.SetText(mhText);
        debugStats.MushroomTemperatureLabel.SetText(mtText);
        debugStats.FireTemperaturesLabel.SetText(ftText);
        debugStats.FireHeatLabel.SetText(fhText);
    }

    string FormatFTItem(FireItem item) {
        return (int) item.DebugGetHeatValue() + "/" + (int) item.maxHeat;
    }
    string FormatFHItem(FireItem item) {
        return item.GetHeatLevel() + "/" + item.GetMaxHeat();
    }
}
