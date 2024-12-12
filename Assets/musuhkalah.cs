using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusuhKalah : MonoBehaviour
{
    public GameObject enemyContainer;  // Referensi ke GameObject yang menampung semua musuh
    public GameObject winPanel;        // Referensi ke panel kemenangan
    public Button nextLevelButton;     // Tombol untuk melanjutkan ke level berikutnya

    void Start()
    {
        // Pastikan panel kemenangan tidak aktif di awal
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        // Pastikan tombol 'NextLevelButton' disembunyikan pada awal
        if (nextLevelButton != null)
        {
            nextLevelButton.gameObject.SetActive(false);
            nextLevelButton.onClick.AddListener(NextLevel);
        }
        else
        {
            Debug.LogWarning("Next Level Button belum diatur di Inspector.");
        }
    }

    void Update()
    {
        if (enemyContainer == null)
        {
            Debug.LogError("enemyContainer tidak diatur!");
            return;
        }

        // Periksa jika semua musuh sudah dikalahkan
        if (enemyContainer.transform.childCount == 0)
        {
            Debug.Log("Kita menang!");
            ShowWinPanel();
        }
    }

    // Fungsi untuk menampilkan panel kemenangan
    void ShowWinPanel()
    {
        if (winPanel != null && !winPanel.activeSelf)
        {
            winPanel.SetActive(true);
        }

        // Menampilkan tombol "Lanjutkan ke Level Berikutnya"
        if (nextLevelButton != null)
        {
            nextLevelButton.gameObject.SetActive(true);
        }
    }

    // Fungsi untuk melanjutkan ke level berikutnya
    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Cek apakah level berikutnya ada, jika ada, muat level tersebut
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);

            // Menyimpan progress level jika perlu (misalnya, menggunakan PlayerPrefs)
            if (nextSceneIndex > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", nextSceneIndex);
            }
        }
        else
        {
            Debug.Log("Sudah mencapai level terakhir!");
            // Anda bisa menambahkan logika jika level sudah selesai (misalnya, kembali ke menu utama)
        }
    }
}
