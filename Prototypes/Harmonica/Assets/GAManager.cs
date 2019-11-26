using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAManager : MonoBehaviour
{
    public int popSize, 
               surSize, 
               evolveMax;

    public float mutationRate;

    public int[,] target;

    private MusicLibrary music;
    private List<Performance> population;

    private bool playing;

    private void Start()
    {
        music = GetComponent<MusicLibrary>();
        population = new List<Performance>();
        target = MusicCollection.songs[2];
        CreateInitialPopulation();
        AssessPopulation();
        playing = true;
    }

    private void Update()
    {
        if (!playing)
        {
            if (Input.GetKeyDown("e"))
            {
                EvolveAction();
            }
        }
    }

    private void FixedUpdate()
    {
        
        if(playing)
        {
            playing = music.PlaySample(population[0].attempt);
            
        }
    }

    private void EvolveAction()
    {
        for(int i = 0; i < evolveMax; i++)
        {
            Evolve();
        }
        float newSpeed = 0.75f * music.noteSpeed;
        if(newSpeed < music.maxSpeed)
        {
            newSpeed = music.maxSpeed;
        }
        music.noteSpeed = newSpeed;
        music.sourcesAllowed++;
        if(music.sourcesAllowed > music.sources.Length)
        {
            music.sourcesAllowed = music.sources.Length;
        }
        playing = true;
    }

    private void Evolve()
    {
        AssessPopulation();
        List<Performance> survivors = population.GetRange(0, surSize);
        List<Performance> children = new List<Performance>();
        for(int i = 0; i < popSize - surSize; i++)
        {
            children.Add(MakeChild());
        }
        population.Clear();
        population.AddRange(survivors);
        population.AddRange(children);

    }

    private Performance MakeChild()
    {
        Performance child = new Performance();
        child.attempt = new int[target.GetLength(0), target.GetLength(1)];
        Performance a = ChooseParent();
        Performance b = ChooseParent();
        for(int i = 0; i < child.attempt.GetLength(0); i++)
        {
            for(int j = 0; j < child.attempt.GetLength(1); j++)
            {
                if(Random.Range(0, 100) % 2 == 0)
                {
                    child.attempt[i, j] = a.attempt[i, j];
                }
                else
                {
                    child.attempt[i, j] = b.attempt[i, j];
                }
            }
        }
        if(Random.Range(0f, 1f) < mutationRate)
        {
            child.attempt[Random.Range(0, child.attempt.GetLength(0)), Random.Range(0, child.attempt.GetLength(1))]
                = Random.Range(-1, music.harmonicaClips.Length);
        }
        return child;
    }

    private Performance ChooseParent()
    {
        Performance a = population[Random.Range(0, popSize)];
        Performance b = population[Random.Range(0, popSize)];
        if(a.fitness < b.fitness)
        {
            return a;
        }
        else
        {
            return b;
        }
    }

    private void AssessPopulation()
    {
        foreach(Performance p in population)
        {
            p.fitness = AssessPerformance(p);
        }
        population = population.OrderBy(x => x.fitness).ToList();
    }

    private int AssessPerformance(Performance p)
    {
        int score = 0;
        for(int i = 0; i < p.attempt.GetLength(0); i++)
        {
            for(int j = 0; j < p.attempt.GetLength(1); j++)
            {
                score += (target[i, j] - p.attempt[i, j]) * (target[i, j] - p.attempt[i, j]);
            }
        }
        return score;
    }

    private void CreateInitialPopulation()
    {
        for(int i = 0; i < popSize; i++)
        {
            Performance p = new Performance();


            p.attempt = (int[,]) target.Clone();
            /*
            for(int j = 0; j < p.attempt.GetLength(0); j++)
            {
                for(int k = 0; k < p.attempt.GetLength(1); k++)
                {
                    if(Random.Range(0, 100) % 2 == 0)
                    {
                        p.attempt[j, k] = Random.Range(-1, music.harmonicaClips.Length);
                    }
                }
            }
            */
            population.Add(p);
        }
    }
}
