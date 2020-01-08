using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyPart { Head, Chest, LegLeft, RightLeg, ShoulderLeft, ShoulderRight, ForearmLeft, ForearmRight, LAST};

public class PlayerArmorController : MonoBehaviour
{
    public Transform[] slotArray = new Transform[(int)BodyPart.LAST];
    public ArmorController[] armorRef = new ArmorController[(int)BodyPart.LAST];

    private bool armorEquipped = false;
    public bool ArmorEquipped { get { return armorEquipped; } }

    private void Awake()
    {
        SimplePlayerController controller = GetComponent<SimplePlayerController>();
        if(controller)
            controller.OnHurted += Unequip;
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
        if (!armorEquipped)
            return;

        Physics.IgnoreLayerCollision(8, 12, false);
        for (int i=0; i< (int)BodyPart.LAST; i++)
        {
            if(armorRef[i] != null)
                armorRef[i].DetachArmor(Vector3.up * 400 + Vector3.forward * Random.Range(-200, 200));
        }
        armorEquipped = false;
    }

    public void Equip()
    {
        if (armorEquipped)
            return;

        Physics.IgnoreLayerCollision(8, 12, true);
        for (int i = 0; i < (int)BodyPart.LAST; i++)
        {
            if(armorRef[i]!=null)
                armorRef[i].AttachArmor(slotArray[i]);
        }
        armorEquipped = true;
    }
}
