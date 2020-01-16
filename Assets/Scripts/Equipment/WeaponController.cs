using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    [SerializeField] int damages = 1;
    public int Damages { get { return damages; } }
    [SerializeField] protected float weaponDuration = 2.0f;
    [SerializeField] protected float translationSpeed = 300f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Invoke("DestroyWeapon", weaponDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            return;

        if (other.gameObject.CompareTag("Ennemi"))
        {
            other.gameObject.GetComponent<EntityController>().TakeDamages(Damages);
        }
        DestroyWeapon();
    }


    protected void DestroyWeapon()
    {
        Destroy(gameObject);
    }
}
