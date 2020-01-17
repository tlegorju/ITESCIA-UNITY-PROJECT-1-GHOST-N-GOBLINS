using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance)
                GameManager.Instance.LevelWon();
        }
    }
}
