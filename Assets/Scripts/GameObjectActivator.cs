using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectActivator : MonoBehaviour
{
    [SerializeField] private GameObject gameObject1;
    [SerializeField] private GameObject gameObject2;
    [SerializeField] private Image imageToActivate;
    [SerializeField] private Image tipsToActivate;
    [SerializeField] private float moveDuration = 1f;

    private Vector2 initialPosition = new Vector2(1414, 365); 
    private Vector2 targetPosition = new Vector2(600, 365); 

    private void Start()
    {
        if (tipsToActivate != null)
        {
            RectTransform rt = tipsToActivate.GetComponent<RectTransform>();
            rt.anchoredPosition = initialPosition;
        }
    }

    private void Update()
    {
        if (gameObject1 != null && gameObject1.activeSelf)
        {
            if (gameObject2 != null && !gameObject2.activeSelf)
            {
                gameObject2.SetActive(true);
            }

            if (imageToActivate != null && !imageToActivate.gameObject.activeSelf)
            {
                imageToActivate.gameObject.SetActive(true);
            }

            if (tipsToActivate != null && !tipsToActivate.gameObject.activeSelf)
            {
                tipsToActivate.gameObject.SetActive(true);
                StartCoroutine(SlideInTips());
            }
        }
    }

    private IEnumerator SlideInTips()
    {
        float elapsedTime = 0f;
        RectTransform rt = tipsToActivate.GetComponent<RectTransform>();
        Vector2 startingPos = rt.anchoredPosition;

        while (elapsedTime < moveDuration)
        {
            rt.anchoredPosition = Vector2.Lerp(startingPos, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rt.anchoredPosition = targetPosition;
    }
}
