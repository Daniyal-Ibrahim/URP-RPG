using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;
    [SerializeField] private float invulnerableTime;
    private bool _isVulnerable = true;
    [SerializeField] private Image bar;

    private void Start()
    {
        bar.fillAmount = (float)health/maxHealth;
    }

    private void DamageAmount(int value)
    {
        if (health - value > 0)
        {
            _isVulnerable = false;
            health -= value;
            bar.fillAmount = (float)health/ maxHealth;
            Invoke(nameof(MakeVulnerable),invulnerableTime);
        }
        else
        {
            bar.fillAmount = 0f;
            Death();
        }
    }

    private void MakeVulnerable()
    {
        _isVulnerable = true;
    }

    private void Death()
    {
        health = maxHealth;
    }

    public void Damage(int value)
    {
        if(_isVulnerable)
            DamageAmount(value);
    }
}