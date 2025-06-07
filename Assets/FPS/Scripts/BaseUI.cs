using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMesh Pro

public class BaseUI : MonoBehaviour
{
    [SerializeField]
    private Slider _baseHP_Slider;

    [SerializeField]
    private TextMeshProUGUI _baseHP_TMP;

    [SerializeField]
    private Base _base;

    private void Awake()
    {
        _baseHP_Slider.maxValue = _base.Health_Start;
        _baseHP_Slider.value = _base.Health_Current;
    }

    private void OnEnable()
    {
        _base.OnHealthChanged += UpdateBaseUI;
    }

    private void OnDisable()
    {
        _base.OnHealthChanged -= UpdateBaseUI;
    }

    private void UpdateBaseUI()
    {
        _baseHP_TMP.text = $"{_base.Health_Current}/{_base.Health_Start}";
        _baseHP_Slider.value = _base.Health_Current;
    }
}
