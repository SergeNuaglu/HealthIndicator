using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]

public class PlayerHealthChanger : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private float _changeRate;
    [SerializeField] private float _totalHealth;

    private float _healthPerClick = 10f;
    private float _targetHealth;
    private Coroutine _changeHealthJob;
    private Animator _animator;
    private const string _damageTrigger = "TakeDamage";
    private const string _healthTrigger = "AddHealth";
    private const string _healthParameter = "Health";

    public void TakeDamage()
    {
        if (_healthBar.value - _healthPerClick >= _healthBar.minValue)
            _targetHealth = _healthBar.value - _healthPerClick;
        else
            _targetHealth = _healthBar.minValue;

        StartChangeHealth(true);
    }

    public void AddHealth()
    {
        if (_healthBar.value + _healthPerClick <= _healthBar.maxValue)
            _targetHealth = _healthBar.value + _healthPerClick;
        else
            _targetHealth = _healthBar.maxValue;

        StartChangeHealth(false);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat(_healthParameter, _totalHealth);
        _healthBar.maxValue = _totalHealth;
        _healthBar.value = _healthBar.maxValue;
    }

    private IEnumerator ChangeHealth(float targetHealth)
    {
        while (_healthBar.value != targetHealth)
        {
            _healthBar.value = Mathf.MoveTowards(_healthBar.value, targetHealth, _changeRate * Time.deltaTime);
            _animator.SetFloat(_healthParameter, _healthBar.value);
            yield return null;
        }
    }

    private void StartChangeHealth(bool isDamage)
    {
        if (_healthBar.value != _targetHealth)
        {
            StopChangeHealth();
            _changeHealthJob = StartCoroutine(ChangeHealth(_targetHealth));

            if (isDamage)
                _animator.SetTrigger(_damageTrigger);
            else
                _animator.SetTrigger(_healthTrigger);
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
