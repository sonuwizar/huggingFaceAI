using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;

    public Transform target;
    public float smoothSpeed;
    public Vector2 rotationOffset;
    public Vector3 offset;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = desiredPosition;
        if (Camera.main != null)
        {
            Camera.main.transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, 0);
        }
    } 

    public void SetCameraPosition()
    {
        transform.position = target.position + offset;
    }

    public void SetCameraAngle()
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, target.eulerAngles.y, transform.eulerAngles.z));
    }

    public void SetCameraRotation(float yAxis, float xAxis)
    {
        Debug.Log("Reset Camera"+yAxis+" "+xAxis);
        float xValue = transform.eulerAngles.x + rotationOffset.x * xAxis/10;
        float yValue = transform.eulerAngles.y - rotationOffset.y * yAxis/10;
        transform.rotation = Quaternion.Euler(new Vector3(0, yValue, 0));
    }
}
