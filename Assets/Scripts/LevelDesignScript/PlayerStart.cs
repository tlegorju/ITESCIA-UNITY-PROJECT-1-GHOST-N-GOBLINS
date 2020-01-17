using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject instancePlayer = GameObject.FindGameObjectWithTag("Player");
        if (!instancePlayer)
        {
            instancePlayer = Instantiate(playerPrefab, transform.position, transform.rotation);
            if (GameManager.Instance)
                GameManager.Instance.PlayerRef = instancePlayer;
        }
        else
        {
            instancePlayer.transform.position = transform.position;
            instancePlayer.transform.rotation = transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
