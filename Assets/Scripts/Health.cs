using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth;
    public HealthBar healthBar;
    public int currentHealth;
    public TextMeshProUGUI healthBarValueText;

    [Header("Hit Effect")]
    public Animator anim;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Checkpoint and Game Management")]
    public GameManagerScript gameManager;
    public BossReset bossReset;
    public ErebosReset erebosReset;
    public ResetErebosPhase2 erebos2Reset;

    private bool isInvulnerable;
    private bool isDead;

    void Start()
    {
        isInvulnerable = false;
        isDead = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    void Update()
    {
        healthBarValueText.text = $"{currentHealth}/{maxHealth}";
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable || isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0 && !isDead)
        {
            StartCoroutine(HandleDeath());
            return;
        }

        HitEffect();
        StartCoroutine(Invulnerability());
        healthBar.SetHealth(currentHealth);

        Debug.Log($"Damage taken: {damage}, Current health: {currentHealth}");
    }

    private IEnumerator HandleDeath()
    {
        isDead = true;

        anim.SetBool("isDead", true);

        var playerController = GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.isDead = true;
            playerController.ResetMovement();
        }

        anim.SetTrigger("Dead");

        yield return new WaitForSeconds(1.5f);

        gameManager.gameOver();
    }

    public void RespawnPlayer()
    {
        Debug.Log("Player Respawning...");
        isDead = false;

        anim.SetBool("isDead", false);

        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);

        var playerController = GetComponent<PlayerController>();

        if (CheckpointController.HasCheckpoint())
        {
            var respawnPosition = CheckpointController.GetRespawnPosition();
            if (playerController != null)
            {
                playerController.isDead = false;
                playerController.Respawn(respawnPosition);
            }
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        bossReset?.ResetBoss();
        erebosReset?.ResetBoss();
        erebos2Reset?.ResetBoss();

        Debug.Log("Player respawned.");
    }

    private void HitEffect()
    {
        float horizontal = PlayerCombat.instance.animator.GetFloat("Horizontal");
        Vector3 scale = spriteRenderer.transform.localScale;

        if (horizontal == -1f)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else if (horizontal == 1f)
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        spriteRenderer.transform.localScale = scale;
        anim.SetTrigger("Hit");
        StartCoroutine(ResetSpriteScale());
    }

    private IEnumerator ResetSpriteScale()
    {
        yield return new WaitForSeconds(0.5f);

        Vector3 scale = spriteRenderer.transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        spriteRenderer.transform.localScale = scale;
    }

    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(1f);
        isInvulnerable = false;
    }

    public void AddMaxHealth(int value)
    {
        maxHealth += value;
        currentHealth = maxHealth;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    public void AddHealth(int value)
    {
        currentHealth += value;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
    }
}
