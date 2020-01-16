using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnnemiSpawner : MonoBehaviour
{
    [SerializeField] GameObject ennemiToSpawnPrefab;
    [SerializeField] Transform spawnPoint;
    GameObject ennemiSpawned = null;



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            /*if (other.gameObject.transform.position.z >= transform.position.z)
                return;*/

            if (ennemiSpawned)
                return;

            ennemiSpawned = Instantiate(ennemiToSpawnPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
        }
    }
}
