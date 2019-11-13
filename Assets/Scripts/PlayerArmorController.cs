using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyPart { Head, Chest, Legs, Arm};

public class PlayerArmorController : MonoBehaviour
{
    public Transform[] slotArray = new Transform[3];
    public ArmorController[] armorRef = new ArmorController[3];

    private bool armorEquipped = true;
    public bool ArmorEquipped { get { return armorEquipped; } }

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        Equip();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.E))
        {
            if (equipped)
                Unequip();
            else
                Equip();
        }*/
    }

    public void Unequip()
    {
        Physics.IgnoreLayerCollision(8, 12, false);
        for (int i=0; i<3; i++)
        {
            armorRef[i].DetachArmor(Vector3.up * 400 + Vector3.forward * Random.Range(-200, 200));
        }
        armorEquipped = false;
    }

    public void Equip()
    {
        Physics.IgnoreLayerCollision(8, 12, true);
        for (int i = 0; i < 3; i++)
        {
            if(armorRef[i]!=null)
                armorRef[i].AttachArmor(slotArray[i]);
        }
        armorEquipped = true;
    }
}
