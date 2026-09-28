using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField] private BounceController bounceController;
    private const string SaveFolderName = "savedBounces";
    private const string SaveFileName = "savedBounces.json";

    // PATHS
    private string GetSaveFolderPath()
    {
        return Path.Combine(Application.persistentDataPath, SaveFolderName);
    }
    private string GetSaveFilePath()
    {
        return Path.Combine(GetSaveFolderPath(), SaveFileName);
    }
    // HELPERS
    private void AssertDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            Debug.Log($"Directory created: {path}");
        }
    }
    private bool AssertFile(string path)
    {
        return File.Exists(path);
    }
    // --------------------------------------------------------------------
    // Save & Load Logic
    // --------------------------------------------------------------------
    public void Save()
    {
        string folderPath = GetSaveFolderPath();
        string filePath = GetSaveFilePath();

        AssertDirectory(folderPath);
        
        // 1. Get current state from the controller.
        BounceDto data = bounceController.CaptureState();

        // 2. Serialize DTO to JSON.
        string json = JsonUtility.ToJson(data, true);

        // 3. Write JSON to file.
        File.WriteAllText(filePath, json);

        // 4. Debug
        Debug.Log($"Saved successfully: {filePath}");
    }
    public void Load()
    {
        string filePath = GetSaveFilePath();

        if (AssertFile(filePath))
        {
            // 1. Read JSON from file
            string json = File.ReadAllText(filePath);

            // 2. Deserialize JSON into DTO.
            BounceDto data = JsonUtility.FromJson<BounceDto>(json);

            // 3. Restore state in the controller.
            bounceController.RestoreState(data);

            // 4. Debug
            Debug.Log($"Loaded successfully: {filePath}");
        }
        else
        {
            Debug.LogWarning($"Cannot load. Save file does not exist: {filePath}");
        }
    }
}
