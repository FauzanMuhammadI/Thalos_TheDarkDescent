using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject boss;
    public BossHealthBar bossHealthBar;
    public Animator anim;
    public AudioManager audioManager;

    private EnemyController enemyController;

    void Start()
    {
        currentHealth = maxHealth;
        bossHealthBar.SetBossMaxHealth(maxHealth);
        bossHealthBar.SetBossHealth(currentHealth);
        anim = GetComponent<Animator>();
        anim.SetBool("Dead", false);

        enemyController = GetComponent<EnemyController>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        bossHealthBar.SetBossHealth(currentHealth);
        anim.SetBool("Hit", true);

        if (enemyController != null)
        {
            enemyController.TriggerHitAnimation();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        anim.SetBool("Dead", true);
        anim.SetBool("Walking", false);
        GetComponent<Collider>().enabled = false;
        this.enabled = false;

        StartCoroutine(DestroyAfterAnimation());
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        bossHealthBar.SetBossMaxHealth(maxHealth);
        bossHealthBar.SetBossHealth(currentHealth);

        if (boss != null)
        {
            boss.SetActive(false);
        }
        this.enabled = true;
        Debug.Log("Boss health has been reset!");
    }

    public void PlayAttackAudio()
    {
        if (audioManager != null && audioManager.SFXEnemy != null)
        {
            audioManager.SFXEnemy.clip = audioManager.phobosHit;
            audioManager.SFXEnemy.Play();
        }
    }

}
