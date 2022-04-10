using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private float _changeRate;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat(AnimatorParams.HealthParameter, _healthBar.value);
        _healthBar.maxValue = _health.MaxHealth;
        _healthBar.value = _healthBar.maxValue;
    }

    private void Update()
    {
        if (_healthBar.value != _health.CurrentHealth)
        _healthBar.value = Mathf.MoveTowards(_healthBar.value, _health.CurrentHealth, _changeRate * Time.deltaTime);

        _animator.SetFloat(AnimatorParams.HealthParameter, _healthBar.value);
    }
}
