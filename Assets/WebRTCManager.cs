using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebRTCManager : MonoBehaviour
{
    // Method to start a whisper call
    public void StartWhisper(string targetUserId)
    {
        // Call JavaScript function to start whisper with targetUserId
        Application.ExternalCall("startWhisper", targetUserId);
    }

    // Method to accept a whisper call
    public void AcceptWhisper()
    {
        // Call JavaScript function to accept incoming whisper call
        Application.ExternalCall("acceptWhisper");
    }

    // Method to end a whisper call
    public void EndWhisper()
    {
        // Call JavaScript function to end ongoing whisper call
        Application.ExternalCall("endWhisper");
    }

    // Method to display received text in Unity
    public void DisplayText(string text)
    {
        // Display the text in the game UI
        Debug.Log("Received text: " + text);
    }
}
