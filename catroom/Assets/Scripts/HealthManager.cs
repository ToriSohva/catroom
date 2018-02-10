using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    public int MaxHealth;
    public int Health;

    public void Hit(int amount)
    {
        Health -= amount;
    }

    public int CheckHealth()
    {
        return Health;
    }

    public int CheckMaxHealth()
    {
        return MaxHealth;
    }

    public void ResetHealth()
    {
        Debug.Log(MaxHealth);
        Health = MaxHealth;
    }
}
