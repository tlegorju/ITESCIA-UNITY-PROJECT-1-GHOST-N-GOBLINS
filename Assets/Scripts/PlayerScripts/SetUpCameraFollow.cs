using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class SetUpCameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance && GameManager.Instance.PlayerRef != null)
            player = GameManager.Instance.PlayerRef.transform;
        else
            player = GameObject.FindGameObjectWithTag("Player").transform;

        cinemachineVirtualCamera.Follow = player;
        cinemachineVirtualCamera.LookAt = player;
    }
}
