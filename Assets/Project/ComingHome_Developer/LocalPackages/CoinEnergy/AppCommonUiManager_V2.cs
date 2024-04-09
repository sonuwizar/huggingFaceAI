using UnityEngine;

public class AppCommonUiManager_V2 : MonoBehaviour
{
    public static AppCommonUiManager_V2 instance;

    public CanvasGroup[] wizbux_GemsCanvasGroups ,otherCanvasGroups;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            //Addressable.DontDestroy_Handler.instance.Add_To_DontDestroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetCommonUi(bool status)
    {
        SetCanvasGroups(otherCanvasGroups, status);
    }

    public void SetWizbuxUI(bool status)
    {
        SetCanvasGroups(wizbux_GemsCanvasGroups, status);
    }

    void SetCanvasGroups(CanvasGroup[] cg, bool status)
    {
        for (int i = 0; i < cg.Length; i++)
        {
            cg[i].alpha = (status) ? 1 : 0;

            cg[i].interactable = status;
            cg[i].blocksRaycasts = status;
        }
    }

    
}
