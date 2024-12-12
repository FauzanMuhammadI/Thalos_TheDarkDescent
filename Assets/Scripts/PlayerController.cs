using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float groundDist;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public Animator anim;
    public Ghost ghost;
    public AudioManager audioManager;

    private bool isDashing = false;
    private bool canDash = true;

    public float dashingPower = 50f;
    public float dashingTime = 0.5f;
    public float dashingCooldown = 0.75f;
    public Vector3 lastMoveDirection;

    public ParticleSystem dust;
    private float lastFacingDirection = 1;

    private bool isWalking;

    [Header("Stamina Settings")]
    public int maxStamina = 100;
    private int currentStamina;
    public StaminaBar staminaBar;
    public float staminaRegenRate = 20f;

    public bool isDead { get; set; } = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);

        if (dust != null)
        {
            dust.Stop();
        }
    }

    void Update()
    {
        if (isDead)
        {
            ResetMovement();
            return; 
        }

        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash && !isDashing && currentStamina > 0)
        {
            StartCoroutine(Dash());
            PlayDashAudio();
        }

        if (!isDashing)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            lastMoveDirection = new Vector3(x, 0, y);

            if (lastMoveDirection.magnitude > 1)
            {
                lastMoveDirection = lastMoveDirection.normalized;
            }

            rb.velocity = lastMoveDirection * speed;

            anim.SetFloat("Horizontal", x != 0 ? x : lastFacingDirection);
            anim.SetFloat("Vertical", y);
            anim.SetFloat("Speed", Mathf.Clamp01(lastMoveDirection.magnitude));

            if (x != 0)
            {
                lastFacingDirection = Mathf.Sign(x);
            }

            isWalking = lastMoveDirection.magnitude > 0.1f;

            if (isWalking)
            {
                if (!dust.isPlaying)
                {
                    dust.Play();
                }
            }
            else
            {
                if (dust.isPlaying)
                {
                    dust.Stop();
                }
            }
        }

        if (!isDashing && currentStamina < maxStamina)
        {
            currentStamina = Mathf.Min(maxStamina, currentStamina + Mathf.CeilToInt(staminaRegenRate * Time.deltaTime));
            staminaBar.SetStamina(currentStamina);
        }

        if (ghost != null)
        {
            ghost.makeGhost = isDashing;
        }
    }


    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + lastMoveDirection * speed * Time.fixedDeltaTime);
    }

    public void PlayDashAudio()
    {
        if (audioManager != null && audioManager.SFXSource != null)
        {
            audioManager.SFXSource.clip = audioManager.dash;
            audioManager.SFXSource.Play();
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 dashDirection = new Vector3(x, 0, y).normalized;

        rb.velocity = dashDirection * dashingPower;

        float staminaLossRate = maxStamina / dashingTime;
        float elapsedTime = 0f;

        while (elapsedTime < dashingTime)
        {
            elapsedTime += Time.deltaTime;
            currentStamina = Mathf.Max(0, currentStamina - Mathf.CeilToInt(staminaLossRate * Time.deltaTime));
            staminaBar.SetStamina(currentStamina);

            yield return null;
        }

        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void ResetMovement()
    {
        if (rb != null)
            rb.velocity = Vector3.zero; 

        if (anim != null)
            anim.SetFloat("Speed", 0f); 
    }


    public void Respawn(Vector3 respawnPosition)
    {
        transform.position = respawnPosition;
        rb.velocity = Vector3.zero; 
        Debug.Log("Player respawned at checkpoint!");
    }

    public void PlayHitAudio()
    {
        if (audioManager != null && audioManager.SFXBackground != null)
        {
            audioManager.SFXBackground.clip = audioManager.characterHit;
            audioManager.SFXBackground.Play();
        }
    }

}

