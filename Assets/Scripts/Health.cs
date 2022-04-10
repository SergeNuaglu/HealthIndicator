using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]

public class Health : MonoBehaviour
{
    [SerializeField] private float _totalHealth;

    private float _currentHealth;

    public float TotalHealth => _totalHealth;
    public float CurrentHealth => _currentHealth;

    private void Start()
    {
        _currentHealth = _totalHealth;
    }

    public void SetCurrentHealth(float health)
    {
        _currentHealth = health;
    }
}
