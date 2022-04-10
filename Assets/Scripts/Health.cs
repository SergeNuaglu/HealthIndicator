using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private HealthDisplay _healthDisplay;

    private float _currentHealth;
    private float _minHealth = 0;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;
    private float _healthPerClick = 10f;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage()
    {
        if (_currentHealth - _healthPerClick >= _minHealth)
            _currentHealth -= _healthPerClick;
        else
            _currentHealth = _minHealth;

        _healthDisplay.StartChangeHealth(true, _currentHealth);
    }

    public void AddHealth()
    {
        if (_currentHealth + _healthPerClick <= _maxHealth)
            _currentHealth += _healthPerClick;
        else
            _currentHealth = _maxHealth;

        _healthDisplay.StartChangeHealth(false, _currentHealth);
    }
}
