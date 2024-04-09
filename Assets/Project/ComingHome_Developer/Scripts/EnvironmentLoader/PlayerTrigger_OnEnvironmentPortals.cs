using UnityEngine;

public class PlayerTrigger_OnEnvironmentPortals : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        EnvironmentLoaderOnDoor.instance?.LoadEnvironment(other.gameObject);
    }
}
