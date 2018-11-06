using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour
{
    public float Speed = 10.0f;
    public float RotSpeed = 10.0f;

    Genome genome;
	NeuralNetwork brain;
    GameObject nearMine;
    float[] inputs;

    // Sets a brain to the tank
    public void SetBrain(Genome genome, NeuralNetwork brain)
    {
        this.genome = genome;
        this.brain = brain;
        inputs = new float[brain.InputsCount];
    }

    // Used by the PopulationManager to set the closest mine
    public void SetNearestMine(GameObject mine)
    {
        nearMine = mine;
    }
	
	// Update is called once per frame
	public void Think(float dt) 
	{
        // Tank position
        Vector3 pos = this.transform.position;

        // Direction to closest mine (normalized!)
        Vector3 dirToMine = (nearMine.transform.position - pos).normalized;

        // Current tank view direction (it's always normalized)
        Vector3 dir = this.transform.forward;

        // Sets current tank view direction and direction to the mine as inputs to the Neural Network
        inputs[0] = dirToMine.x;
        inputs[1] = dirToMine.z;
        inputs[2] = dir.x;
        inputs[3] = dir.z;

        // Think!!! 
        float[] output = brain.Synapsis(inputs);

        // Use the outputs as the force of both tank tracks
        float rotFactor = Mathf.Clamp((output[1] - output[0]), -1.0f, 1.0f);

        // Rotate the tank as the rotation factor
        this.transform.rotation *= Quaternion.AngleAxis(rotFactor * RotSpeed * dt, Vector3.up);

        // Move the tank in current forward direction
        pos += this.transform.forward * Speed * dt;

        // Sets current position
        this.transform.position = pos;

        // If mine is close enough, take it!
        if ((pos - nearMine.transform.position).sqrMagnitude <= 2.0f)
        {
            // Increment tank score
            genome.fitness++;

            // Move the mine to a random position in the screen
            PopulationManager.Instance.RelocateMine(nearMine);
        }
	}
}
