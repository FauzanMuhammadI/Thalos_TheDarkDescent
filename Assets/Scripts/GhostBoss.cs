using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBoss : MonoBehaviour
{
    public float ghostDelay;
    private float ghostDelaySeconds;
    public GameObject ghost;
    public bool makeGhost = false;

    private BossController bossController;

    void Start()
    {
        ghostDelaySeconds = ghostDelay;
        bossController = FindObjectOfType<BossController>(); // Mencari BossController di scene
    }

    void Update()
    {
        if (makeGhost)
        {
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                Vector3 ghostPosition = new Vector3(transform.position.x, transform.position.y - 6.5f, transform.position.z);
                Quaternion ghostRotation = Quaternion.Euler(60f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                GameObject currentGhost = Instantiate(ghost, ghostPosition, ghostRotation);

                Animator ghostAnimator = currentGhost.GetComponent<Animator>();
                if (ghostAnimator != null && bossController != null)
                {
                    ghostAnimator.Play("GhostFadeOut"); 

                    SpriteRenderer ghostSprite = currentGhost.GetComponent<SpriteRenderer>();
                    if (ghostSprite != null)
                    {
                        ghostSprite.flipX = bossController.lastMoveDirection.x > 0;
                    }
                }

                ghostDelaySeconds = ghostDelay;
                Destroy(currentGhost, 1f);
            }
        }
    }
}
