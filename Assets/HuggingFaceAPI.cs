using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Linq;
using System.Net.Http;

using System;
using UnityEngine.Windows.Speech;
using UnityEngine.Windows;
using static HuggingFaceAPIExample;
using Samples.Whisper;

public class HuggingFaceAPIExample : MonoBehaviour
{ 
    public static HuggingFaceAPIExample instance;

    // HuggingFaceAI
    private string apiUrl = "https://api-inference.huggingface.co/models/sentence-transformers/all-mpnet-base-v2";
    private string hfApiKey = "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";

    // Language translate API
    private string apiUrl1 = "https://api-inference.huggingface.co/models/Helsinki-NLP/opus-mt-hi-en";

    private string micDevice;
    private bool isRecording = false;

    public GameObject Jammo;
    public TextMeshProUGUI outputText;

    private float maxScore;
    private int maxScoreIndex;

   

   [SerializeField] public Action[] ActionList;



    private void Awake()
    {
        instance = this;    
    }
    public void CallTransformCoroutine()
    {
        StartCoroutine(TransformCoroutine());
    }


    IEnumerator TransformCoroutine()
    {
        string transcriptionResult = Whisper.instance.transcriptionResult;
        Debug.Log("transcriptionResult");
        // Create JSON payload for translation
        string jsonPayload = "{\"inputs\": \"" + transcriptionResult + "\"}";
        Debug.Log("2");
        // Create HTTP content
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(bodyRaw);
        uploadHandlerRaw.contentType = "application/json";
        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

        // Create and configure UnityWebRequest
        UnityWebRequest www = new UnityWebRequest(apiUrl1, "POST", downloadHandlerBuffer, uploadHandlerRaw);
        www.SetRequestHeader("Authorization", "Bearer " + hfApiKey);
        www.SetRequestHeader("Content-Type", "application/json");
       
        // Send the POST request
        yield return www.SendWebRequest();
    
        // Check if the request was successful
        if (www.result == UnityWebRequest.Result.Success)
        {
            // Read and display the translated text
            string jsonResponse = www.downloadHandler.text;
            Debug.Log("Translated text: " + jsonResponse);
            yield return Transform(jsonResponse);
            Debug.Log(jsonResponse);
        }
        else
        {
            Debug.LogError("Failed to translate: " + www.error);
        }
    }

    private IEnumerator Transform(string jsonResponse)
    {
        //jsonResponse = "Jammo move left";
        string sourceSentence = jsonResponse;
        Debug.Log(jsonResponse);
        string[] sampleSentences = new string[]
        {
            "Stop means Idle",
            "jammo move left",
            "jammo move right",
            "jammo stop",
            "Jammo jump",
            "Jammo move"
        };

        yield return StartCoroutine(Query(new Inputs
        {
            source_sentence = sourceSentence,
            sentences = sampleSentences
        }));
        Debug.Log(sourceSentence);
    }

    IEnumerator Query(Inputs payload)
    {
        string requestUrl = apiUrl;

        // Convert payload to JSON string
        string payloadJson = JsonConvert.SerializeObject(payload);

        // Convert JSON string to bytes
        byte[] bodyData = System.Text.Encoding.UTF8.GetBytes(payloadJson);

        // Create UnityWebRequest object for POST request
        UnityWebRequest request = UnityWebRequest.PostWwwForm(requestUrl, "POST");

        // Set request headers
        request.uploadHandler = new UploadHandlerRaw(bodyData);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", "Bearer " + hfApiKey);
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result == UnityWebRequest.Result.Success)
        {
            // Request succeeded, parse and handle the response
            string response = request.downloadHandler.text;
            Debug.Log("Response: " + response);

            outputText.text = response;
            yield return StartCoroutine(ProcessResult(response));
        }
        else
        {
            Debug.LogError("POST request failed: " + request.error);
        }
    }

    private IEnumerator ProcessResult(string result)
    {
        string cleanedResult = result.Replace("[", "");
        cleanedResult = cleanedResult.Replace("]", "");

        string[] splitArray = cleanedResult.Split(char.Parse(","));
        float[] myFloat = splitArray.Select(float.Parse).ToArray();

        maxScore = myFloat.Max();
        maxScoreIndex = myFloat.ToList().IndexOf(maxScore);
        Debug.Log(maxScoreIndex);
        Utility(maxScore, maxScoreIndex);

        yield return null;
    }

    private void Utility(float maxScore, int maxScoreIndex)
    {
        if (maxScore < 0.20f)
        {
            if (AIController.instance != null)
            {
                AIController.instance.state = AIController.State.Idle;
            }
        }
        else
        {
            // Get the verb and noun (if there is one)
            string verb = ActionList[maxScoreIndex].verb;
            AIController.instance.state = (AIController.State)System.Enum.Parse(typeof(AIController.State), verb, true);
        }
    }

    // Define class for JSON serialization
    [System.Serializable]
    public class Inputs
    {
        public string source_sentence;
        public string[] sentences;
    }

    [System.Serializable]
    public class Action
    {
        [Header("ActionList")]
        public string sentence;
        public string noun;
        public string verb;
    }
}
