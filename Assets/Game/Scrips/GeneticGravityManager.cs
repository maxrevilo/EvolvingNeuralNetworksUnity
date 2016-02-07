using UnityEngine;
using System;
using ANNs;

public class GeneticGravityManager : SceneManager
{
    [SerializeField]
    private BoxCollider stageArea;
    [SerializeField]
    private GameObject boxPrefab;
    [SerializeField]
    private Transform targetDestination;
    [SerializeField]
    private int boxesAmount = 100;

    private GameObject boxesParent;

    public GameObject[] boxes { get; private set; }

    protected override void Start()
    {
        if (stageArea == null) throw new Exception("stageArea is not set");
        if (boxPrefab == null) throw new Exception("boxPrefab is not set");

        stageArea.isTrigger = true;

        SpawnBoxes();

        base.Start();
    }

    void SpawnBoxes()
    {
        boxesParent = new GameObject("Boxes");
        boxesParent.transform.SetParent(transform);

        boxes = new GameObject[boxesAmount];

        Bounds stageBounds = stageArea.bounds;

        for (int i = 0; i < boxesAmount; i++)
        {

            GameObject box = (GameObject)Instantiate(boxPrefab);
            box.transform.SetParent(boxesParent.transform);
            boxes[i] = box;

            Vector3 position = new Vector3(
                UnityEngine.Random.Range(stageBounds.min.x, stageBounds.max.x),
                UnityEngine.Random.Range(stageBounds.min.y, stageBounds.max.y),
                UnityEngine.Random.Range(stageBounds.min.z, stageBounds.max.z)
            );

            box.transform.position = position;
        }
    }

    void ResetBoxes()
    {
        foreach (GameObject box in boxes) {
               
            Quaternion rotation = Quaternion.AngleAxis(
                UnityEngine.Random.Range(0, 360),
                Vector3.forward
            );
            box.transform.rotation = rotation;

            Rigidbody boxBody = box.GetComponent<Rigidbody>();
            if(boxBody)
            {
                boxBody.angularVelocity = new Vector3(
                UnityEngine.Random.Range(-0.1f, 0.1f),
                UnityEngine.Random.Range(-0.1f, 0.1f),
                UnityEngine.Random.Range(-0.1f, 0.1f)
            );
            }
        }
    }

    protected override void StartGeneration()
    {
        ResetBoxes();
        
        if (generations.Count > 0)
        {
            Generation prevGeneration = generations.Last.Value;
            float[][][] offspring = prevGeneration.GenerateOffspring(boxesAmount);

            int i = 0;
            foreach (float[][] genes in offspring)
            {
                GameObject box = boxes[i];
                setGenesToBox(genes, box);
                i++;
            }
        }

        base.StartGeneration();

        EndGeneration();
    }

    protected override void EndGeneration()
    {
        Generation currentGeneration = generations.Last.Value;
        foreach (GameObject box in boxes)
        {
            currentGeneration.AddPhenotype(getGenesFromBox(box), getBoxFitness(box));
        }

        base.EndGeneration();
    }

    protected void setGenesToBox(float[][] genes, GameObject box)
    {
        box.transform.position = new Vector3(genes[0][0], genes[0][1], genes[0][2]);
    }

    protected float[][] getGenesFromBox(GameObject box)
    {
        float[][] result = new float[1][];
        result[0] = new float[3];
        result[0][0] = box.transform.position.x;
        result[0][1] = box.transform.position.y;
        result[0][2] = box.transform.position.z;
        return result;
    }

    protected float getBoxFitness(GameObject box)
    {
        return 100 / Vector3.Distance(box.transform.position, targetDestination.position);
    }
}
