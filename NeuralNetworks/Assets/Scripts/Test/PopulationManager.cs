using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopulationManager : MonoBehaviour
{
    public GameObject TankPrefab;
    public GameObject MinePrefab;

    public int PopulationCount = 40;
    public int MinesCount = 50;

    public Vector3 SceneHalfExtents = new Vector3 (20.0f, 0.0f, 20.0f);

    public float GenerationDuration = 20.0f;
    public int IterationCount = 1;

    public int EliteCount = 4;
    public float MutationChance = 0.10f;
    public float MutationRate = 0.01f;

    public int InputsCount = 4;
    public int HiddenLayers = 1;
    public int OutputsCount = 2;
    public int NeuronsCountPerHL = 7;
    public float Bias = 1f;
    public float P = 0.5f;


    GeneticAlgorithm genAlg;

    List<Tank> populationGOs = new List<Tank>();
    List<Genome> population = new List<Genome>();
    List<NeuralNetwork> brains = new List<NeuralNetwork>();
    List<GameObject> mines = new List<GameObject>();
     
    float accumTime = 0;
    int generation = 0;

    static PopulationManager instance = null;

    public static PopulationManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PopulationManager>();

            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Create and confiugre the Genetic Algorithm
        genAlg = new GeneticAlgorithm(EliteCount, MutationChance, MutationRate);

        GenerateInitialPopulation();
        CreateMines();
    }

    // Generate the random initial population
    void GenerateInitialPopulation()
    {
        generation = 0;

        // Destroy previous tanks (if there are any)
        DestroyTanks();
        
        for (int i = 0; i < PopulationCount; i++)
        {
            NeuralNetwork brain = CreateBrain();
            
            Genome genome = new Genome(brain.GetTotalWeightsCount());

            brain.SetWeights(genome.genome);
            brains.Add(brain);

            population.Add(genome);
            populationGOs.Add(CreateTank(genome, brain));
        }

        accumTime = 0.0f;
    }

    // Creates a new NeuralNetwork
    NeuralNetwork CreateBrain()
    {
        NeuralNetwork brain = new NeuralNetwork();

        // Add first neuron layer that has as many neurons as inputs
        brain.AddFirstNeuronLayer(InputsCount, Bias, P);

        for (int i = 0; i < HiddenLayers; i++)
        {
            // Add each hidden layer with custom neurons count
            brain.AddNeuronLayer(NeuronsCountPerHL, Bias, P);
        }

        // Add the output layer with as many neurons as outputs
        brain.AddNeuronLayer(OutputsCount, Bias, P);

        return brain;
    }

    // Evolve!!!
    void Epoch()
    {
        // Increment generation counter
        generation++;

        // Evolve each genome and create a new array of genomes
        Genome[] newGenomes = genAlg.Epoch(population.ToArray());

        // Clear current population
        population.Clear();

        // Add new population
        population.AddRange(newGenomes);

        // Set the new genomes as each NeuralNetwork weights
        for (int i = 0; i < PopulationCount; i++)
        {
            NeuralNetwork brain = brains[i];

            brain.SetWeights(newGenomes[i].genome);

            populationGOs[i].SetBrain(newGenomes[i], brain);
            populationGOs[i].transform.position = GetRandomPos();
            populationGOs[i].transform.rotation = GetRandomRot();
        }
    }

    // Update is called once per frame
    void FixedUpdate () 
	{
        for (int i = 0; i < Mathf.Clamp(IterationCount, 1, 80); i++)
        {
            foreach (Tank t in populationGOs)
            {
                // Get the nearest mine
                GameObject mine = GetNearestMine(t.transform.position);

                // Set the nearest mine to current tank
                t.SetNearestMine(mine);

                // Think!! 
                t.Think(Time.fixedDeltaTime);

                // Just adjust tank position when reaching world extents
                Vector3 pos = t.transform.position;
                if (pos.x > SceneHalfExtents.x)
                    pos.x = -SceneHalfExtents.x;
                else if (pos.x < -SceneHalfExtents.x)
                    pos.x = SceneHalfExtents.x;

                if (pos.z > SceneHalfExtents.z)
                    pos.z = -SceneHalfExtents.z;
                else if (pos.z < -SceneHalfExtents.z)
                    pos.z = SceneHalfExtents.z;

                // Set tank position
                t.transform.position = pos;
            }

            // Check the time to evolve
            accumTime += Time.fixedDeltaTime;
            if (accumTime >= GenerationDuration)
            {
                accumTime = 0.0f;
                Epoch();
            }
        }
	}

#region Helpers
    Tank CreateTank(Genome genome, NeuralNetwork brain)
    {
        Vector3 position = GetRandomPos();
        GameObject go = Instantiate<GameObject>(TankPrefab, position, GetRandomRot());
        Tank t = go.GetComponent<Tank>();
        t.SetBrain(genome, brain);
        return t;
    }

    void DestroyMines()
    {
        foreach (GameObject go in mines)
            Destroy(go);

        mines.Clear();
    }

    void DestroyTanks()
    {
        foreach (Tank go in populationGOs)
            Destroy(go);

        populationGOs.Clear();
        population.Clear();
        brains.Clear();
    }

    void CreateMines()
    {
        // Destroy previous created mines
        DestroyMines();

        for (int i = 0; i < MinesCount; i++)
        {
            Vector3 position = GetRandomPos();
            GameObject go = Instantiate<GameObject>(MinePrefab, position, Quaternion.identity);
            mines.Add(go);
        }
    }

    public void RelocateMine(GameObject mine)
    {
        mine.transform.position = GetRandomPos();
    }

    Vector3 GetRandomPos()
    {
        return new Vector3(Random.value * SceneHalfExtents.x * 2.0f - SceneHalfExtents.x, 0.0f, Random.value * SceneHalfExtents.z * 2.0f - SceneHalfExtents.z); 
    }

    Quaternion GetRandomRot()
    {
        return Quaternion.AngleAxis(Random.value * 360.0f, Vector3.up);
    }

    GameObject GetNearestMine(Vector3 pos)
    {
        GameObject nearest = mines[0];
        float distance = (pos - nearest.transform.position).sqrMagnitude;

        foreach (GameObject go in mines)
        {
            float newDist = (go.transform.position - pos).sqrMagnitude;
            if (newDist < distance)
            {
                nearest = go;
                distance = newDist;
            }
        }

        return nearest;
    }   

    void OnGUI()
    {
        string strFormat = "Generation: {0}";

        GUILayout.Label(string.Format(strFormat, generation));
    }
#endregion

}
