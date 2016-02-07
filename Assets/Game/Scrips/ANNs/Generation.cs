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

        public Generation(String name) {
            this.name = name;
            phenotypes = new LinkedList<Phenotype>();
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
            
            Random rnd = new Random();

            KeyValuePair<Phenotype, double>[] candiates = new KeyValuePair<Phenotype, double>[phenotypes.Count];
            int i = 0;
            foreach (Phenotype phenotype in phenotypes)
            {
                double p = (0.5 + rnd.NextDouble() * 0.5) * phenotype.fitness / totalFitness;
                candiates[i] = new KeyValuePair<Phenotype, double>(phenotype, p);
                i++;
            }

            candiates.OrderByDescending(
                (KeyValuePair<Phenotype, double> candidate) => candidate.Value

            );

            int added = 0;
            foreach (KeyValuePair<Phenotype, double> candidate in candiates)
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

            for(int i = 0; i < qty; i++)
            {
                Phenotype parentA = parents[i % parents.Length];
                Phenotype parentB = parents[UnityEngine.Random.Range(0, parents.Length)];
                
                float[][] newGenes = BreedFromCrossover(parentA.genotype, parentB.genotype);

                result[i] = newGenes;
            }

            return result;
        }

        private float[][] BreedFromCrossover(float[][] genesA, float[][] genesB)
        {
            Random rnd = new Random();
            float[][] result = (float[][]) genesA.Clone();
            for(int i = 0; i < genesA.Length; i++)
            {
                result[i] = (float[]) genesA[i].Clone();

                for(int j = 0; j < result[i].Length; j++)
                {
                    if(rnd.Next() > 0.5)
                    {
                        result[i][j] = genesB[i][j];
                    }
                }
            }

            return result;
        }
    }
}
