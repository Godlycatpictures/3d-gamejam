using UnityEngine;
using System.Collections;
using NUnit.Framework;

public class TrophyManager : MonoBehaviour
{
    public static TrophyManager Instance;

    [Header("Trophy Settings")]
    public bool hasTrophy = false;                       // Whether the player owns the troph

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}