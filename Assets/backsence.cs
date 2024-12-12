using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backsence : MonoBehaviour
{
    public void GoToMainMenu()
    {
        // Pindah ke scene yang bernama "MainMenu"
        SceneManager.LoadScene("menu");
    }
}
