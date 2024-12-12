using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSentenceDataSO", menuName = "DialogueSystem/DialogueSentenceData")]
public class DialogSentenceDataSO : ScriptableObject
{
    public Sentence[] sentences;
}

[Serializable]
public class Sentence
{
    public string charName;
    public Color colorText = Color.white;
    public Sprite charSprite;
    public Sprite dialogBox;
    [TextArea(10, 5)]
    public string sentence;

}
