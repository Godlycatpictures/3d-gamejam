using UnityEngine;
using System.Collections;

public class TrophyManager : MonoBehaviour
{
    public static TrophyManager Instance;

    [Header("Trophy Settings")]
    public bool hasTrophy = false;                       // Whether the player owns the trophy
    [SerializeField] private GameObject trophyObject;    // Prefab reference (assign in Inspector!)

    // Spawn position, rotation, and scale
    private Vector3 Place = new Vector3(-4.321f, 2.243f, 1.323f);
    private Quaternion TrophyTime = Quaternion.Euler(-90f, 0f, 35f);
    private Vector3 TrophyScale = new Vector3(0.1f, 0.1f, 0f);

    // Reference to the spawned instance (so we don’t spawn duplicates)
    private GameObject spawnedTrophy;

    private void Awake()
    {
        // ✅ Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Wait briefly before spawning the trophy (if already earned)
        StartCoroutine(SpawnTrophyAfterDelay(0.5f));
    }

    private IEnumerator SpawnTrophyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (hasTrophy)
        {
            SpawnTrophy();
        }
    }

    public void UpdateTrophyState()
    {
        if (hasTrophy)
        {
            SpawnTrophy();
        }
        else if (spawnedTrophy != null)
        {
            Destroy(spawnedTrophy);
        }
    }

    private void SpawnTrophy()
    {
        if (trophyObject == null)
        {
            Debug.LogError("❌ Trophy prefab not assigned in TrophyManager!");
            return;
        }

        // Don’t spawn duplicates
        if (spawnedTrophy != null)
        {
            Debug.Log("Trophy already spawned.");
            return;
        }

        // Scale and spawn
        trophyObject.transform.localScale = TrophyScale;
        spawnedTrophy = Instantiate(trophyObject, Place, TrophyTime);
        spawnedTrophy.transform.localScale = TrophyScale;

        Debug.Log("🏆 Trophy instantiated at " + Place);
    }
}
