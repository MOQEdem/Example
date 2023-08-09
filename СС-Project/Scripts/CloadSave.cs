using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using System;
using UnityEngine.Scripting;

public class CloadSave : MonoBehaviour
{
    [SerializeField] private bool _isStartScene = false;

    private Dictionary<string, string> _data;

    public bool IsLoaded { get; private set; }

    private void Awake()
    {
        IsLoaded = false;

        _data = new Dictionary<string, string>();

        if (!_isStartScene)
            PlayerAccount.GetCloudSaveData(cloudSaveData => LoadData(cloudSaveData));
    }

    public void LoadStartSceneSave()
    {
        PlayerAccount.GetCloudSaveData(cloudSaveData => LoadData(cloudSaveData));
    }

    public string GetSavedData(string saveID)
    {
        string savedString;

        _data.TryGetValue(saveID, out savedString);

        return savedString;
    }

    public void SetSavedData(string saveID, string jsonPurchasedProductList)
    {
        _data.Remove(saveID);
        _data.Add(saveID, jsonPurchasedProductList);

        SaveData();
    }

    private void LoadData(string jsonString)
    {
        if (jsonString != null)
        {
            _data = JsonUtility.FromJson<SaveData>(jsonString).SavedData;

            if (!_data.ContainsKey(SaveID.levelProgress))
                SetNewData();
        }
        else
        {
            SetNewData();
        }

        IsLoaded = true;
    }

    private void SetNewData()
    {
        _data = new Dictionary<string, string>();

        GameProgress progress = new GameProgress();

        var jsonString = JsonUtility.ToJson(progress);

        _data.Add(SaveID.levelProgress, jsonString);
        _data.Add(SaveID.purchasedProduct, null);

        SaveData();
    }

    private void SaveData()
    {
        SaveData dataToSave = new SaveData();

        dataToSave.SavedData = _data;

        var jsonString = JsonUtility.ToJson(dataToSave);

        Debug.Log(jsonString);

        PlayerAccount.SetCloudSaveData(jsonString);
    }
}

public class SaveID
{
    public const string levelProgress = nameof(levelProgress);
    public const string purchasedProduct = nameof(purchasedProduct);
}

[Serializable]
public class SaveData
{
    [field: Preserve]
    [SerializeField]
    public string LevelProgress;

    [field: Preserve]
    [SerializeField]
    public string PurchasedProduct;

    public Dictionary<string, string> SavedData
    {
        get
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            data.Add(SaveID.levelProgress, LevelProgress);
            data.Add(SaveID.purchasedProduct, PurchasedProduct);
            return data;
        }
        set
        {
            value.TryGetValue(SaveID.levelProgress, out string levelProgress);
            LevelProgress = levelProgress;
            value.TryGetValue(SaveID.purchasedProduct, out string purchasedProduct);
            PurchasedProduct = purchasedProduct;
        }
    }
}