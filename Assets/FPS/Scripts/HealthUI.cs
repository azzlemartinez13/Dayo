using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro; // Import TextMesh Pro
public class HealthUI : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public TMP_Text healthText;



    private HealthComponent healthComponent;

    private void Start()
    {
        healthComponent = FindObjectOfType<HealthComponent>();
    }

    private void Update()
    {
        //UpdateHeartsUI();
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);

        if (healthText != null)
        {
            healthText.text = health + " / " + health; // Initial full health display
        }
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);

        if (healthText != null)
        {
            healthText.text = health + " / " + slider.maxValue; // Update health text dynamically
        }
    }
}
