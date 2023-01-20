using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DestructibleObj : MonoBehaviour
{
    [SerializeField]
    private Material damageMat;

    [SerializeField]
    internal int health = 2;

    [SerializeField]
    internal string objectType;

    public delegate void DestroyObjAction(GameObject obj);
    public static event DestroyObjAction onDestroyedObj;

    private MeshRenderer meshRenderer;
    private Material[] originalMats;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        
        Material[] mats = meshRenderer.materials;
        originalMats = (Material[])mats.Clone();
    }

    public int DamageObj(int value)
    {
        health -= value;
        if (health <= 0)
        {
            onDestroyedObj(gameObject);
        }

        DamageFlash();

        HandleChildren();

        return health;
    }

    private void DamageFlash()
    {
        meshRenderer.materials = Enumerable.Repeat<Material>(damageMat, originalMats.Length).ToArray();

        StartCoroutine(RestoreMaterials());
    }

    private IEnumerator RestoreMaterials()
    {
        yield return new WaitForSeconds(0.1f);

        meshRenderer.materials = originalMats;
    }

    private void HandleChildren()
    {
        if (transform.childCount == 0)
            return;

        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child == this.transform)
                continue;

            if (child.CompareTag("Destructible"))
            {
                child.parent = null;
                child.GetComponent<Collider>().enabled = true;

                DestructibleObj destructibleObj = child.gameObject.AddComponent<DestructibleObj>();
                child.gameObject.AddComponent<Rigidbody>();
                child.gameObject.GetComponent<Rigidbody>().mass = 0.05f;
                destructibleObj.health = 1;
            }
        }
    }
}
