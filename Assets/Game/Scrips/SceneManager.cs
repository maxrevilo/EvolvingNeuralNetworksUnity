using UnityEngine;
using System;
using System.Collections.Generic;
using ANNs;

public class SceneManager : BaseBehaviour
{
    public int generationNumber { get; protected set; }

    protected LinkedList<Generation> generations;

    protected virtual void Start()
    {
        generationNumber = 0;
        generations = new LinkedList<Generation>();
        StartGeneration();
    }

    protected virtual void StartGeneration()
    {
        generationNumber++;
        Debug.Log("New generation " + generationNumber);
        Generation currentGeneration = new Generation("G" + generationNumber);
        generations.AddLast(currentGeneration);
    }

    protected virtual void EndGeneration()
    {
        Generation currentGeneration = generations.Last.Value;
        Debug.Log(String.Format("Generation ended, fitness {0}s", currentGeneration.BestFitness() ));
        
        Invoke("StartGeneration", 3);
    }
}
