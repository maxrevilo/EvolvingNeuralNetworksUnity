using System;

namespace ANNs
{
    public class NeuralNetwork
    {

        public float[][] ws { get; protected set; }
        public int[] topology { get; protected set; }

        private float[][] tempResults;

        public NeuralNetwork(int[] topology)
        {
            if (topology.Length < 2)
                throw new ArgumentException("Topology should have at least 2 layers");
            this.topology = topology;

            tempResults = new float[topology.Length][];

            for (int i = 0; i < topology.Length; i++)
            {
                tempResults[i] = new float[topology[i]];
            }

            ws = new float[topology.Length - 1][];
            for (int i = 0; i < topology.Length - 1; i++)
            {
                ws[i] = new float[topology[i + 1] * topology[i]];
            }
        }

        public float[] Query(float[] input)
        {
            if (input.Length != topology[0])
                throw new ArgumentOutOfRangeException("input length");

            for (int i = 0; i < topology[0]; i++)
            {
                tempResults[0][i] = input[i];
            }

            for (int x = 1; x < topology.Length; x++)
            {
                for (int j = 0; j < topology[x]; j++)
                {
                    float sum = 0;
                    int l = topology[x];
                    for (int i = 0; i < topology[x - 1]; i++)
                    {
                        sum += ws[x - 1][i * l + j] * tempResults[x - 1][i];
                    }

                    tempResults[x][j] = Activation(sum);
                }
            }

            return tempResults[topology.Length - 1];
        }

        public void RandomizeWeights(int seed, float min = -100, float max = 100)
        {
            Random rand = new Random(seed);

            for(int i = 0; i < ws.Length; i++)
            {
                for (int j = 0; j < ws[i].Length; j++) {
                    ws[i][j] = min + (float) rand.NextDouble() * (max - min);
                }
            }
        }

        public float Activation(float f)
        {
            return LogSigmoid(f);
        }

        public float LogSigmoid(float x)
        {
            if (x < -45f) return 0f;
            else if (x > 45f) return 1f;
            else return 1f / (1f + (float)Math.Exp(x));
        }
    }
}


