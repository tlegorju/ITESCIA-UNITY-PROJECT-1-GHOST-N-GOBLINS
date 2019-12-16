using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;

    private bool canTakeDamages = true;
    [SerializeField] float invulnerabilityDuration = 2.0f;

    public event Action OnHurted = delegate { };
    public event Action OnDie = delegate { };

    private void Awake()
    {
        health = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ennemi"))
        {
            TakeDamages(1);
        }
    }

    private void TakeDamages(int damage)
    {
        if (!canTakeDamages)
            return;

        health -= damage;

        PlayerArmorController armorController = GetComponent<PlayerArmorController>();

        if (!armorController.ArmorEquipped)
            Dies();
        else
            Hurted();
    }

    private void Hurted()
    {
        StartCoroutine("FlashOnDamages");
        OnHurted();
    }

    private void Dies()
    {
        Debug.Log("Dies");
        OnDie();
        Destroy(gameObject);
    }

    IEnumerator FlashOnDamages()
    {
        int flashingCount = 8;
        float flashingInterval = .1f;
        bool displayModel = true;

        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

        EnableDamages(false);
        for (int counter = 0; counter < flashingCount; counter++)
        {
            displayModel = !displayModel;
            for (int i = 0; i < renderers.Length; i++)
                renderers[i].enabled = displayModel;

            yield return new WaitForSeconds(flashingInterval);
        }
        EnableDamages(true);
    }

    private void EnableDamages(bool enable)
    {
        canTakeDamages = enable;
        Physics.IgnoreLayerCollision(9, 10, !enable);
    }
}
