using UnityEngine;

public class InformationPanelOnOff : MonoBehaviour
{
    public static InformationPanelOnOff instance;

    public GameObject infoBtn;
    public GameObject infoPanel;
    public GameObject customizeBtn;

    private void Awake()
    {
        instance = this;
    }

    public void InfoPanelOn()
    {
        infoPanel.SetActive(true);
        customizeBtn.SetActive(false);
        infoBtn.SetActive(false);
    }

    public void InfoPanelOff()
    {
        infoPanel.SetActive(false);
        customizeBtn.SetActive(true);
        infoBtn.SetActive(true);
    }


}
