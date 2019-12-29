using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnnemiController
{
    [SerializeField] float lifePointToStage2 = 5;

    protected override void Awake()
    {
        base.Awake();
        base.OnHurted += CheckGoToStage2;
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
}
