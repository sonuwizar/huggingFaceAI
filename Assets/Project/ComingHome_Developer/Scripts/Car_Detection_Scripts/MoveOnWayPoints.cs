using System.Collections.Generic;
using UnityEngine;

public class MoveOnWayPoints : MonoBehaviour
{
    public static MoveOnWayPoints instance;

    public List<GameObject> wayPoints;
    public float speed = 2;
    int index = 0;
    public bool isLoop = true;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 destination = wayPoints[index].transform.position;
        Vector3 newPos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        transform.position = newPos;

        float distance = Vector3.Distance(transform.position, destination);

        if(distance <= 0.05)
        {
            if(index < wayPoints.Count-1)
            {
                index++;
            }
            else
            {
                if(isLoop)
                {
                    index = 0;
                }
            }
        }
    }
}