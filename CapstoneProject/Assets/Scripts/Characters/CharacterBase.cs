using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected static bool isAlive;
    protected static float movementSpeed;

    protected static float resource;
    protected float recoverResourceRate = 1.0f;
    protected float recoverRestedMultiplier = 4.0f;

    [SerializeField] protected float _maxHealth;
    protected float _health;

    protected static float StandardHeight = 2;
    protected static float CrouchedHeight = 1;

    protected static bool moving;
    protected static bool Rested;
    protected static bool slowed;

    public bool atFullHealth;

    protected float length = (StandardHeight - CrouchedHeight);

    protected Rigidbody rigidBody;
    protected Animator animator;
    protected CapsuleCollider capsule;

    public float GetHealthRatio()
    {
        return (_health / _maxHealth) * 100;
    }
    
    public void AddHealth(float value)
    {
        _health += value;

        if(_health >= _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    protected void RecoverResource()
    {
        if (Rested)
            resource += recoverResourceRate * recoverRestedMultiplier;
        else
            resource += recoverResourceRate;
    }

    protected void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0.0f)
        {
            _health = 0.0f;
            isAlive = false;
        }
    }
}