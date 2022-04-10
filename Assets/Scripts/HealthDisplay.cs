using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private float _changeRate;

    private Animator _animator;
    private float _currentHealth;

    private void Start()
    {
        Health._changedHealth += OnChangedHealth;
        _animator = GetComponent<Animator>();
        _animator.SetFloat(AnimatorParams.HealthParameter, _healthBar.value);
        _healthBar.value = _healthBar.maxValue;
        _currentHealth = _healthBar.maxValue;
    }

    private void Update()
    {
        _healthBar.value = Mathf.MoveTowards(_healthBar.value, _currentHealth, _changeRate * Time.deltaTime);
        _animator.SetFloat(AnimatorParams.HealthParameter, _currentHealth);
    }

    public void OnChangedHealth(float currentHealth)
    {
        _currentHealth = currentHealth;
    }
}
