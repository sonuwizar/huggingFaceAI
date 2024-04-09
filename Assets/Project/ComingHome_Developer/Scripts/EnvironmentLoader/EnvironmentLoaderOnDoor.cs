using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Used to get portals references from modules.
/// Assign other modules references to each portals.
/// </summary>
public class EnvironmentLoaderOnDoor : MonoBehaviour
{
    public static EnvironmentLoaderOnDoor instance;

    public EnvironmentDoorData[] environmentDoorData;
    public EnvironmentDoorData prevEnvironmentDoorData;

    public enum EnvironmentType {MasterVirtualWorld, Lab, QuestGame};
    public EnvironmentType environmentType;


    private void Awake()
    {
        instance = this;
        Debug.Log("EnvironmentLoaderOnDoor");
    }

    private void Start()
    {
        if (prevEnvironmentDoorData != null && prevEnvironmentDoorData.door != null)
        {
            prevEnvironmentDoorData.door.SetActive(true);
        }

        if (PlayerPrefs.HasKey("CHMVW_Activity"))
        {
            BackendDataInitialiser.instance.SetCurrSession(PlayerPrefs.GetInt("CHMVW_Activity")-1);
        }

        switch (environmentType)
        {
            case EnvironmentType.MasterVirtualWorld:
                if (BackendDataInitialiser.instance.currSessionIndex < 0 || BackendDataInitialiser.instance.currSessionLabData == null)   //for intro sene
                {
                    environmentDoorData[0].environmentNameToLoad = BackendDataInitialiser.instance.labEnvironmentAssetName_Introduction;
                }
                else  //After intro scene
                {
                    environmentDoorData[0].environmentNameToLoad = BackendDataInitialiser.instance.currSessionLabData[0].EnvAssetName;
                }
                break;
            case EnvironmentType.Lab:
                if (BackendDataInitialiser.instance.currSessionIndex >= 0)  //After intro scene
                {
                    BackendDataInitialiser.instance.InitCurrLabData();
                    InitQuest_ForSelectedAssignment(BackendDataInitialiser.instance.currAssignmentIndex);
                }
                break;
            case EnvironmentType.QuestGame:
                break;
            default:
                break;
        }
    }

    #region Init Quest
    public void InitQuest_ForSelectedAssignment(int assignmentIndex)
    {
        if(BackendDataInitialiser.instance.currSessionLabData!=null)
        InitQuest_ForSelectedAssignment(BackendDataInitialiser.instance.currSessionLabData[assignmentIndex].Quests);
    }

    void InitQuest_ForSelectedAssignment(List<Quest> quest)
    {
        for (int i = 0; i < environmentDoorData.Length; i++)
        {
            if (quest.Count > i)
            {
                environmentDoorData[i].environmentNameToLoad = quest[i].Enviorment.assetName;
                environmentDoorData[i].door.SetActive(true);
            }
            else
            {
                environmentDoorData[i].door.SetActive(false);
            }
        }
    }
    #endregion

    //public void InitEnvironments_OnPortals(List<string> environmentsName)
    //{
    //    Debug.Log("env name list- " + environmentsName[0]);
    //    for (int i = 0; i < environmentDoorData.Length; i++)
    //    {
    //        if (environmentsName.Count > i)
    //        {
    //            environmentDoorData[i].environmentNameToLoad = environmentsName[i];
    //        }
    //        else
    //        {
    //            break;
    //        }
    //    }
    //}

    /// <summary>
    /// To load modules on triggering portals
    /// </summary>
    /// <param name="triggeredObject"></param>
    public void LoadEnvironment(GameObject triggeredObject)
    {
        if (prevEnvironmentDoorData!=null && prevEnvironmentDoorData.door!=null && prevEnvironmentDoorData.door.name.Equals(triggeredObject.name))
        {
            LoadPreviousEnvironment();
        }
        else
        {
            for (int i = 0; i < environmentDoorData.Length; i++)
            {
                if (environmentDoorData[i].door.name.Equals(triggeredObject.name))
                {
                    SaveEnvironmentToDb(i);
                    PlayerPrefs.SetString("environmentToLoad", environmentDoorData[i].environmentNameToLoad);
                    UnityEngine.SceneManagement.SceneManager.LoadScene("EnvironmentLoader");
                    break;
                }
            }
        }
    }

    void SaveEnvironmentToDb(int index)
    {
        switch (environmentType)
        {
            case EnvironmentType.MasterVirtualWorld:
                PlayerPrefs.SetInt("labindex", index);
                break;
            case EnvironmentType.Lab:
                PlayerPrefs.SetInt("questindex", index);
                break;
            default:
                break;
        }
    }

    internal void LoadPreviousEnvironment()
    {
        int index = 0;

        switch (environmentType)
        {
            case EnvironmentType.MasterVirtualWorld:
                break;
            case EnvironmentType.Lab:
                PlayerPrefs.SetString("environmentToLoad", EnvironmentsDownloadManager.instance.mvwEnvironmentName);
                break;
            case EnvironmentType.QuestGame:
                index = PlayerPrefs.GetInt("labindex");
                PlayerPrefs.SetString("environmentToLoad", BackendDataInitialiser.instance.currSessionLabData[BackendDataInitialiser.instance.currSessionIndex].EnvAssetName);
                break;
            default:
                break;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("EnvironmentLoader");
    }
}

[System.Serializable]
public class EnvironmentDoorData
{
    public GameObject door;
    public string environmentNameToLoad;
}
