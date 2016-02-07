using UnityEngine;
using ANNs;
using System;

[RequireComponent(typeof(CritterMotor))]
[RequireComponent(typeof(CritterSensors))]
public class CritterANNControl : BaseBehaviour
{

    private NeuralNetwork neuralNetwork;
    private CritterMotor critterMotor;
    private CritterSensors critterSensors;

    [SerializeField]
    private int[] topology = new int[] {3, 5, 2};

    void Awake()
    {
        critterMotor = GetComponent<CritterMotor>();
        critterSensors = GetComponent<CritterSensors>();
    }

    void Start () {
        neuralNetwork = new NeuralNetwork(topology);
        int epoch = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        neuralNetwork.RandomizeWeights(epoch);
    }
	
    void Update () {
        float[] input = new float[] {
            critterSensors.SampleLife(),
            critterSensors.SampleAntenaL(),
            critterSensors.SampleAntenaR()
        };

        float[] output = neuralNetwork.Query(input);

        float vAxis = output[0];

        if (vAxis > 0) critterMotor.MoveForward();
        else critterMotor.Stop();

        float hAxis = output[1];

        if (hAxis > 0.5f) critterMotor.TurnRight();
        else if (hAxis < 0.5f) critterMotor.TurnLeft();
        else critterMotor.StopTurning();
    }

    private void CritterDied()
    {
        enabled = false;
    }
}
