using UnityEngine;

public class Player_TriggerCollision_DroneActivity : MonoBehaviour
{
    public static Player_TriggerCollision_DroneActivity instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Debug.Log("Script Enabled ");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!this.enabled)
        {
            return;
        }
        Debug.Log("colliders" + other.name, other.gameObject);
        BagManager_DroneActivity.instance.TriggeredComponents(other.name, true);
        TableTrigger_DroneActivity.instance.TableTriggerEnter(other);
        DroneAnimationHandler_DroneActivity.instance.DroneAnimationTrigger(other);
        DroneDescriptionTriggerHandler_DroneActivity.instance.OnDescriptionTriggerEnter(other);
        VonLab_DroneActivity.instance.OnTriggerVanEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!this.enabled)
        {
            return;
        }
        BagManager_DroneActivity.instance.TriggeredComponents(other.name, false);
        TableTrigger_DroneActivity.instance.TableTriggerExit(other);
    }

}
