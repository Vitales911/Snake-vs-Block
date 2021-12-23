using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private List<GameObject> activeTiles = new List<GameObject>();
    public float spawnPos;
    private float tileLengs = 50;
    [SerializeField] private Transform Player;

    private int starttiles = 3;
    void Start()
    {
        for (int i = 0; i < starttiles; i++)
        {
            SpawnTile(Random.Range(0,tilePrefabs.Length));
            
        }
        
    }
    private void Update()
    {
        if (Player.position.z - 100 > spawnPos - (starttiles * tileLengs))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }
    private void SpawnTile(int tileIndex)
    {
        GameObject newTile = Instantiate(tilePrefabs[tileIndex], transform.forward * spawnPos, transform.rotation);
        activeTiles.Add(newTile);
        spawnPos += tileLengs;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
