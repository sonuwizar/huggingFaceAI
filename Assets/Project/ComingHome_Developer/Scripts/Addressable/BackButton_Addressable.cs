using UnityEngine;

namespace Addressable
{
    public class BackButton_Addressable : MonoBehaviour
    {
        public void Click_BackButton()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(DontDestroy_Handler.instance.appSceneIndex);
            DontDestroy_Handler.instance.Destroy_AllDontDestroy();
        }

    }
}
