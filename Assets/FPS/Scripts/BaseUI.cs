using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
		_base.OnHealthChanged += _base_OnHealthChanged;
	}

	private void OnDisable()
	{
		_base.OnHealthChanged -= _base_OnHealthChanged;
	}

	private void _base_OnHealthChanged()
	{
		_baseHP_TMP.text = $"{_base.Health_Current}/{_base.Health_Start + (float)(_base.BaseUpgradges * 100)}";
		_baseHP_Slider.value = _base.Health_Current;
	}
}
