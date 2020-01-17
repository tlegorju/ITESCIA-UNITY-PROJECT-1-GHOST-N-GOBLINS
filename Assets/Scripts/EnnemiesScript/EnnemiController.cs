using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiController : EntityController
{
    [SerializeField] Transform player;
    public Transform Player {get{return player;}}
    private new Rigidbody rigidbody;
    private Animator animator;

    [SerializeField] float lifePoint;
    public float LifePoint { get { return lifePoint; } }
    [SerializeField] float damages = 1.0f;


    [SerializeField] int nbPointsOnDeath = 100;


    protected virtual void Awake()
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
        CallOnSpawn();
        Invoke("CallOnMove", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TakeDamages(int damages)
    {
        if (damages <= 0)
            return;
        lifePoint -= damages;

        if (lifePoint <= 0)
            Dies();
        else
        {
            animator.SetTrigger("TakeDamages");
            CallOnHurted();
        }
    }

    public override void Dies()
    {
        CallOnDies();

        animator.SetTrigger("Dies");
        GetComponent<Collider>().enabled = false;
        rigidbody.isKinematic = true;
        ScoreManager tmpScoreManager = ScoreManager.Instance;
        if (tmpScoreManager)
            tmpScoreManager.AddScore(nbPointsOnDeath);

        Destroy(gameObject, 1.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<SimplePlayerController>().TakeDamages(1);
            CallOnAttack();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<SimplePlayerController>().TakeDamages(1);
            CallOnAttack();
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
