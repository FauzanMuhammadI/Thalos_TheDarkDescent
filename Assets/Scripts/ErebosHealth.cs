using System;
using System.Collections;
using UnityEngine;

public class ErebosHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject boss;
    public BossHealthBar bossHealthBar;
    private Animator anim;

    private ErebosController erebosController;
    public event Action<int> onHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
        bossHealthBar.SetBossMaxHealth(maxHealth);
        bossHealthBar.SetBossHealth(currentHealth);

        anim = GetComponent<Animator>();
        anim.SetBool("Dead", false);

        erebosController = GetComponent<ErebosController>();
    }

    public void TakeDamage(int damage)
    {
        //currentHealth -= damage;
        //currentHealth = Mathf.Max(currentHealth, 0); 
        //bossHealthBar.SetBossHealth(currentHealth);

        //if (erebosController != null)
        //{
        //    erebosController.TriggerHitAnimation();
        //}

        //if (currentHealth <= 0)
        //{
        //    Die();
        //}

        currentHealth -= damage;
        bossHealthBar.SetBossHealth(currentHealth);
        anim.SetBool("Hit", true);

        if (erebosController != null)
        {
            erebosController.TriggerHitAnimation();
        }

        if (currentHealth <= 0)
        {
            Die();
        }

        onHealthChanged?.Invoke(currentHealth);
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
        if (anim != null)
        {
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        }
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
}
