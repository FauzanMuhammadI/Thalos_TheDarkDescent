using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayer(PlayerController player, Health healthComponent)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player, healthComponent);
        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log($"Data pemain berhasil disimpan: Scene: {data.sceneIndex}, Posisi: ({data.position[0]}, {data.position[1]}, {data.position[2]}), Health: {data.health}");
    }

    public static PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            Debug.Log($"Data yang dimuat: Scene: {data.sceneIndex}, Posisi: ({data.position[0]}, {data.position[1]}, {data.position[2]}), Health: {data.health}");
            return data;
        }
        else
        {
            Debug.LogError("Save file tidak ditemukan di " + path);
            return null;
        }
    }
}
