using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collectible : MonoBehaviour
{
    [SerializeField] int nbPointsOnCollected;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            CollidedWithPlayer(other.gameObject);
    }

    protected virtual void CollidedWithPlayer(GameObject player)
    {
        if (ScoreManager.Instance)
        {
            ScoreManager.Instance.AddScore(nbPointsOnCollected);
            DestroyObject();
        }
    }

    protected virtual void DestroyObject()
    {
        Destroy(gameObject);
    }
}
