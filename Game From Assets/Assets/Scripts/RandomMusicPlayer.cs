using UnityEngine;
using System.Collections.Generic;

public class RandomMusicPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> musicClips;

    [SerializeField] private bool loopAll = true;
    [SerializeField] private bool dontDestroyOnLoad = true;

    [SerializeField] private AudioSource audioSource;
    private int lastIndex = -1;
    void Awake()
    {
        audioSource.spatialBlend = 0f;
        if (dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
        if (musicClips.Count > 0)
            PlayRandomTrack();
    }

    void Update()
    {
        if (!audioSource.isPlaying && loopAll)
            PlayRandomTrack();
    }

    void PlayRandomTrack()
    {
        int index;
        do {
            index = Random.Range(0, musicClips.Count);
        } 
        while (index == lastIndex && musicClips.Count > 1);

        lastIndex = index;
        AudioClip clip = musicClips[index];
        audioSource.clip = clip;
        audioSource.Play();
    }
}