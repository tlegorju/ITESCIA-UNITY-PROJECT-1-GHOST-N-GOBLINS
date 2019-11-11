using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] float damages = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
            return;

        if(other.gameObject.CompareTag("Ennemi"))
        {
            other.gameObject.GetComponent<EnnemiController>().TakeDamages(damages);
        }
        DestroyWeapon();
    }

    protected void DestroyWeapon()
    {
        Destroy(gameObject);
    }
}
