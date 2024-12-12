using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUII : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private Image imageChar;
    [SerializeField] private Image dialogBox;
    [SerializeField] private TextMeshProUGUI textSentence;

    [SerializeField] private float textTypingSpeed;
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private GameObject panel;

    private int _index;
    public bool _isDialogActive = false;
    private DialogSentenceDataSO dialogSentenceDataSO;

    private void Update()
    {
        if (!_isDialogActive)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (textSentence.text == dialogSentenceDataSO.sentences[_index].sentence)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textSentence.text = dialogSentenceDataSO.sentences[_index].sentence;
            }
        }
    }

    public void StartDialog(DialogSentenceDataSO data)
    {
        dialogSentenceDataSO = data;
        _index = 0;
        textSentence.text = string.Empty;

        dialogueUI.SetActive(true);
        panel.SetActive(true);
        imageChar.gameObject.SetActive(true);
        dialogBox.gameObject.SetActive(true);

        var currentSentence = dialogSentenceDataSO.sentences[_index];
        textName.text = currentSentence.charName;
        textName.color = currentSentence.colorText;
        imageChar.sprite = currentSentence.charSprite;
        dialogBox.sprite = currentSentence.dialogBox;

        StartCoroutine(TypeLine());

        _isDialogActive = true;

        // Freeze time
        Time.timeScale = 0f;
    }

    public void NextLine()
    {
        if (_index < dialogSentenceDataSO.sentences.Length - 1)
        {
            _index++;
            textSentence.text = string.Empty;

            var currentSentence = dialogSentenceDataSO.sentences[_index];
            textName.text = currentSentence.charName;
            textName.color = currentSentence.colorText;
            imageChar.sprite = currentSentence.charSprite;
            dialogBox.sprite = currentSentence.dialogBox;

            StartCoroutine(TypeLine());
        }
        else
        {
            dialogueUI.SetActive(false);
            panel.SetActive(false);
            imageChar.gameObject.SetActive(false);
            dialogBox.gameObject.SetActive(false);

            _isDialogActive = false;

            // Unfreeze time
            Time.timeScale = 1f;
        }
    }

    IEnumerator TypeLine()
    {
        foreach (var c in dialogSentenceDataSO.sentences[_index].sentence.ToCharArray())
        {
            textSentence.text += c;
            textSentence.color = dialogSentenceDataSO.sentences[_index].colorText;
            yield return new WaitForSecondsRealtime(textTypingSpeed); // Use WaitForSecondsRealtime to respect time freeze
        }
    }

    public bool IsDialogActive
    {
        get { return _isDialogActive; }
    }
}
