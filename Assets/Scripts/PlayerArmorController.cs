using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyPart { Head, Chest, Legs, Arm};

public class PlayerArmorController : MonoBehaviour
{
    public Transform[] slotArray = new Transform[3];
    public GameObject[] armorRef = new GameObject[3];

    private bool equipped = true;

    private void Awake()
    {
        armorRef[(int)BodyPart.Head] = GameObject.Find("Armor_Head");
        armorRef[(int)BodyPart.Chest] = GameObject.Find("Armor_Chest");
        armorRef[(int)BodyPart.Legs] = GameObject.Find("Armor_Leg");
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
        for(int i=0; i<3; i++)
        {
            Rigidbody tmpRigidbody = armorRef[i].GetComponent<Rigidbody>();
            tmpRigidbody.isKinematic = false;
            armorRef[i].transform.SetParent(null);
            tmpRigidbody.AddForce(Vector3.up * 400+Vector3.forward*Random.Range(-200,200));
        }
        equipped = false;
    }

    public void Equip()
    {
        for (int i = 0; i < 3; i++)
        {
            armorRef[i].transform.parent = (slotArray[i]);
            armorRef[i].transform.localPosition = Vector3.zero;
            armorRef[i].transform.localRotation = Quaternion.identity;
            armorRef[i].GetComponent<Rigidbody>().isKinematic = true;
        }
        equipped = true;
    }
}
