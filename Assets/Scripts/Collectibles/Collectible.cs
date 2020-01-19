using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collectible : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] int nbPointsOnCollected;
    public AudioClip pickedupSound;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.Rotate(0, Time.deltaTime * 10, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        source.clip = pickedupSound;
        
        if (other.gameObject.CompareTag("Player"))
        {
            source.Play();
            CollidedWithPlayer(other.gameObject);
        }
    }

    protected virtual void CollidedWithPlayer(GameObject player)
    {
       
        if (ScoreManager.Instance)
        {
            ScoreManager.Instance.AddScore(nbPointsOnCollected);
        }
        StartCoroutine(DestroyObjects());
    }

    protected virtual void DestroyObject()
    {
       Destroy(gameObject);
    }
    IEnumerator DestroyObjects()
    {
        yield return new WaitForSeconds(0.1f);
        DestroyObject();
    }
}
