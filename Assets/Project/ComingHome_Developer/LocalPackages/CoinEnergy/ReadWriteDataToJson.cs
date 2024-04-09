using System.Collections.Generic;
using UnityEngine;

public class ReadWriteDataToJson : MonoBehaviour
{
    public static ReadWriteDataToJson instance;
    public string moduleName;

    [SerializeField]
    private int requiredCoinToBuyWizbux;
    [Header("To provide free Wizbux/Coins to user at initial stage")]
    [SerializeField]
    int freeWizbux;
    [SerializeField]
    int freeCoins;

    DataCollector dataCollector;
    UnlockedLevelCollector unlockedLevelCollector;

    string jsonData, unlockedLevelJsonData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dataCollector = new DataCollector();
        dataCollector.availableCompoList = new List<string>();
        dataCollector.modelColor = new List<string>();

        unlockedLevelCollector = new UnlockedLevelCollector();
        GetDataFromJson();
        GetUnlockedLevelFromDB();
    }

    //to write data in to json and save to player prefs
    void WriteDataToJson()
    {
        jsonData = JsonUtility.ToJson(dataCollector);
        PlayerPrefs.SetString(moduleName, jsonData);
        //Debug.Log("json writed "+ jsonData);
    }

    //to get data from json if json is empty then initialise basic data to json
    void GetDataFromJson()
    {
        if (PlayerPrefs.HasKey(moduleName))
        {
            jsonData = PlayerPrefs.GetString(moduleName);

            dataCollector = JsonUtility.FromJson<DataCollector>(jsonData);

            dataCollector.collectedCoins = 0;
            WriteDataToJson();
        }
        else
        {
            InitialiseDataInJson();
        }
    }

    //to initialise basic data
    void InitialiseDataInJson()
    {
        dataCollector.totalCoin = freeCoins;
        dataCollector.collectedCoins = 0;
        dataCollector.totalWizbux = freeWizbux;
        dataCollector.minuteValue = 0;
        dataCollector.secondValue = 0;
        dataCollector.isLevelPurchased = false;

        WriteDataToJson();
    }

    #region CoinManager
    public int GetTotalCoin()
    {
        if (ParentModuleCollaborator.Instance != null)
        {
            return ParentModuleCollaborator.Instance.GetTotalGems();
        }
        else
        {
            return dataCollector.totalCoin;
        }
    }

    public int GetCollectedCoin()
    {
        if (ParentModuleCollaborator.Instance != null)
        {
            return ParentModuleCollaborator.Instance.GetCollectedGems();
        }
        else
        {
            return dataCollector.collectedCoins;
        }
    }

    public void AddCoin(int coinsToAdd)
    {
        if (ParentModuleCollaborator.Instance != null)
        {
            ParentModuleCollaborator.Instance.AddGems(coinsToAdd);
        }
        else
        {
            dataCollector.totalCoin += coinsToAdd;
            dataCollector.collectedCoins += coinsToAdd;
            WriteDataToJson();
        }
    }

    public bool PayCoin(int coinsToPay)
    {
        if (ParentModuleCollaborator.Instance != null)
        {
            return ParentModuleCollaborator.Instance.PayGems(coinsToPay);
        }
        else
        {
            if (dataCollector.totalCoin >= coinsToPay)
            {
                dataCollector.totalCoin -= coinsToPay;
                WriteDataToJson();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    #endregion

    #region Component manager
    public void SetComponentOnDB(string compoName, bool status)
    {
        if (ParentModuleCollaborator.Instance != null)
        {
            ParentModuleCollaborator.Instance.SetComponentOnDB(compoName, status);
        }
        else
        {
            if (status)
            {
                if (!dataCollector.availableCompoList.Contains(compoName))
                {
                    dataCollector.availableCompoList.Add(compoName);
                }
            }
            else
            {
                if (dataCollector.availableCompoList.Contains(compoName))
                {
                    dataCollector.availableCompoList.Remove(compoName);
                }
            }
            WriteDataToJson();
        }
    }

    public bool GetCompoStatus(string compoName)
    {
        if (ParentModuleCollaborator.Instance != null)
        {
            return ParentModuleCollaborator.Instance.GetCompoStatus(compoName);
        }
        else
        {
            return (dataCollector.availableCompoList.Contains(compoName));
        }
    }
    #endregion

    #region Color Manager

    public void InitialiseColorList(int listLength)
    {
        if (ParentModuleCollaborator.Instance != null)
        {
            ParentModuleCollaborator.Instance.InitialiseColorList(listLength);
        }
        else
        {
            if (dataCollector.modelColor.Count == 0)
            {
                for (int i = 0; i < listLength; i++)
                {
                    dataCollector.modelColor.Add(Color.black.ToString());
                }
                WriteDataToJson();
            }
        }
    }

    public void StoreColor(Color color, int index)
    {
        if (ParentModuleCollaborator.Instance != null)
        {
            ParentModuleCollaborator.Instance.StoreColor(color, index);
        }
        else
        {
            dataCollector.modelColor[index] = color.ToString();
        }
    }

    public Color GetColor(int index)
    {
        if (ParentModuleCollaborator.Instance != null)
        {
            return ParentModuleCollaborator.Instance.GetColor(index);
        }
        else
        {
            string currStr = dataCollector.modelColor[index];
            currStr = currStr.Replace("RGBA(", "");
            currStr = currStr.Replace(")", "");
            string[] rgbaStr = currStr.Split(","[0]);
            Color currColor = Color.black;
            for (int j = 0; j < rgbaStr.Length; j++)
            {
                currColor[j] = System.Single.Parse(rgbaStr[j]);
            }
            return currColor;
        }
    }

    public void StoreAllColorToDb()
    {
        WriteDataToJson();
    }

    #endregion

    #region Unlock Level

    //To store unlocked level to json
    void WriteUnlockedLevelToJson()
    {
        unlockedLevelJsonData = JsonUtility.ToJson(unlockedLevelCollector);
        PlayerPrefs.SetString(moduleName+"UL", unlockedLevelJsonData);
        Debug.Log("UnlockLevelCollector---" + unlockedLevelJsonData);
    }

    void GetUnlockedLevelFromDB()
    {
        if (PlayerPrefs.HasKey(moduleName + "UL"))
        {
            unlockedLevelJsonData = PlayerPrefs.GetString(moduleName + "UL");
            Debug.Log("UnlockedLevelJsondata---" + unlockedLevelJsonData);
            unlockedLevelCollector = JsonUtility.FromJson<UnlockedLevelCollector>(unlockedLevelJsonData);
        }
        else
        {
            InitialisedUnlockedLevel();
        }
    }

    void InitialisedUnlockedLevel()
    {
        //unlockedLevelCollector.UnlockedLevelCount = 0;
        unlockedLevelCollector.prevUnlockedLevel = 0;
        SetUnlockedLevelToDB(0);
    }
    //-----------------------------------------

    public void SetUnlockedLevelToDB(int levelCount)
    {
        //if (ParentModuleCollaborator.Instance != null)
        //{
        //    ParentModuleCollaborator.Instance.SetUnlockedLevelToDB(levelCount);
        //}
        //else 
        Debug.Log("Label Unlock-----" + levelCount);
        if(unlockedLevelCollector.UnlockedLevelCount < levelCount)
        {
            //PlayerPrefs.GetString("PrevUnlockLevel").Equals("False");
            unlockedLevelCollector.UnlockedLevelCount = levelCount;
            WriteUnlockedLevelToJson();
        }
    }

    public int GetUnlockedLevel()
    {
        //if (ParentModuleCollaborator.Instance != null)
        //{
        //    return ParentModuleCollaborator.Instance.GetUnlockedLevel();
        //}
        //else
        {
            return unlockedLevelCollector.UnlockedLevelCount;
        }
    }

    public void SetPreviousUnlockedLevel()
    {
        //    if (ParentModuleCollaborator.Instance != null)
        //    {
        //        ParentModuleCollaborator.Instance.SetPreviousUnlockedLevel();
        //    }
        //    else
        {
            //PlayerPrefs.GetString("PrevUnlockLevel").Equals("True");
            unlockedLevelCollector.prevUnlockedLevel = unlockedLevelCollector.UnlockedLevelCount;
            Debug.Log("Previous Level---" + SEC_1.GameManuUIManager_CharacterHandler.instance.prevUnlockLevel + " UnlockLevel----" + SEC_1.GameManuUIManager_CharacterHandler.instance.unlockLevel);
            WriteUnlockedLevelToJson();
        }
    }

    public int GetPreviousUnlockedLevel()
    {
        Debug.Log("Previous Unlock" + SEC_1.GameManuUIManager_CharacterHandler.instance.prevUnlockLevel);
        //if (ParentModuleCollaborator.Instance != null)
        //{
        //    return ParentModuleCollaborator.Instance.GetPreviousUnlockedLevel();
        //}
         return unlockedLevelCollector.prevUnlockedLevel;
    }

    #endregion

    #region Wizbux Manager

    public bool BuyWizbux(int requiredWizbux)
    {
        //if (ParentCoinManager.INSTANCE != null)
        //{
        //    return ParentCoinManager.INSTANCE.BuyWizbux(requiredWizbux);
        //}
        //else
        {
            int requiredCoin = requiredCoinToBuyWizbux * requiredWizbux;
            if (requiredCoin <= dataCollector.totalCoin)
            {
                dataCollector.totalWizbux += requiredWizbux;
                dataCollector.totalCoin -= requiredCoin;
                WriteDataToJson();
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool PayWizbux(int amount)
    {
        if(ParentCoinManager.INSTANCE != null)
        {
            //if (ParentCoinManager.INSTANCE.GetWizBux() >= amount)
            //{
            //    ParentCoinManager.INSTANCE.SetWizBux(-amount);
            //    return true;
            //}
            return false;
        }
        else
        {
            if (dataCollector.totalWizbux >= amount)
            {
                dataCollector.totalWizbux -= amount;
                WriteDataToJson();
                return true;
            }
            else
            {
                return false;
            } 
        }
    }

    public void AddWizbux(int wizbux)
    {
        dataCollector.totalWizbux += wizbux;
        WriteDataToJson();
    }
    public int GetTotalWizbux()
    {
        if (ParentCoinManager.INSTANCE != null)
        {
            return ParentCoinManager.INSTANCE.GetWizBux();
        }
        else
        {
            return dataCollector.totalWizbux;
        }
    }

    #endregion

    #region Time manager
    public void SetTimer(int minutes, int seconds)
    {
        if (ParentModuleCollaborator.Instance != null)
        {
            ParentModuleCollaborator.Instance.SetTimer(minutes, seconds);
        }
        else
        {
            dataCollector.minuteValue = minutes;
            dataCollector.secondValue = seconds;
            WriteDataToJson();
        }
    }

    public int GetMinuteValue()
    {
        if (ParentModuleCollaborator.Instance != null)
        {
            return ParentModuleCollaborator.Instance.GetMinuteValue();
        }
        else
        {
            return dataCollector.minuteValue;
        }
    }

    public int GetSecondValue()
    {
        if (ParentModuleCollaborator.Instance != null)
        {
            return ParentModuleCollaborator.Instance.GetSecondValue();
        }
        else
        {
            return dataCollector.secondValue;
        }
    }
    #endregion

    #region LevelPurchased

    public bool IsLevelPurchased()
    {
        return dataCollector.isLevelPurchased;
    }

    public bool PurchaseLevelsSuccessfully(int requiredWizbux)
    {
        dataCollector.isLevelPurchased = PayWizbux(requiredWizbux);
        WriteDataToJson();
        return dataCollector.isLevelPurchased;
    }

    #endregion

}

[System.Serializable]
public class DataCollector
{
    public bool isLevelPurchased;
    public int totalCoin, collectedCoins, totalWizbux, minuteValue, secondValue;
    public List<string> modelColor;
    public List<string> availableCompoList;
}

[System.Serializable]
public class UnlockedLevelCollector {
    public string userId = "ComingHome_MVW";
    public int UnlockedLevelCount, prevUnlockedLevel;
}

