using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributionSpawner : MonoBehaviour
{
    public int numDuplicates = 10;
    public GameObject prefab;
    public float minScale = 0.5f;
    public float maxScale = 2.0f;
    public int maxIterations = 50;

    public float frequency;
    public int phaseShift;

    private List<Transform> duplicatedGameObjects = new List<Transform>();
    private GameObject groundObject;

    void Awake()
    {
        duplicatedGameObjects = new List<Transform>();
        groundObject = GameObject.FindGameObjectWithTag("Ground");

        for (int i = 0; i < numDuplicates; i++)
        {
            GameObject duplicate = Instantiate(prefab);
            duplicatedGameObjects.Add(duplicate.transform);
            duplicate.transform.parent = transform;
            duplicate.transform.localScale = Vector3.one;
        }
    }

    void Start()
    {
        StartCoroutine(DistributeObjectsOverGround());
    }

    IEnumerator DistributeObjectsOverGround()
    {
        Vector3 center = prefab.transform.position;
        float radius = 10.0f;
        float distributionRate = 3.0f;

        for (int i = 0; i < duplicatedGameObjects.Count; i++)
        {
            distributionRate = Mathf.Sin(Mathf.PI * ((float)i / (float)numDuplicates));
            Vector3 position = center + Random.insideUnitSphere * radius * distributionRate;
            position.y = groundObject.GetComponent<Terrain>().SampleHeight(position);

            duplicatedGameObjects[i].position = position;
        }
        ScaleUsingSine();
        yield return null;
    }

    public void ScaleUsingSine()
    {
        foreach (Transform child in duplicatedGameObjects)
        {
            float scaleFactor = Mathf.Lerp(minScale, maxScale, Mathf.Sin(child.position.x * frequency + phaseShift));

            child.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        }
        Invoke("Static", 20);
    }

    public void Static()
    {
        foreach (Transform child in duplicatedGameObjects)
        {
            child.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

}
