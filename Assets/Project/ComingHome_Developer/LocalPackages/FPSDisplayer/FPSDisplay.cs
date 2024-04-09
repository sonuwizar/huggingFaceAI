using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
    static FPSDisplay instance;

    public int avgFrameRate;
    public Text display_Text;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(instance);
        }
    }

    float deltaTime;

    public void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1 / deltaTime;
        display_Text.text = Mathf.Ceil(fps) + " FPS";
    }
}
