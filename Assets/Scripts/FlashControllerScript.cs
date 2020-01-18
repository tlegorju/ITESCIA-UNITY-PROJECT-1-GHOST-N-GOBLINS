using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashControllerScript : MonoBehaviour
{
    [SerializeField] Material flashMaterial;
    private Material[] originalMaterialsArray;
    [SerializeField] private new Renderer renderer;

    [SerializeField] float timeFlashing=1.0f, intervalFlash=0.2f;
    private float flashingCounter;

    Coroutine flashCoroutine;

    void Awake()
    {
        EntityController controller = GetComponent<EntityController>();
        if(!controller)
        {
            Debug.Log("FlashControllerScript : No entity controller found");
            Destroy(this);
        }

        controller.OnHurted += StartFlashing;
        controller.OnDies += StartFlashing;

        if(renderer!=null)
            renderer = GetComponentInChildren<Renderer>();
        GetOriginalMaterials();
    }

    private void StartFlashing()
    {
        if(flashCoroutine!=null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(FlashOnDamages());
    }

    IEnumerator FlashOnDamages()
    {
        flashingCounter = Time.time + timeFlashing;
        bool flashing = false;

        while(flashingCounter>Time.time)
        {
            if(!flashing)
            {
                ChangeMaterial(flashMaterial);
            }
            else
            {
                ResetMaterial();
            }
            flashing = !flashing;
            yield return new WaitForSeconds(intervalFlash);
        }
        if (flashing)
            ResetMaterial();
    }

    private void GetOriginalMaterials()
    {
        List<Material> tmpMaterialsList = new List<Material>();
        renderer.GetMaterials(tmpMaterialsList);
        originalMaterialsArray = tmpMaterialsList.ToArray();
    }

    void ChangeMaterial(Material newMat)
    {
        Material[] rendererMaterialArray = new Material[renderer.materials.Length];
        for (var i = 0; i < renderer.materials.Length; i++)
        {
            rendererMaterialArray[i] = newMat;
        }
        renderer.materials = rendererMaterialArray;
    }

    void ResetMaterial()
    {
        renderer.materials = originalMaterialsArray;
    }
}
