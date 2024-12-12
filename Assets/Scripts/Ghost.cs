using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float ghostDelay;
    private float ghostDelaySeconds;
    public GameObject ghost;
    public bool makeGhost = false;

    private PlayerController playerController;

    void Start()
    {
        ghostDelaySeconds = ghostDelay;
        playerController = FindObjectOfType<PlayerController>();
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
                Vector3 ghostPosition = new Vector3(transform.position.x, 0f, transform.position.z - 4f);
                Quaternion ghostRotation = Quaternion.Euler(60f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                Vector3 ghostScale = new Vector3(1.8f, 1.8f, 1.8f);

                GameObject currentGhost = Instantiate(ghost, ghostPosition, ghostRotation);
                currentGhost.transform.localScale = ghostScale;

                Animator ghostAnimator = currentGhost.GetComponent<Animator>();
                if (ghostAnimator != null && playerController != null)
                {
                    if (playerController.lastMoveDirection.z > 0)
                    {
                        ghostAnimator.Play("GhostFadeOutUp");
                    }
                    else if (playerController.lastMoveDirection.z < 0)
                    {
                        ghostAnimator.Play("GhostFadeOutDown");
                    }
                    else
                    {
                        ghostAnimator.Play("GhostFadeOut");

                        SpriteRenderer ghostSprite = currentGhost.GetComponent<SpriteRenderer>();
                        if (ghostSprite != null)
                        {
                            ghostSprite.flipX = playerController.lastMoveDirection.x == 1;
                        }
                    }
                }

                ghostDelaySeconds = ghostDelay;
                Destroy(currentGhost, 1f);
            }
        }
    }
}
