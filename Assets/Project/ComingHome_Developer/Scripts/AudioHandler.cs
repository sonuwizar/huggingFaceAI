using UnityEngine;

namespace SEC_1
{
    public class AudioHandler : MonoBehaviour
    {
        public static AudioHandler instance;

        public AudioSource playerAudioSource, npcAudioSource, bgPlayer;
        public AudioClip runningFootStepAudioClip, gamePlayAudio, cutSceneAudio, cutSceneWithBlinkAudio;

        private void Awake()
        {
            instance = this;
        }

        public void PlayAudio_RunningFootStep()
        {
            if (playerAudioSource.clip != runningFootStepAudioClip || !playerAudioSource.isPlaying)
            {
                playerAudioSource.clip = runningFootStepAudioClip;
                playerAudioSource.Play();
                playerAudioSource.loop = true;
            }
        }

        public void StopAudio_PlayerAudioSource()
        {
            playerAudioSource.Stop();
        }

        public void PlayAudio_NPC()
        {
            if (npcAudioSource.clip != runningFootStepAudioClip || !npcAudioSource.isPlaying)
            {
                npcAudioSource.clip = runningFootStepAudioClip;
                npcAudioSource.Play();
                npcAudioSource.loop = true;
            }
        }

        public void StopAudio_NPC()
        {
            npcAudioSource.Stop();
        }

        public void PlayGamePlayBg()
        {
            if (bgPlayer.isPlaying)
            {
                bgPlayer.Stop();
            }
            bgPlayer.clip = gamePlayAudio;
            bgPlayer.loop = true;
            bgPlayer.volume = 0.7f;
            bgPlayer.Play();
        }

        public void PlayCutSceneBg()
        {
            if (bgPlayer.isPlaying)
            {
                bgPlayer.Stop();
            }
            bgPlayer.clip = cutSceneAudio;
            bgPlayer.loop = true;
            bgPlayer.volume = 0.7f;
            bgPlayer.Play();
        }

        public void PlayCutSceneWithBlinkBg()
        {
            if (bgPlayer.isPlaying)
            {
                bgPlayer.Stop();
            }
            bgPlayer.clip = cutSceneWithBlinkAudio;
            bgPlayer.loop = false;
            bgPlayer.volume = 0.7f;
            bgPlayer.Play();
        }
    }
}
