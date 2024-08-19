using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 100f;  // Maximum health value
    public float currentHealth;     // Current health value

    void Start()
    {
        // Initialize health to maximum health at the start
        currentHealth = maxHealth;
    }

    // Method to apply damage to the character
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        // Ensure health doesn't drop below zero
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        // Notify other systems that health has changed
        NotificationsManager.Instance.PostNotification("OnHealthChanged", gameObject, currentHealth);
    }

    // Method to heal the character
    public void Heal(float amount)
    {
        currentHealth += amount;

        // Ensure health doesn't exceed max health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Notify other systems that health has changed
        NotificationsManager.Instance.PostNotification("OnHealthChanged", gameObject, currentHealth);
    }

    // Method to handle character death
    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");

        // Notify other systems that the character has died
        NotificationsManager.Instance.PostNotification("OnCharacterDied", gameObject);

        // Optionally, you can destroy the object or disable it
        // Destroy(gameObject);
        // gameObject.SetActive(false);
    }

    // Optional: Method to reset health
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        NotificationsManager.Instance.PostNotification("OnHealthChanged", gameObject, currentHealth);
    }
}
