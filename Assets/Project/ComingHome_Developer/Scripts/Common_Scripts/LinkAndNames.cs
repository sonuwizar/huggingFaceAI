using UnityEngine;

public class LinkAndNames : MonoBehaviour
{
    public static string LinkToAssetBundle = "https://storage.googleapis.com/onos2023/orange-dev/";  //Links to android assetbundles

#if UNITY_EDITOR || UNITY_ANDROID
    public static string deviceType = "Android";
#else
    public static string deviceType = "Ios";
#endif

    public static string bundleVersion = "1";

    public static string catalogJsonPath = "catalog.json"; //name for catalog.json for addressable

    public static string GetCatalogFileUrl(string moduleName)
    {
        return LinkToAssetBundle + moduleName + "_" + deviceType + "_" + bundleVersion + "/" + moduleName + "_" + bundleVersion + "_" + catalogJsonPath;
    }

    public static string Warning01 = "Something went wrong. start without updating ?";

    public static string Warning02 = "Something went wrong, check internet connection and restart downloading ?";

    public static string Warning03 = "Something went wrong, restart loading ?";

    public static string error01 = "Activation Not Allowed. Key has been used on maximum devices.";

    public static string error02 = "Could not connect to server, please check your internet connection or try after some time";

    public static string error03 = "Server is under maintenance, please be patient and try after some time.";

    public static string error04 = "Wrong Activation Key. please check and re-enter the key.";

    public static string error05 = "This Activation key is not for the book you are trying to activate. Thankyou.";

    public static string error06 = "Unable to process your request, please try after some time.";

    public static string error07 = "Unknown Error, Please restart your application or redownload application.";

    public static string error08 = "Your application is outdated, please update your app from store.";

    public static string error09 = "Login error, try later again later or use another login type.";

#if UNITY_ANDROID
    public static string PathToScreenshots = "/mnt/sdcard/DCIM/WizAR/";
    public static string PathToScreenshotsThumbnail = "/mnt/sdcard/DCIM/WizAR/";
#endif

#if UNITY_IOS
	public static string PathToScreenshots = Application.persistentDataPath + "/WizAR/";
	public static string PathToScreenshotsThumbnail = Application.persistentDataPath + "/WizAR/";
#endif

    public static string questTrailApiUrl = "https://admin.wizar.io/api/v1/QuestTrail/";
    public static string learningTrail_SchoolNameApiUrl = "https://admin.wizar.io/api/v1/LearningTrail/school/";
}
