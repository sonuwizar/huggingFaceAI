using System.Collections.Generic;
using UnityEngine;
using Addressable;
/// <summary>
/// To get environments list form backend data.
/// To download/Load selected environments.
/// </summary>
public class EnvironmentsDownloadManager : MonoBehaviour
{
    public static EnvironmentsDownloadManager instance;

    internal string mvwEnvironmentName="cominghome_mvw";
    internal List<string> labEnvNameList = new List<string>(), questEnvNameList = new List<string>();

    internal List<Lab> labList = new List<Lab>();

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //public void SetEnvLocal()
    //{
    //    labEnvNameList.Add("science_lab");
    //    questEnvNameList.Add("mazemadness_qa");
    //    questEnvNameList.Add("obstaclecourse_qa");
    //    questEnvNameList.Add("escaperoom_qa");
    //    questEnvNameList.Add("quizgame_qa");
    //}


    public void LoadAndSetMVW(LearnVerseTrail learnverseTrail)
    {
        mvwEnvironmentName = learnverseTrail.SelectedMultiVerse.EnvAssetName;
        LoadModule(mvwEnvironmentName);
    }

    //public IEnumerator SetEnvListToLoad_Ienumrator(LearnVerseTrail learnverseTrail)
    //{

    //    if (labList.Count > 0)
    //    {
    //        labList.Clear();
    //    }

    //    //Stored labs from selected master virtual world
    //    for (int i = 0; i < learnverseTrail.Labcontent.Count; i++)
    //    {
    //        labList.Add(learnverseTrail.Labcontent[i]);
    //        labEnvNameList.Add(learnverseTrail.Labcontent[i].EnvAssetName);
    //    }

    //    //Stored quests from labs
    //    for (int i = 0; i < labList.Count; i++)
    //    {
    //        questEnvNameList.Add(labList[i].Quests[0].Enviorment.assetName);
    //        Debug.Log("Quest name- " + labList[i].Quests[0].Enviorment.assetName);
    //        yield return StartCoroutine(BackendServicesHandler.instance.GetQuest_Ienumrator(labList[i].Quests[0].Id));
    //    }
        
    //}

    public void LoadModule(string environmentName)
    {
        string moduleUrl = LinkAndNames.GetCatalogFileUrl(environmentName);
        Initialize_AddressableScene.instance.LoadModuleUsingPath(LinkAndNames.GetCatalogFileUrl(environmentName));
    }
}
