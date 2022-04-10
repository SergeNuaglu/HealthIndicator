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

    private float _healthPerClick = 10f;
    private float _targetHealth;
    private Coroutine _changeHealthJob;
    private Animator _animator;

    public void TakeDamage()
    {
        if (_health.CurrentHealth - _healthPerClick >= _healthBar.minValue)
            _targetHealth = _health.CurrentHealth - _healthPerClick;
        else
            _targetHealth = _healthBar.minValue;

        StartChangeHealth(true);
    }

    public void AddHealth()
    {
        if (_health.CurrentHealth + _healthPerClick <= _healthBar.maxValue)
            _targetHealth = _health.CurrentHealth + _healthPerClick;
        else
            _targetHealth = _healthBar.maxValue;

        StartChangeHealth(false);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat(AnimatorParams._healthParameter, _health.CurrentHealth);
        _healthBar.maxValue = _health.TotalHealth;
        _healthBar.value = _healthBar.maxValue;
    }

    private IEnumerator ChangeHealth(float targetHealth)
    {
        while (_health.CurrentHealth != targetHealth)
        {
            _healthBar.value = Mathf.MoveTowards(_healthBar.value, targetHealth, _changeRate * Time.deltaTime);
            _health.SetCurrentHealth(_healthBar.value);
            _animator.SetFloat(AnimatorParams._healthParameter, _healthBar.value);
            yield return null;
        }
    }

    private void StartChangeHealth(bool isDamage)
    {
        if (_health.CurrentHealth != _targetHealth)
        {
            StopChangeHealth();
            _changeHealthJob = StartCoroutine(ChangeHealth(_targetHealth));

            if (isDamage)
                _animator.SetTrigger(AnimatorParams._damageTrigger);
            else
                _animator.SetTrigger(AnimatorParams._healthTrigger);
        }
    }

    private void StopChangeHealth()
    {
        if (_changeHealthJob != null)
        {
            StopCoroutine(_changeHealthJob);
            _changeHealthJob = null;
        }
    }
}
