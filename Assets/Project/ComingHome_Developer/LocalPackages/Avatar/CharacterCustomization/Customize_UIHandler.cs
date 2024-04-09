using UnityEngine;

public class Customize_UIHandler : MonoBehaviour
{
    public static Customize_UIHandler instance;

    public GameObject customizeBtn;
    public GameObject avatarCustomizePanel;

    private void OnEnable()
    {
        Customisation.Ver2.AvtarCustomisationHandler.onCustomisationSubmit += OnClickSubmit;
    }

    private void OnDisable()
    {
        Customisation.Ver2.AvtarCustomisationHandler.onCustomisationSubmit -= OnClickSubmit;
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    public void OnClickSubmit()
    {
        avatarCustomizePanel.SetActive(false);
        customizeBtn.SetActive(true);
    }

    public void CustomizeBtn()
    {
        avatarCustomizePanel.SetActive(true);
        customizeBtn.SetActive(false);
    }
    
}
