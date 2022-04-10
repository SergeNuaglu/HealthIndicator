using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private ChangeHealthEvent _changeHealthEvent;

    private float _currentHealth;
    private float _minHealth = 0;
    private Animator _animator;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;
    private float _healthPerClick = 10f;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage()
    {
        _currentHealth = Mathf.Clamp(_currentHealth - _healthPerClick, _minHealth, _maxHealth);
        _animator.SetTrigger(AnimatorParams.DamageTrigger);
        _changeHealthEvent?.Invoke(_currentHealth);
    }

    public void AddHealth()
    {
        _currentHealth = Mathf.Clamp(_currentHealth + _healthPerClick, _minHealth, _maxHealth);
        _animator.SetTrigger(AnimatorParams.HealthTrigger);
        _changeHealthEvent?.Invoke(_currentHealth);
    }
}
