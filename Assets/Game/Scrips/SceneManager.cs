using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using ANNs;

public class SceneManager : BaseBehaviour
{
    public int generationNumber { get; protected set; }

    [SerializeField]
    private Text generationTxt;
    [SerializeField]
    private Text infoTxt;

    protected LinkedList<Generation> generations;

    protected float bestFitness = 0;
    protected float avgFitness = 0;

    protected virtual void Start()
    {
        generationNumber = 0;
        generations = new LinkedList<Generation>();
        StartGeneration();
    }

    protected virtual void StartGeneration()
    {
        generationNumber++;

        if(generationTxt != null)
        {
            generationTxt.text = "GEN " + generationNumber;
        }

        Debug.Log("New generation " + generationNumber);
        Generation currentGeneration = new Generation("G" + generationNumber);
        generations.AddLast(currentGeneration);
    }

    protected virtual void EndGeneration()
    {
        Generation currentGeneration = generations.Last.Value;
        Debug.Log(String.Format("Generation ended, fitness {0}s", currentGeneration.BestFitness() ));

        avgFitness = (avgFitness * (generations.Count - 1) + currentGeneration.BestFitness()) / generations.Count;

        if (bestFitness < currentGeneration.BestFitness()) bestFitness = currentGeneration.BestFitness();

        if (infoTxt != null)
        {
            infoTxt.text = String.Format(
                "Last Fit {0:0.00}s, Avg Fit {1:0.00}s Best Fit {2:0.00}s",
                currentGeneration.BestFitness(),
                avgFitness,
                bestFitness
            );
        }
        
        Invoke("StartGeneration", 3);
    }
}
