using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

[Serializable]
public class ColorEvent : UnityEvent<Color> { }

public class ColorPicker : MonoBehaviour
{
    public Image colorImage;
    public ColorEvent onColorPreview;
    public ColorEvent onColorSelect;

    RectTransform rect;
    Texture2D colorTexture;

    public delegate void SetColor(Color color);
    public static event SetColor setColor;

    // Start is called before the first frame update
    void Start()
    {
        rect = colorImage.GetComponent<RectTransform>();
        colorTexture = colorImage.mainTexture as Texture2D;
    }

    // Update is called once per frame
    void Update()
    {
        if (!colorImage.gameObject.activeInHierarchy)
        {
            return;
        }

        Vector3 currPos = Vector3.zero;
#if UNITY_EDITOR
        currPos = Input.mousePosition;
#else
        if (Input.touchCount > 0)
        {
            currPos = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }
#endif

        if (RectTransformUtility.RectangleContainsScreenPoint(rect, currPos))
        {
            Vector2 delta;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, currPos, null, out delta);
            float width = rect.rect.width;
            float height = rect.rect.height;
            delta += new Vector2(width * 0.5f, height * 0.5f);
            float x = Mathf.Clamp(delta.x / width, 0, 1);
            float y = Mathf.Clamp(delta.y / height, 0, 1);
            int texX = Mathf.RoundToInt(x * colorTexture.width);
            int texY = Mathf.RoundToInt(y * colorTexture.height);
            Color color = colorTexture.GetPixel(texX, texY);

            Debug.Log("color value- " + color.a);

            //color.a = 0;
            onColorPreview?.Invoke(color);
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                onColorSelect?.Invoke(color);

                if (setColor != null)
                {
                    setColor(color);
                }
            }
#else
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                onColorSelect?.Invoke(color);
                if (setColor != null)
                {
                    setColor(color);
                }
            }
#endif
        }
    }
}