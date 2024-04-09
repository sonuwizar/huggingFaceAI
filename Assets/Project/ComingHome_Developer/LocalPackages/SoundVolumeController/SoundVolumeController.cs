using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeController : MonoBehaviour
{
    public Slider soundVolumeSlider;
    public Image muteUnmuteBtn_Image;

    [Header("Sprites")]
    public Sprite[] unmuteSprites;

    float preVolume;
    bool isMute;

    private void OnEnable()
    {
        isMute = (PlayerPrefs.HasKey("isMute") && PlayerPrefs.GetInt("isMute") == 0) ? false : true;
        SetSoundMute(isMute);
    }

    //To set volume from slider
    public void SetSoundVolume()
    {
        if (isMute && soundVolumeSlider.value > 0)
        {
            PlayerPrefs.SetInt("isMute", 0);
            isMute = false;
            SetMuteBtnUI(soundVolumeSlider.value);
        }
        else if(soundVolumeSlider.value == 0)
        {
            PlayerPrefs.SetInt("isMute", 1);
            isMute = true;
            SetMuteBtnUI(0);
        }

        float soundVolume = soundVolumeSlider.value;
        PlayerPrefs.SetFloat("SoundVolume", soundVolume);
        SetMuteBtnUI(soundVolume);
        AudioListener.volume = soundVolumeSlider.value;
    }

    //to manage mute and unmute 
    public void ClickMuteUnmuteBtn()
    {
        isMute = !isMute;
        SetSoundMute(isMute);
    }

    void SetSoundMute(bool status)
    {
        int muteValue = (isMute) ? 1 : 0;
        PlayerPrefs.SetInt("isMute", muteValue);

        if (status)
        {
            preVolume = soundVolumeSlider.value;
            soundVolumeSlider.value = 0;
            SetMuteBtnUI(0);
        }
        else
        {
            if (preVolume == 0)
            {
                preVolume = 0.1f;
            }
            soundVolumeSlider.value = preVolume;
            PlayerPrefs.SetFloat("SoundVolume", preVolume);
            SetMuteBtnUI(preVolume);
        }
        AudioListener.volume = soundVolumeSlider.value;
    }

    //to set ui button for mute and unmute
    void SetMuteBtnUI(float volumeValue)
    {
        if (volumeValue == 0)
        {
            muteUnmuteBtn_Image.sprite = unmuteSprites[0];
        }
        else if(volumeValue < 0.6f)
        {
            muteUnmuteBtn_Image.sprite = unmuteSprites[1];
        }
        else
        {
            muteUnmuteBtn_Image.sprite = unmuteSprites[2];
        }
    }

}
