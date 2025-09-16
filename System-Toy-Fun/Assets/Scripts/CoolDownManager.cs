using System;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownManager : MonoBehaviour
{
    private Dictionary<string, float> cooldownTimers = new Dictionary<string, float>();
    private Dictionary<string, float> cooldownDurations = new Dictionary<string, float>();

    public void RegisterAbility(string abilityName, float cooldownDuration)
    {
        cooldownDurations[abilityName] = cooldownDuration;
        cooldownTimers[abilityName] = 0f;
    }

    public bool CanUse(string abilityName)
    {
        return cooldownTimers[abilityName] <= 0f;
    }

    public void TriggerCooldown(string abilityName)
    {
        cooldownTimers[abilityName] = cooldownDurations[abilityName];
    }

    void Update()
    {
        var keys = new List<string>(cooldownTimers.Keys);
        foreach (var key in keys)
        {
            cooldownTimers[key] = Mathf.Max(0f, cooldownTimers[key] - Time.deltaTime);
        }
    }
}
