using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] int damages = 1;
    public int Damages { get { return damages; } }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyWeapon", 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            return;

        if(other.gameObject.CompareTag("Ennemi"))
        {
            other.gameObject.GetComponent<EntityController>().TakeDamages(damages);
        }
        DestroyWeapon();
    }

    protected void DestroyWeapon()
    {
        Destroy(gameObject);
    }
}
