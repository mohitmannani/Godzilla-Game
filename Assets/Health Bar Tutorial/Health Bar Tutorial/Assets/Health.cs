using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Text healthText;
    public Image healthBar, ringHealthBar;
    public Image[] healthPoints; // Assuming you have 10 health points

    int totalHealthPoints = 10;
    int currentHealthPoints;

    float maxHealth = 100;
    float health, lerpSpeed;
    [SerializeField] private float regenerationRate = 5f; // Adjust the rate as needed
    private bool isRegenerating = false;

    private void Start()
    {
        health = maxHealth;
        currentHealthPoints = totalHealthPoints;
        UpdateHealthUI();
        StartCoroutine(RegenerateOverTime());
    }

    private void Update()
    {
        healthText.text = "Health: " + Mathf.Round(health) + "%";

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        lerpSpeed = 3f * Time.deltaTime;

        HealthBarFiller();
        ColorChanger();
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (health / maxHealth), lerpSpeed);
        ringHealthBar.fillAmount = Mathf.Lerp(ringHealthBar.fillAmount, (health / maxHealth), lerpSpeed);

        for (int i = 0; i < totalHealthPoints; i++)
        {
            if (i < healthPoints.Length) // Check if the index is within the array bounds
            {
                healthPoints[i].enabled = (i < currentHealthPoints);
            }
        }
    }

    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));
        healthBar.color = healthColor;
        ringHealthBar.color = healthColor;
    }

    public void TakeDamage(float damagePoints)
    {
        if (health > 0)
        {
            health -= damagePoints;
            UpdateHealthUI();

            // Deduct health points visually
            if (currentHealthPoints > 0)
            {
                currentHealthPoints--;
            }

            if (health <= 0)
            {
                // Perform any actions when the player dies
                StartCoroutine(Die());
            }
        }
    }

    public void Heal(float healingPoints)
    {
        if (health < maxHealth)
        {
            health += healingPoints;

            // Increase health points visually
            if (currentHealthPoints < totalHealthPoints)
            {
                currentHealthPoints++;
            }

            UpdateHealthUI();
        }
    }

    void UpdateHealthUI()
    {
        healthText.text = "Health: " + Mathf.Round(health) + "%";
        Debug.Log("Updating Health UI: " + healthText.text);
    }

    IEnumerator Die()
    {
        // Perform death animations or actions
        Debug.Log("Player has died!");

        // Wait for 2 seconds before additional death actions
        yield return new WaitForSeconds(2f);

        // Additional death actions can be added here

        // Restart the level or perform any other actions
        // For example, you can use SceneManager.LoadScene("YourSceneName");
    }

    IEnumerator RegenerateOverTime()
    {
        isRegenerating = true;

        while (isRegenerating)
        {
            if (health < maxHealth)
            {
                health += Time.deltaTime * regenerationRate;
                UpdateHealthUI();
            }

            yield return null;
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(RegenerateOverTime());
    }
}
