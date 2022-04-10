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

    private Coroutine _changeHealthJob;
    private Animator _animator;

    public void StartChangeHealth(bool isDamage, float targetHealth)
    {
        if (_healthBar.value != targetHealth)
        {
            StopChangeHealth();
            _changeHealthJob = StartCoroutine(ChangeHealth(targetHealth));

            if (isDamage)
                _animator.SetTrigger(AnimatorParams._damageTrigger);
            else
                _animator.SetTrigger(AnimatorParams._healthTrigger);
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat(AnimatorParams._healthParameter, _healthBar.value);
        _healthBar.maxValue = _health.MaxHealth;
        _healthBar.value = _healthBar.maxValue;
    }

    private IEnumerator ChangeHealth(float targetHealth)
    {
        while (_healthBar.value != targetHealth)
        {
            _healthBar.value = Mathf.MoveTowards(_healthBar.value, targetHealth, _changeRate * Time.deltaTime);
            _animator.SetFloat(AnimatorParams._healthParameter, _healthBar.value);
            yield return null;
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
