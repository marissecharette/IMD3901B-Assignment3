using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class UI : MonoBehaviour
{
    public Ice ice;
    public Boar boar;
    public TextMeshProUGUI iceHeatText;
    public TextMeshProUGUI boarDamageText;

    private float totalIceHeat = 4f;
    private float maxBoarHealth = 10f;

    void Start()
    {
        if (ice != null)
            totalIceHeat = ice.currentMelted.Value + 0; // start value

        if (boar != null)
            maxBoarHealth = boar.currentHealth.Value;
    }

    void Update()
    {
        if (ice != null)
        {
            float heatTaken = totalIceHeat - ice.currentMelted.Value;
            iceHeatText.text = $"Ice Heat: {heatTaken}/{totalIceHeat}";
        }

        if (boar != null)
        {
            float damageTaken = maxBoarHealth - boar.currentHealth.Value;
            boarDamageText.text = $"Boar Damage: {damageTaken}/{maxBoarHealth}";
        }
    }
}
