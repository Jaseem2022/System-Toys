using System;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownManager : MonoBehaviour
{
    private Dictionary<int, float> cooldownTimers = new Dictionary<int, float>();
    private Dictionary<int, float> cooldownDurations = new Dictionary<int, float>();

    public void RegisterAbility(int abilityNameIndex, float cooldownDuration)
    {
        cooldownDurations[abilityNameIndex] = cooldownDuration;
        cooldownTimers[abilityNameIndex] = 0f;
    }

    public bool CanUse(int abilityNameIndex)
    {
        return cooldownTimers[abilityNameIndex] <= 0f;
    }

    public void TriggerCooldown(int abilityNameIndex)
    {
        cooldownTimers[abilityNameIndex] = cooldownDurations[abilityNameIndex];
    }

    void Update()
    {
        var keys = new List<int>(cooldownTimers.Keys);
        foreach (var key in keys)
        {
            cooldownTimers[key] = Mathf.Max(0f, cooldownTimers[key] - Time.deltaTime);
        }
    }
}
