using System.Collections.Generic;
using UnityEngine;

public class BackendDataInitialiser : MonoBehaviour
{
    public static BackendDataInitialiser instance;

    List<Lab> LabcontentList;
    Lab _lab;

    Dictionary<string, List<Lab>> labDataForSessions = new Dictionary<string, List<Lab>>();

    public string labEnvironmentAssetName_Introduction = "science_lab";

    internal int currAssignmentIndex, currSessionIndex;

    string sessionName = "Session";
    internal List<Lab> currSessionLabData;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    internal void InitLearnverseTrailData(LearnVerseTrail learnverseTrail)
    {
        LabcontentList = learnverseTrail.Labcontent;
        GroupLabDataForSessions();
        EnvironmentsDownloadManager.instance.LoadAndSetMVW(learnverseTrail);
    }

    #region Managed and Grouped lab content based on sessions

    //To group labs based on sessions
    void GroupLabDataForSessions()
    {
        foreach (var labItem in LabcontentList)
        {
            if (labDataForSessions.ContainsKey(labItem.Chapter))//add lab for existing sessions
            {
                labDataForSessions[labItem.Chapter].Add(labItem);
            }
            else//add lab for new session
            {
                List<Lab> tempLabList = new List<Lab>();
                tempLabList.Add(labItem);
                labDataForSessions.Add(labItem.Chapter, tempLabList);
            }
        }
    }

    public List<Lab> GetLabListForSession(int sessionIndex)
    {
        Debug.Log("Session index- " + sessionIndex);
        string sessionName = this.sessionName + " " + (sessionIndex + 1);
        if (labDataForSessions.ContainsKey(sessionName))
        {
            return labDataForSessions[sessionName];
        }
        return null;
    }

    #endregion

    #region Init Current Lab Data

    public void SetCurrSession(int index)
    {
        currSessionIndex = index;
        currSessionLabData = null;
        currSessionLabData = GetLabListForSession(currSessionIndex);
        Debug.Log("currSession- " + currSessionLabData != null);
    }

    public void InitCurrLabData()
    {
        if (currSessionLabData != null)
        {
            //MiniScreenManager.instance.Init_Pdfs(currSessionLabData);
            //TVPocHandler.instance.InitVideos(currSessionLabData);
            //WebActivity_UIHandler.instance.InitWebActivity(currSessionLabData);
        }
    }

    public Dictionary<int, List<InGameLearningMods>> GetQuestLevels()
    {
        return currSessionLabData[currAssignmentIndex].Quests[0].Levels;
    }
    #endregion
}
