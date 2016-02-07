using UnityEngine;
using System;
using System.Collections.Generic;

public class GeneticGravityManager : SceneManager
{/*
    [SerializeField]
    private BoxCollider2D stageArea;
    [SerializeField]
    private GameObject critterPrefab;
    [SerializeField]
    private GameObject foodPrefab;
    [SerializeField]
    private int crittersAmount = 10;
    [SerializeField]
    private int foodAmount = 40;

    private GameObject crittersParent;
    private GameObject consumablesParent;

    public GameObject[] critters { get; private set; }
    public GameObject[] foods { get; private set; }

    private int crittersAlive = 0;

    protected override void Start()
    {
        if (stageArea == null) throw new Exception("stageArea is not set");
        if (critterPrefab == null) throw new Exception("critterPrefab is not set");
        if (foodPrefab == null) throw new Exception("foodPrefab is not set");

        stageArea.isTrigger = true;

        SpawnFood();
        SpawnCritters();

        base.Start();
    }

    void SpawnCritters()
    {
        crittersParent = new GameObject("Critters");
        crittersParent.transform.SetParent(transform);

        critters = new GameObject[crittersAmount];

        for (int i = 0; i < crittersAmount; i++)
        {
            GameObject critter = (GameObject)Instantiate(critterPrefab);
            critter.transform.SetParent(crittersParent.transform);
            critters[i] = critter;

            CritterSensors critterSensors = critter.GetComponent<CritterSensors>();
            critterSensors.scene = this;

            CritterCtrl critterCtrl = critter.GetComponent<CritterCtrl>();
            critterCtrl.OnDied += CritteriDed;
        }
    }

    void ResetCritters()
    {
        Bounds stageBounds = stageArea.bounds;

        foreach (GameObject critter in critters)
        {
            Vector3 position = new Vector3(
                UnityEngine.Random.Range(stageBounds.min.x, stageBounds.max.x),
                UnityEngine.Random.Range(stageBounds.min.y, stageBounds.max.y),
                0
            );

            Quaternion rotation = Quaternion.AngleAxis(UnityEngine.Random.Range(0, 360), Vector3.forward);

            critter.transform.position = position;
            critter.transform.rotation = rotation;

            CritterCtrl critterCtrl = critter.GetComponent<CritterCtrl>();
            critterCtrl.Initialize();

            crittersAlive++;
        }
    }

    void SpawnFood()
    {
        consumablesParent = new GameObject("Consumables");
        consumablesParent.transform.SetParent(transform);

        foods = new GameObject[foodAmount];

        for (int i = 0; i < foodAmount; i++)
        {
            GameObject food = (GameObject)Instantiate(foodPrefab);
            food.transform.SetParent(consumablesParent.transform);
            foods[i] = food;
        }
    }

    void ResetFood()
    {
        Bounds stageBounds = stageArea.bounds;

        foreach (GameObject food in foods)
        {
            Vector3 position = new Vector3(
                UnityEngine.Random.Range(stageBounds.min.x, stageBounds.max.x),
                UnityEngine.Random.Range(stageBounds.min.y, stageBounds.max.y),
                0
            );

            Quaternion rotation = Quaternion.AngleAxis(UnityEngine.Random.Range(0, 360), Vector3.forward);

            food.transform.position = position;
            food.transform.rotation = rotation;

            FoodCtrl foodCtrl = food.GetComponent<FoodCtrl>();
            foodCtrl.Initialize();
        }
    }

    void CritteriDed(GameObject critter)
    {
        crittersAlive--;

        if (crittersAlive <= 0)
        {
            EndGeneration();
        }
    }

    protected override void StartGeneration()
    {
        crittersAlive = 0;

        ResetFood();
        ResetCritters();

        if (generations.Count > 0)
        {
            Generation prevGeneration = generations.Last.Value;
            float[][][] offspring = prevGeneration.GenerateOffspring(crittersAmount);

            int i = 0;
            foreach (float[][] genes in offspring)
            {
                GameObject critter = critters[i];
                CritterANNControl ann = critter.GetComponent<CritterANNControl>();
                ann.neuralNetwork.SetWeights(genes);
                i++;
            }
        }

        base.StartGeneration();
    }

    protected override void EndGeneration()
    {
        Generation currentGeneration = generations.Last.Value;

        foreach (GameObject critter in critters)
        {
            CritterCtrl ctrl = critter.GetComponent<CritterCtrl>();
            CritterANNControl ann = critter.GetComponent<CritterANNControl>();
            currentGeneration.AddPhenotype(ann.neuralNetwork.ws, ctrl.LifeSpan() - ctrl.naturalLifeSpan());
        }

        base.EndGeneration();
    }*/
}
