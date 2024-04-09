using System.Collections;
using UnityEngine;
using Newtonsoft.Json;

public class BackendServicesHandler : MonoBehaviour
{
    public static BackendServicesHandler instance;

    [SerializeField] string masterVirtualWorldName;
    [SerializeField] LearnVerseTrail[] learnverseTrail;
    //[SerializeField] List<Lab> labs = new List<Lab>();

    static bool isLoaded;

    Quest[] questTrail /*= new Quest()*/;
    Lab _lab;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isLoaded)
        {
            return;
        }

        if (PlayerPrefs.HasKey("learnverseTrail"))
        {
            isLoaded = true;
            learnverseTrail = JsonConvert.DeserializeObject<LearnVerseTrail[]>(PlayerPrefs.GetString("learnverseTrail"));
            BackendDataInitialiser.instance.InitLearnverseTrailData(learnverseTrail[1]);
        }
        else
        {
            GetLearnverseTrail();
        }
    }

    #region GettingLearnverseTrail
    void GetLearnverseTrail()
    {
        isLoaded = true;
        ApiRequestHandler.instance.GetRequest((LinkAndNames.learningTrail_SchoolNameApiUrl + masterVirtualWorldName), AfterGettingLearnverseTrail);
    }

    void AfterGettingLearnverseTrail(string webRequestMsg, bool requestStatus)
    {
        if (requestStatus)
        {
            learnverseTrail = JsonConvert.DeserializeObject<LearnVerseTrail[]>(webRequestMsg);
            PlayerPrefs.SetString("learnverseTrail", webRequestMsg);

            BackendDataInitialiser.instance.InitLearnverseTrailData(learnverseTrail[1]);
        }
    }

    #endregion

    #region Getting Quest
    public void GetQuest(string questId)
    {
        StartCoroutine(GetQuest_Ienumrator(questId));
    }

    public IEnumerator GetQuest_Ienumrator(string questId)
    {
        ApiRequestHandler.instance.GetRequest((LinkAndNames.questTrailApiUrl + questId), AfterGettingQuest);
        yield return null;
    }

    void AfterGettingQuest(string webRequestMsg, bool requestStatus)
    {
        if (requestStatus)
        {
            Debug.Log("web msg- " + webRequestMsg);
            questTrail = JsonConvert.DeserializeObject<Quest[]>(webRequestMsg);
            PlayerPrefs.SetString("questTrail", webRequestMsg);
        }
    }
    #endregion

}
