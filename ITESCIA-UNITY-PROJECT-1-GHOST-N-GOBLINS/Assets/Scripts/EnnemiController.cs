using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiController : MonoBehaviour
{
    [SerializeField] GameObject player;
    public GameObject Player{get{return player;}}
    private new Rigidbody rigidbody;
    private Animator animator;

    [SerializeField] float walkingSpeed;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveTowardsPosition(Vector3 targetPosition)
    {
        Vector3 direction = (Vector3)(targetPosition - rigidbody.position).normalized;
        rigidbody.MovePosition(rigidbody.position + direction * walkingSpeed * Time.fixedDeltaTime);
    }

    public void LookAtPoint(Vector3 pointToLookAt)
    {
        Vector3 direction = (Vector3)(pointToLookAt - rigidbody.position).normalized;
        rigidbody.MoveRotation(Quaternion.LookRotation(direction));
    }
}
