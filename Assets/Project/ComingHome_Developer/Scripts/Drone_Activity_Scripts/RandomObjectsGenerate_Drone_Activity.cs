using System.Collections.Generic;
using UnityEngine;

public class RandomObjectsGenerate_Drone_Activity : MonoBehaviour
{
    public static RandomObjectsGenerate_Drone_Activity instance;

    public List<GameObject> objectToPlace;
    public List<Transform> randomPositions;
    public GameObject highlighterPrefab;
    internal GameObject prefab;

    List<Transform> randomPositionsBackup;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        randomPositionsBackup = randomPositions;
        PlaceObjects();
    }

    void PlaceObjects()
    {
        for (int i = 0; i < objectToPlace.Count ; i++)
        {
            GameObject objectsToPlace = objectToPlace[i];
            int index = Random.Range(0, randomPositionsBackup.Count);
            objectsToPlace.transform.position = randomPositionsBackup[index].position;
            objectsToPlace.transform.SetParent(randomPositionsBackup[index]);
            randomPositionsBackup[index].gameObject.SetActive(true);
            prefab = Instantiate(highlighterPrefab, randomPositionsBackup[index].transform);
            randomPositionsBackup.RemoveAt(index);
        }
    }

}
