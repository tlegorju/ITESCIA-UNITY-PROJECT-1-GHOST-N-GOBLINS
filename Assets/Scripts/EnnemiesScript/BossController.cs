using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EntityController
{
    private new Rigidbody rigidbody;
    private Animator animator;

    [SerializeField] private float lifePoint = 10;
    [SerializeField] float lifePointToStage2 = 5;

    [SerializeField] Transform[] targetPoints;

    private Transform playerRef;

    [SerializeField] float rotationSpeed = 10.0f;


    [SerializeField] int nbPointsOnDeath = 50000;

    private void Awake()
    {
        OnHurted += CheckGoToStage2;

        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance && GameManager.Instance.PlayerRef != null)
            playerRef = GameManager.Instance.PlayerRef.transform;
        else
            playerRef = GameObject.FindGameObjectWithTag("Player").transform;

        animator.SetInteger("LastTargetIndex", targetPoints.Length-1);
    }

    // Update is called once per frame
    void Update()
    {
        FacePlayer();
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

        Invoke("EndGame", 5.0f);
    }

    private void EndGame()
    {

        Destroy(playerRef.gameObject);

        if (GameManager.Instance)
            GameManager.Instance.LevelWon();

    }

    private void CheckGoToStage2()
    {
        if(lifePoint <= lifePointToStage2)
        {
            GetComponent<Animator>().SetTrigger("Stage2");
        }
    }

    public Vector3 GetCurrentTargetPosition(int currentTargetIndex)
    {
        if (currentTargetIndex >= 0 && currentTargetIndex < targetPoints.Length)
            return targetPoints[currentTargetIndex].position;
        return Vector3.zero;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            EntityController tmpEntity = other.gameObject.GetComponent<EntityController>();
            if(tmpEntity)
                tmpEntity.TakeDamages(1);
        }
    }

    private void FacePlayer()
    {
        if (playerRef == null)
            return;

        float targetRotation=0;
        if (playerRef.position.z < transform.position.z)
            targetRotation = 180;

        rigidbody.MoveRotation(Quaternion.Lerp(rigidbody.rotation, Quaternion.Euler(0, targetRotation, 0), Time.deltaTime * rotationSpeed));
    }
}
