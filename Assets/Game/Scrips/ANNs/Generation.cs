using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANNs
{
    public class Generation
    {
        public LinkedList<Phenotype> phenotypes;
        private Phenotype bestPhenotype;

        public String name { get; private set; }

        public double rouleteBiasToTheBest = 0.5;
        public bool useCrossover = true;
        public double crossoverMotherBias = 0.8;
        public double mutationProbability = 0.1;
        public float maxMutationDisplacement = 10f;

        private Random rnd;

        public Generation(String name)
        {
            this.name = name;
            phenotypes = new LinkedList<Phenotype>();
            rnd = new Random();
        }

        public void AddPhenotype(float[][] genotype, float fitness)
        {
            Phenotype phenotype = new Phenotype() { genotype = genotype, fitness = fitness };
            phenotypes.AddLast(phenotype);

            if (bestPhenotype == null || bestPhenotype.fitness < phenotype.fitness) bestPhenotype = phenotype;
        }

        public float BestFitness()
        {
            if (bestPhenotype != null) return bestPhenotype.fitness;
            else return 0;
        }

        public Phenotype BestPhenotype()
        {
            return bestPhenotype;
        }

        public Phenotype[] ChooseBestByRoulete(int qty)
        {
            Phenotype[] result = new Phenotype[qty];

            float totalFitness = 0;

            foreach (Phenotype phenotype in phenotypes)
            {
                totalFitness += phenotype.fitness;
            }

            KeyValuePair<Phenotype, double>[] candiates = new KeyValuePair<Phenotype, double>[phenotypes.Count];
            int i = 0;
            foreach (Phenotype phenotype in phenotypes)
            {
                double p = (rouleteBiasToTheBest + rnd.NextDouble() * (1 - rouleteBiasToTheBest)) * phenotype.fitness / totalFitness;
                candiates[i] = new KeyValuePair<Phenotype, double>(phenotype, p);
                i++;
            }

            IOrderedEnumerable<KeyValuePair<Phenotype, double>> bestCandidates = candiates.OrderByDescending(
                (KeyValuePair<Phenotype, double> candidate) => candidate.Value
            );

            int added = 0;
            foreach (KeyValuePair<Phenotype, double> candidate in bestCandidates)
            {
                result[added++] = candidate.Key;
                if (added >= qty) break;
            }

            return result;
        }

        public float[][][] GenerateOffspring(int qty)
        {
            float[][][] result = new float[qty][][];

            Phenotype[] parents = ChooseBestByRoulete(phenotypes.Count / 3);

            for (int i = 0; i < qty; i++)
            {
                Phenotype parentA = parents[i % parents.Length];
                Phenotype parentB = parents[UnityEngine.Random.Range(0, parents.Length)];

                float[][] newGenes = parentA.genotype;

                if (useCrossover)
                {
                    newGenes = BreedFromCrossover(newGenes, parentB.genotype);
                }

                if (mutationProbability != 0)
                {
                    applyMutation(newGenes);
                }

                result[i] = newGenes;
            }

            return result;
        }

        private float[][] BreedFromCrossover(float[][] genesA, float[][] genesB)
        {
            float[][] result = (float[][])genesA.Clone();
            for (int i = 0; i < genesA.Length; i++)
            {
                result[i] = (float[])genesA[i].Clone();

                for (int j = 0; j < result[i].Length; j++)
                {
                    if (rnd.NextDouble() > crossoverMotherBias)
                    {
                        result[i][j] = genesB[i][j];
                    }
                }
            }

            return result;
        }

        private void applyMutation(float[][] genes)
        {
            for (int i = 0; i < genes.Length; i++)
            {
                for (int j = 0; j < genes[i].Length; j++)
                {
                    if (mutationProbability >= rnd.NextDouble())
                    {
                        genes[i][j] = maxMutationDisplacement * 2f * (float)(rnd.NextDouble() - 0.5);
                    }
                }
            }

        }
    }
}
