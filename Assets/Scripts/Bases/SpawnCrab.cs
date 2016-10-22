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

    bool canSpawn;

    void Start()
    {
        canSpawn = true;
    }

    public void SpawnPlayerCrab(int _i, float _t)
    {
        if (canSpawn)
        {
            GameObject crab = Instantiate(crabPrefabs[_i], playerBase.position + Vector3.right, Quaternion.identity) as GameObject;
            StartCoroutine(SpawnCycle(_t));
        }
    }

    public void SpawnEnemyCrab(int _i, float _t)
    {
        if (canSpawn)
        {
            GameObject crab = Instantiate(crabPrefabs[_i], enemyBase.position + Vector3.left, Quaternion.identity) as GameObject;
            StartCoroutine(SpawnCycle(_t));
        }
    }

    IEnumerator SpawnCycle(float _t)
    {
        canSpawn = false;
        yield return new WaitForSeconds(_t);
        canSpawn = true;
    }
}