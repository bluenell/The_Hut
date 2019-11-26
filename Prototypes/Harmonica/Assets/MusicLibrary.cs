using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLibrary : MonoBehaviour
{
    public AudioClip[]   harmonicaClips;

    public int sourcesAllowed;

    public float minSpeed, 
                 maxSpeed, 
                 timeCounter, 
                 noteSpeed;

    private int           noteCounter;
    public AudioSource[] sources;

    private void Awake()
    {
        harmonicaClips = Resources.LoadAll<AudioClip>("HarmonicaFiles/Harmonica Samples");
        
        GameObject[] voices = GameObject.FindGameObjectsWithTag("Voice");
        sources = new AudioSource[voices.Length];
        for(int i = 0; i < voices.Length; i++)
        {
            sources[i] = voices[i].GetComponent<AudioSource>();
        }
        
        noteSpeed = minSpeed;

        MusicCollection.songs = new List<int[,]>();
        MusicCollection.songs.Add(MusicCollection.dixie);
        MusicCollection.songs.Add(MusicCollection.ring);
        MusicCollection.songs.Add(MusicCollection.smokey);
    }

    public bool PlaySample(int[,] voices)
    {
        if(noteCounter < voices.GetLength(1))
        {
            if(timeCounter > noteSpeed)
            {
                for(int i = 0; i < sourcesAllowed; i++)
                {
                    if(voices[i, noteCounter] != -1)
                    {
                        if(voices[i, noteCounter] == 0)
                        {
                            sources[i].Stop();
                        }
                        else
                        {
                            sources[i].clip = harmonicaClips[voices[i, noteCounter]];
                            sources[i].Play();
                        }
                    }
                    
                }
                noteCounter++;
                timeCounter = 0f;
            }
            else
            {
                timeCounter += Time.deltaTime;
            }
            return true;
        }
        else
        {
            noteCounter = 0;
            for (int i = 0; i < sources.Length; i++)
            {
                sources[i].Stop();
            }
            return false;
        }
    }
}
