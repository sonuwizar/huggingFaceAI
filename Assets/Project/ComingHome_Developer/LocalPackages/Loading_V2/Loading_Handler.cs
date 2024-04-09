using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class Loading_Handler : MonoBehaviour
{
    public static Loading_Handler instance;

    [SerializeField] GameObject loadingPanel;
    [SerializeField] Image loadingFillBar;

    Coroutine updateLoadingBarRoutine;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        SetLoadingPanel(true);
    }

    private void OnDisable()
    {
        SetLoadingPanel(false);
    }

    internal void SetLoadingPanel(bool status)
    {
        if(loadingPanel != null) loadingPanel.SetActive(status);
    }

    internal void UpdateLoadingBar(float loadingBarValue)
    {
       if(loadingFillBar != null) loadingFillBar.fillAmount = loadingBarValue;
    }

    internal void UpdateLoadingBar(AsyncOperation asyncOperation)
    {
        if (updateLoadingBarRoutine != null)
        {
            StopCoroutine(updateLoadingBarRoutine);
        }

        updateLoadingBarRoutine = StartCoroutine(UpdateLoading_Enumrator(asyncOperation));
    }

    internal void UpdateLoadingBar<T>(AsyncOperationHandle<T> asyncOperation)
    {
        if (updateLoadingBarRoutine != null)
        {
            StopCoroutine(updateLoadingBarRoutine);
        }

        updateLoadingBarRoutine = StartCoroutine(UpdateLoading_Enumrator(asyncOperation));
    }

    IEnumerator UpdateLoading_Enumrator(AsyncOperation asyncOperation)
    {
        float iniTime = Time.time;
        float timeValue = (Time.time - iniTime);
        SetLoadingPanel(true);

        while (!asyncOperation.isDone && timeValue < 300)
        {
            UpdateLoadingBar(Mathf.Clamp01(asyncOperation.progress));
            timeValue = (Time.time - iniTime);
            yield return new WaitForEndOfFrame();
        }
        SetLoadingPanel(false);
    }

    IEnumerator UpdateLoading_Enumrator<T>(AsyncOperationHandle<T> asyncOperation)
    {
        float iniTime = Time.time;
        float timeValue = (Time.time - iniTime);
        SetLoadingPanel(true);

        while (!asyncOperation.IsDone && timeValue < 300)
        {
            if (!asyncOperation.IsValid())
            {
                yield break;
            }

            if (asyncOperation.Status == AsyncOperationStatus.Failed)
            {
                Debug.Log("Operation Failed");
                yield break;
            }

            UpdateLoadingBar(Mathf.Clamp01(asyncOperation.PercentComplete));
            timeValue = (Time.time - iniTime);
            yield return new WaitForEndOfFrame();
        }
        SetLoadingPanel(false);
    }

}
