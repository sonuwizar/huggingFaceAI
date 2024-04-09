using UnityEngine;
using Addressable;
/// <summary>
/// To load an empty scene, destroy all previous dontDestroyOnLoad and load selected environments.
/// </summary>
public class EnvironmentLoaderSceneHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string envName = "";
        if (PlayerPrefs.HasKey("environmentToLoad"))
        {
            envName = PlayerPrefs.GetString("environmentToLoad");
        }

        DontDestroy_Handler.instance.Destroy_AllDontDestroy();
        if (!string.IsNullOrEmpty(envName)) EnvironmentsDownloadManager.instance.LoadModule(envName);
        //Initialize_AddressableScene.instance.LoadModuleUsingPath("https://local-cdn.wizar.io/Mobile/com.wizar.stem/assetbundle/android/thetrooper_1_catalog.json");
    }
}
