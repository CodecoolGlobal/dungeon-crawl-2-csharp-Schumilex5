using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource Skeltons;
    public AudioSource Slash;
    public AudioSource Walk;
    public AudioSource Woosh;
    public AudioSource Door;
    public AudioSource Punch;
    public AudioSource Collect;
    public AudioSource Eat;

    public static Dictionary<Songs, AudioSource> Playlist = new Dictionary<Songs, AudioSource>();


    // Start is called before the first frame update
    void Start()
    {
        Walk.volume = 0.1f;
        Skeltons.volume = 0.2f;

        if(!Playlist.ContainsKey(Songs.Skeltons)) Playlist.Add(Songs.Skeltons, Skeltons);
        if (!Playlist.ContainsKey(Songs.Walk)) Playlist.Add(Songs.Walk, Walk);
        if (!Playlist.ContainsKey(Songs.Slash)) Playlist.Add(Songs.Slash, Slash);
        if (!Playlist.ContainsKey(Songs.Wooosh)) Playlist.Add(Songs.Wooosh, Woosh);
        if (!Playlist.ContainsKey(Songs.Door)) Playlist.Add(Songs.Door, Door);
        if (!Playlist.ContainsKey(Songs.Punch)) Playlist.Add(Songs.Punch, Punch);
        if (!Playlist.ContainsKey(Songs.Collect)) Playlist.Add(Songs.Collect, Collect);
        if (!Playlist.ContainsKey(Songs.Eat)) Playlist.Add(Songs.Eat, Eat);
    }

    public static void Play(Songs sound)
    {
        Playlist[sound].Play();
    }

    public static void Stop(Songs sound)
    {
        Playlist[sound].Stop();
    }
}

public enum Songs
{
    Skeltons,
    Slash,
    Walk,
    Wooosh,
    Door,
    Punch,
    Collect,
    Eat
}