using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnnemiController
{
    [SerializeField] float lifePointToStage2 = 5;

    [SerializeField] Transform[] targetPoints;

    protected override void Awake()
    {
        base.Awake();
        base.OnHurted += CheckGoToStage2;
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetInteger("LastTargetIndex", targetPoints.Length-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckGoToStage2()
    {
        if(LifePoint <= lifePointToStage2)
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
}
