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

    }
}
