using UnityEngine;
using ANNs;
using System;

[RequireComponent(typeof(CritterMotor))]
[RequireComponent(typeof(CritterSensors))]
public class CritterANNControl : BaseBehaviour
{
    public NeuralNetwork neuralNetwork { get; private set; }
    private CritterMotor critterMotor;
    private CritterSensors critterSensors;

    [SerializeField]
    private int[] topology = new int[] { 3, 5, 2 };
    [SerializeField]
    private float minNeuronInitialWeight = -10;
    [SerializeField]
    private float maxNeuronInitialWeight = 10;

    void Awake()
    {
        critterMotor = GetComponent<CritterMotor>();
        critterSensors = GetComponent<CritterSensors>();
    }

    void Start()
    {
        neuralNetwork = new NeuralNetwork(topology);
        int seed = UnityEngine.Random.Range(int.MinValue + 1, int.MaxValue - 1);
        neuralNetwork.RandomizeWeights(seed, minNeuronInitialWeight, maxNeuronInitialWeight);
    }

    void FixedUpdate()
    {
        float[] input = new float[] {
            critterSensors.SampleLife() / 100f,
            5f / critterSensors.SampleAntenaL(),
            5f / critterSensors.SampleAntenaR()
        };

        float[] output = neuralNetwork.Query(input);

        float vAxis = output[0];

        if (vAxis > 0.5f) critterMotor.MoveForward();
        else critterMotor.Stop();

        float hAxis = output[1];

        if (hAxis > 0.66f) critterMotor.TurnRight();
        else if (hAxis < 0.33f) critterMotor.TurnLeft();
        else critterMotor.StopTurning();
    }

    private void CritterDied()
    {
        enabled = false;
    }

    private void CritterRespawned()
    {
        enabled = true;
    }
}
