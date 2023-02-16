using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Block", menuName ="MyGame/Block", order =1)]
public class Block : ScriptableObject
{
    [SerializeField] private string nameBlock = "Element";
    [Space(10)]
    [SerializeField] private GameObject prefab = null;
    [SerializeField] private Material material = null;
    [SerializeField] private ElementType elementType = ElementType.None;
    [Space(10)]
    [SerializeField] private float temperature = 22.5f;
    [SerializeField] private float weight = 1.0f;

    public GameObject GetPrefab()
    {
        if (prefab == null)
        return null;

        var outPrefab = prefab;
        outPrefab.GetComponent<Renderer>().material = material;

        return outPrefab;
    }
}
