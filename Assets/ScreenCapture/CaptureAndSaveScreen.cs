using UnityEngine;
using System.IO;
using System;

public class CaptureAndSaveScreen : MonoBehaviour
{
    public static CaptureAndSaveScreen instance;

    string path;
	Texture2D currentTexture;
	int textureWidth, textureHeight;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
		path = Application.persistentDataPath + "/WizAR/";
		textureWidth = Screen.width;
		textureHeight = Screen.height;
	}

    public void CaptureScreen(string fileName)
    {
		try
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			DeleteCaptureImage(fileName);

			string screenShotName = path + fileName;
			RenderTexture rt = new RenderTexture(textureWidth, textureHeight, 100, RenderTextureFormat.ARGB32);
			rt.useMipMap = false;
			rt.antiAliasing = 1;
			RenderTexture.active = rt;
			Camera.main.targetTexture = rt;
			Texture2D shot = new Texture2D(textureWidth, textureHeight, TextureFormat.ARGB32, false);
			Camera.main.Render();
			shot.ReadPixels(new Rect(0, 0, textureWidth, textureHeight), 0, 0, false);
			shot.Apply();
			byte[] bytes = shot.EncodeToPNG();
			File.WriteAllBytes(screenShotName + ".png", bytes);

			Debug.Log("Customisation ui captured");

			//StartCoroutine(ScreenshotShownPopUp(shot));
			bytes = null;
			shot = null;
			Camera.main.targetTexture = null;
			RenderTexture.active = null;
			Destroy(rt);
		}
		catch (Exception e)
		{
			Debug.Log(e);
		}
	}

	public Sprite LoadCaptureImage(string fileName)
    {
		string captureImagePath = path + fileName + ".png";

		if (File.Exists(captureImagePath))
        {
			byte[] bytes_1 = File.ReadAllBytes(captureImagePath);
			currentTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.ARGB32, false);
			currentTexture.LoadImage(bytes_1);

            return Sprite.Create(currentTexture, new Rect(0, 0, currentTexture.width, currentTexture.height), new Vector2(0.2f, 0.2f), 100);
        }
		return null;
    }

	public void DeleteCaptureImage(string fileName)
    {
		string captureImagePath = path + fileName + ".png";
        if (File.Exists(captureImagePath))
        {
			File.Delete(captureImagePath);
		}
	}
}
