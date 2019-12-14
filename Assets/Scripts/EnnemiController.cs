using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiController : MonoBehaviour
{
    [SerializeField] Transform player;
    public Transform Player {get{return player;}}
    private new Rigidbody rigidbody;
    private Animator animator;

    [SerializeField] float lifePoint;
    public float LifePoint { get { return lifePoint; } }
    [SerializeField] float damages = 1.0f;

    void Awake()
    {
        GameObject tmpPlayer = GameObject.FindGameObjectWithTag("Player");
        if(tmpPlayer!=null)
            player = tmpPlayer.transform;
        else
            Debug.Log("Error : player not found");
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //animator.SetTrigger("Spawn");
        animator.SetBool("IsWalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamages(float damages)
    {
        if (damages <= 0)
            return;
        lifePoint -= damages;

        if (lifePoint <= 0)
            Dies();
        else
            animator.SetTrigger("TakeDamages");
    }

    private void Dies()
    {
        animator.SetTrigger("Dies");
        GetComponent<Collider>().enabled = false;
        rigidbody.isKinematic = true;
        Destroy(gameObject, 2.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamages();
        }
    }

    /*public void MoveTowardsPosition(Vector3 targetPosition)
    {
        Vector3 direction = (Vector3)(targetPosition - rigidbody.position).normalized;
        rigidbody.MovePosition(rigidbody.position + direction * walkingSpeed * Time.fixedDeltaTime);
    }

    public void LookAtPoint(Vector3 pointToLookAt)
    {
        Vector3 direction = (Vector3)(pointToLookAt - rigidbody.position).normalized;
        rigidbody.MoveRotation(Quaternion.LookRotation(direction));
    }*/


}
