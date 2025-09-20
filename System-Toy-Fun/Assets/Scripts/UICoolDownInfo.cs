using TMPro;
using UnityEngine;

public class UICoolDownInfo : MonoBehaviour
{
    [SerializeField] TMP_Text blinkText;
    [SerializeField] TMP_Text rewindText;
    [SerializeField] TMP_Text cloneSpawnText;

    private CoolDownManager coolDownManager;

    string blinkString = "Blink : ";
    string rewindString = "Rewind : ";
    string cloneSpawnString = "Clone Spawn : ";
    
    void UpdateAbilityText(TMP_Text textField, string label, Ability.PlayerAbilities abilityEnum)
    {
        float remaining = coolDownManager.RemainingTime((int)abilityEnum);
        textField.text = remaining > 0 ? label + remaining.ToString("F0") : label + "Ready";
    }

    void Start()
    {
        coolDownManager = FindFirstObjectByType<CoolDownManager>();
    }


    void Update()
    {
        UpdateAbilityText(blinkText, blinkString, Ability.PlayerAbilities.BLINK);
        UpdateAbilityText(rewindText, rewindString, Ability.PlayerAbilities.REWIND);
        UpdateAbilityText(cloneSpawnText, cloneSpawnString, Ability.PlayerAbilities.CLONE_SPAWN);
    }
}
