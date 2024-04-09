using UnityEngine;
using UnityEngine.UI;

public class Quiz_FirstThirdPersonView : MonoBehaviour
{
    public static Quiz_FirstThirdPersonView instance;


    public Toggle playerViewToggle;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
       
    }

    public void ChangePlayerView()
    {
        ThirdPersonController_V2.instance.ChangePlayerView(playerViewToggle.isOn);
    }
  
}
