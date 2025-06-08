using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;

    private Base _base;

    public void SetBase(Base baseRef)
    {
        _base = baseRef;

        if (_base != null)
        {
            UpdateHealthUI(); // Initialize UI on start
        }
    }

    public void UpdateHealthUI()
{
    if (_base == null) return;

    float ratio = _base.Health_Current / _base.Health_Start;
    healthSlider.value = ratio;

    if (healthText != null)
    {
        healthText.text = $" {_base.Health_Current}/{_base.Health_Start}";
        Debug.Log($"Base UI updated: {_base.Health_Current} HP remaining.");
    }
}
}
