using System.Collections;
using UnityEngine;

public class EnemiesDamaged : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator anim;

    private EnemyController enemyController;
    public AudioManager audioManager;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        anim.SetBool("Dead", false);

        enemyController = GetComponent<EnemyController>(); 
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
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

    public void PlayBatAudio()
    {
        if (audioManager != null && audioManager.SFXEnemy != null)
        {
            audioManager.SFXEnemy.clip = audioManager.bat;
            audioManager.SFXEnemy.Play();
        }
    }
    public void PlayBatDeadAudio()
    {
        if (audioManager != null && audioManager.SFXEnemy != null)
        {
            audioManager.SFXEnemy.clip = audioManager.batDead;
            audioManager.SFXEnemy.Play();
        }
    }
    public void PlaySlimeAudio()
    {
        if (audioManager != null && audioManager.SFXEnemy != null)
        {
            audioManager.SFXEnemy.clip = audioManager.slime;
            audioManager.SFXEnemy.Play();
        }
    }
    public void PlaySlimeDeadAudio()
    {
        if (audioManager != null && audioManager.SFXEnemy != null)
        {
            audioManager.SFXEnemy.clip = audioManager.slimeDead;
            audioManager.SFXEnemy.Play();
        }
    }
    public void PlayHitAudio()
    {
        if (audioManager != null && audioManager.SFXEnemy != null)
        {
            audioManager.SFXEnemy.clip = audioManager.enemyHit;
            audioManager.SFXEnemy.Play();
        }
    }
    public void PlayGolemDeadAudio()
    {
        if (audioManager != null && audioManager.SFXEnemy != null)
        {
            audioManager.SFXEnemy.clip = audioManager.golemDead;
            audioManager.SFXEnemy.Play();
        }
    }
}
