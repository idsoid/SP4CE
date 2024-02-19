using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject exitDoorPrefab;
    [SerializeField] private GameObject lvl3KeycardPrefab;
    [SerializeField] private GameObject lvl4KeycardPrefab;
    [SerializeField] private GameObject lvl5KeycardPrefab;

    [Header("Spawn Positions")]
    [SerializeField] private List<Transform> exitDoorSpawnpoints;
    [SerializeField] private List<Transform> lvl3KeycardSpawnpoints;
    [SerializeField] private List<Transform> lvl4KeycardSpawnpoints;
    [SerializeField] private List<Transform> lvl5KeycardSpawnpoints;
    
    private void Start()
    {
        SpawnExitDoor();
        SpawnLvl3Keycard();
        SpawnLvl4Keycard();
        SpawnLvl5Keycard();
    }

    private void SpawnExitDoor()
    {
        int rng = Random.Range(0, exitDoorSpawnpoints.Count);
        Instantiate(exitDoorPrefab, exitDoorSpawnpoints[rng].position, exitDoorSpawnpoints[rng].rotation);
    }

    private void SpawnLvl3Keycard()
    {
        int rng = Random.Range(0, lvl5KeycardSpawnpoints.Count);
        float rot = Random.Range(-180f, 180f);
        Instantiate(lvl5KeycardPrefab, lvl3KeycardSpawnpoints[rng].position, Quaternion.Euler(new Vector3(-90f, rot, 0f)));
    }

    private void SpawnLvl4Keycard()
    {
        int rng = Random.Range(0, lvl5KeycardSpawnpoints.Count);
        float rot = Random.Range(-180f, 180f);
        Instantiate(lvl5KeycardPrefab, lvl4KeycardSpawnpoints[rng].position, Quaternion.Euler(new Vector3(-90f, rot, 0f)));
    }

    private void SpawnLvl5Keycard()
    {
        int rng = Random.Range(0, lvl5KeycardSpawnpoints.Count);
        float rot = Random.Range(-180f, 180f);
        Instantiate(lvl5KeycardPrefab, lvl5KeycardSpawnpoints[rng].position, Quaternion.Euler(new Vector3(-90f, rot, 0f)));
    }
}
