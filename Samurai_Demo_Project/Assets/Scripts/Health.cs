using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private Animator animator;
    [SerializeField] private HealthBar healthBar;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void DealDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            isDie();
            Die();
        }
    }

    // it return bool for player's prevent click buttons in die animation
    public bool isDie()
    {
        return true;
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " is died");

        animator.SetBool("isDie", true);

        Destroy(gameObject, 1f);
    }
}
