using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ApiRequestHandler : MonoBehaviour
{
    public static ApiRequestHandler instance;

    //To get output after processing request for required request
    public delegate void AfterProcessWebRequest(string webRequestMsg, bool requestStatus);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        instance = this;
    }

    public void GetRequest(string url, AfterProcessWebRequest afterProcessWebRequest) => StartCoroutine(GetRequest_Enumerator(url, afterProcessWebRequest));

    IEnumerator GetRequest_Enumerator(string url, AfterProcessWebRequest afterProcessWebRequest)
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(url))
        {
            yield return unityWebRequest.SendWebRequest();

            if (!String.IsNullOrEmpty(unityWebRequest.error))
            {
                Debug.Log("Error on Get Request- " + unityWebRequest.error);
                if (afterProcessWebRequest != null)
                {
                    afterProcessWebRequest(unityWebRequest.error, false);
                }
            }
            else if (unityWebRequest.isDone)
            {
                if (afterProcessWebRequest != null)
                {
                    afterProcessWebRequest(unityWebRequest.downloadHandler.text, true);
                }
            }
        }
    }

    public void PostRequest(string url, string bodyJson, AfterProcessWebRequest afterProcessWebRequest) => StartCoroutine(PostRequest_Enumerator(url, bodyJson, afterProcessWebRequest));

    IEnumerator PostRequest_Enumerator(string url, string bodyJson, AfterProcessWebRequest afterProcessWebRequest)
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.PostWwwForm(url, "POST"))
        {
            unityWebRequest.SetRequestHeader("Content-Type", "application/json");

            byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJson);
            unityWebRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            unityWebRequest.downloadHandler = new DownloadHandlerBuffer();

            yield return unityWebRequest.SendWebRequest();

            if (!String.IsNullOrEmpty(unityWebRequest.error))
            {
                Debug.Log("Error on Post Request- " + unityWebRequest.error);
                if (afterProcessWebRequest != null)
                {
                    afterProcessWebRequest(unityWebRequest.error, false);
                }
            }
            else if (unityWebRequest.isDone)
            {
                if (afterProcessWebRequest != null)
                {
                    afterProcessWebRequest(unityWebRequest.downloadHandler.text, true);
                }
            }
        }
    }

    public void PutRequest(string url, AfterProcessWebRequest afterProcessWebRequest) => StartCoroutine(PutRequest_Enumerator(url, afterProcessWebRequest));

    IEnumerator PutRequest_Enumerator(string url, AfterProcessWebRequest afterProcessWebRequest)
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Put(url, "PUT"))
        {
            yield return unityWebRequest.SendWebRequest();

            if (!String.IsNullOrEmpty(unityWebRequest.error))
            {
                Debug.Log("Put Error " + unityWebRequest.error);
                if (afterProcessWebRequest != null)
                {
                    afterProcessWebRequest(unityWebRequest.error, false);
                }
            }
            else if (unityWebRequest.isDone)
            {
                if (afterProcessWebRequest != null)
                {
                    afterProcessWebRequest(unityWebRequest.downloadHandler.text, true);
                }
            }
        }
    }
}
