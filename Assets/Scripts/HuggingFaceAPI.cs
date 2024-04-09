using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Linq;


public class HuggingFaceAPIExample : MonoBehaviour
{


    private string apiUrl = "https://api-inference.huggingface.co/models/sentence-transformers/all-mpnet-base-v2";
    private string hfApiKey = "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";
    public GameObject Jammo;

    public TextMeshProUGUI outputText;
    public TMP_InputField sourceInputField;
    private float maxScore;
    private int maxScoreIndex;

    [SerializeField]
    public Action[] ActionList;

    private void Start()
    {
        //// Populate the ActionList array with some data
        //ActionList = new Action[]
        //{
        //    new Action { sentence = "Some sentence", noun = "Some noun", verb = "Some verb" },
        //    // Add more Action instances as needed
        //};
    }
    public void StartTransform()
    {
        string sourceSentence = sourceInputField.text;
        string[] sampleSentences = new string[]
        {
            "Stop means Idle",
            "jammo move left",
            "jammo move right",
            "jammo stop",
            "Jammo jump",
            "Jammo move"
           
        };

        StartCoroutine(Query(new Inputs
        {
            source_sentence = sourceSentence,
            sentences = sampleSentences
        }));
    }

    IEnumerator Query(Inputs payload)
    {
        string requestUrl = apiUrl;

        // Convert payload to JSON string
        string payloadJson = JsonConvert.SerializeObject(payload);

        // Convert JSON string to bytes
        byte[] bodyData = System.Text.Encoding.UTF8.GetBytes(payloadJson);

        // Create UnityWebRequest object for POST request
        UnityWebRequest request = UnityWebRequest.Post(requestUrl,"POST");

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
        if(maxScore < 0.20f)
        {
            if (AIController.instance != null)
            {
                AIController.instance.state = AIController.State.Idle;
            }
        }
        else
        {   // Get the verb and noun (if there is one)
            Debug.Log("I am here");
           // GameObject goalObject = GameObject.Find(ActionList[maxScoreIndex].noun);
           // Debug.Log("noun");
           // Debug.Log(maxScoreIndex);
            string verb = ActionList[maxScoreIndex].verb;
            Debug.Log(verb);
            AIController.instance.state = (AIController.State)System.Enum.Parse(typeof(AIController.State), verb, true);
            Debug.Log("verb");
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

   // Header[]
}

