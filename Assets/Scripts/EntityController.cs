using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    public event Action OnSpawn = delegate { },
                        OnMove = delegate { }, 
                        OnAttack = delegate { }, 
                        OnHurted = delegate { },
                        OnDies = delegate { };

    protected virtual void CallOnSpawn()
    {
        OnSpawn();
    }

    protected virtual void CallOnMove()
    {
        OnMove();
    }

    protected virtual void CallOnAttack()
    {
        OnAttack();
    }

    protected virtual void CallOnHurted()
    {
        OnHurted();
    }

    protected virtual void CallOnDies()
    {
        OnDies();
    }
}
