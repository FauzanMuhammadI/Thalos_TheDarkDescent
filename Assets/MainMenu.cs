using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Memulai permainan (start new game)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/player.fun";

        // Pastikan file save ada
        if (System.IO.File.Exists(path))
        {
            PlayerData data = SaveSystem.LoadPlayerData();

            // Periksa apakah data berhasil dimuat
            if (data != null)
            {
                // Memuat scene terakhir
                SceneManager.LoadScene(data.sceneIndex);

                // Tunggu hingga scene selesai dimuat
                SceneManager.sceneLoaded += (scene, mode) => OnSceneLoaded(scene, mode, data);
            }
            else
            {
                Debug.LogError("Gagal memuat data pemain.");
            }
        }
        else
        {
            Debug.Log("Tidak ada file save yang ditemukan!");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Kamu keluar dari game!");
        Application.Quit();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode, PlayerData data)
    {
        // Hapus event agar tidak dipanggil lebih dari sekali
        SceneManager.sceneLoaded -= (scene, mode) => OnSceneLoaded(scene, mode, data);

        // Pastikan pemain dan komponen Health ada sebelum digunakan
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player == null)
        {
            Debug.LogError("Pemain tidak ditemukan di scene.");
            return;
        }

        Health healthComponent = player.GetComponent<Health>();
        if (healthComponent == null)
        {
            Debug.LogError("Komponen Health tidak ditemukan pada pemain.");
            return;
        }

        // Terapkan posisi pemain
        Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
        player.transform.position = position;

        // Terapkan kesehatan pemain
        healthComponent.currentHealth = data.health;
        healthComponent.healthBar.SetHealth(data.health);

        Debug.Log($"Data diterapkan: Posisi ({position.x}, {position.y}, {position.z}), Health: {data.health}");
    }
}
