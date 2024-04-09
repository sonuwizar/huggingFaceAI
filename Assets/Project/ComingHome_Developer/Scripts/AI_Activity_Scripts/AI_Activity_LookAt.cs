using UnityEngine;

public class AI_Activity_LookAt : MonoBehaviour
{
    public static AI_Activity_LookAt instance;

    public Transform playBtn, player;

    private void Update()
    {
        Vector3 targetPos = playBtn.position -  player.position;
        playBtn.LookAt(playBtn.position + targetPos, Vector3.up);
    }
}
