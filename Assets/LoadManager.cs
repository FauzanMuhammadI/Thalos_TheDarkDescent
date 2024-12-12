using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public static LoadManager Instance; // Singleton instance

    private void Awake()
    {
        // Membuat instance jika belum ada
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Pastikan hanya ada satu instance
        }
    }

    public void LoadGameData()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (System.IO.File.Exists(path))
        {
            PlayerData data = SaveSystem.LoadPlayerData();
            if (data != null)
            {
                // Memuat scene terakhir
                SceneManager.LoadScene(data.sceneIndex);
                // Menunggu scene selesai dimuat
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode, PlayerData data)
    {
        // Memastikan event hanya dipanggil sekali
        SceneManager.sceneLoaded -= (scene, mode) => OnSceneLoaded(scene, mode, data);

        // Menunggu sebentar agar objek dimuat sebelum menerapkan data
        StartCoroutine(LoadPlayerDataAfterSceneLoad(data));
    }

    private IEnumerator LoadPlayerDataAfterSceneLoad(PlayerData data)
    {
        yield return new WaitForSeconds(1f); // Tunggu sejenak

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player == null)
        {
            Debug.LogError("Pemain tidak ditemukan di scene.");
            yield break;
        }

        Health healthComponent = player.GetComponent<Health>();
        if (healthComponent == null)
        {
            Debug.LogError("Komponen Health tidak ditemukan pada pemain.");
            yield break;
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
