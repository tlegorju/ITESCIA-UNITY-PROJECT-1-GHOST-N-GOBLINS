using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyPart { Head, Chest, LegLeft, RightLeg, ShoulderLeft, ShoulderRight, ForearmLeft, ForearmRight, LAST};

public class PlayerArmorController : MonoBehaviour
{
    private AudioSource source;
    public Transform[] slotArray = new Transform[(int)BodyPart.LAST];
    public ArmorController[] armorRef = new ArmorController[(int)BodyPart.LAST];

    private bool armorEquipped = false;
    public bool ArmorEquipped { get { return armorEquipped; } }

    public event Action OnEquip = delegate { };
    public event Action OnUnequip = delegate { };

    public AudioClip armorEquipSound;

    private void Awake()
    {
        SimplePlayerController controller = GetComponent<SimplePlayerController>();
        source = GetComponent<AudioSource>();
        if (controller)
            controller.OnHurted += Unequip;
    }
    // Start is called before the first frame update
    void Start()
    {
        Equip();
    }

    // Update is called once per frame
    /*void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (ArmorEquipped)
                Unequip();
            else
                Equip();
        }
    }*/

    public void Unequip()
    {
        if (!armorEquipped)
            return;

        Physics.IgnoreLayerCollision(8, 12, false);
        for (int i=0; i< (int)BodyPart.LAST; i++)
        {
            if(armorRef[i] != null)
                armorRef[i].DetachArmor(Vector3.up * 400 + Vector3.forward * UnityEngine.Random.Range(-200, 200));
        }
        armorEquipped = false;
        OnUnequip();
    }

    public void Equip()
    {
        if (armorEquipped)
            return;

        Physics.IgnoreLayerCollision(8, 12, true);
        for (int i = 0; i < (int)BodyPart.LAST; i++)
        {
            if(armorRef[i]!=null)
            {
                armorRef[i].gameObject.SetActive(true);
                armorRef[i].AttachArmor(slotArray[i]);
            }
        }
        source.clip = armorEquipSound;
        source.Play();
        armorEquipped = true;
        OnEquip();
    }
}
