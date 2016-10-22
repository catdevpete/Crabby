using UnityEngine;
using System.Collections;

public class SpawnCrab : MonoBehaviour
{
    [SerializeField]
    Transform playerBase;
    [SerializeField]
    Transform enemyBase;
    [SerializeField]
    GameObject[] crabPrefabs;

    public void SpawnPlayerCrab(int _i)
    {
        GameObject crab = Instantiate(crabPrefabs[_i], playerBase.position + Vector3.right, Quaternion.identity) as GameObject;
    }

    public void SpawnEnemyCrab(int _i)
    {
        GameObject crab = Instantiate(crabPrefabs[_i], enemyBase.position + Vector3.left, Quaternion.identity) as GameObject;
    }
}