using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawn_Sandworm : MonoBehaviour
{
    public GameObject sandWormPrefab;
    public GameObject indicatorPrefab;
    public float spawnInterval = 5.0f; // Time between spawns
    public float indicatorDuration = 1.0f; // Duration of the indicator before the sand worm appears
    public Tilemap surfaceTilemap; // Reference to the surface Tilemap
    public int maxSandWorms = 5; // Maximum number of active sandworms
    public float sandWormLifetime = 10.0f; // Lifetime of each sandworm before it's destroyed

    private List<GameObject> activeSandWorms = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnSandWorm());
    }

    IEnumerator SpawnSandWorm()
    {
        while (true)
        {
            // Clean up any destroyed sandworms from the list
            activeSandWorms.RemoveAll(sandWorm => sandWorm == null);

            if (activeSandWorms.Count < maxSandWorms)
            {
                // Find a random surface tile position
                BoundsInt bounds = surfaceTilemap.cellBounds;
                Vector3Int randomCell;
                TileBase tile;
                do
                {
                    randomCell = new Vector3Int(
                        Random.Range(bounds.xMin, bounds.xMax),
                        Random.Range(bounds.yMin, bounds.yMax),
                        0
                    );
                    tile = surfaceTilemap.GetTile(randomCell);
                } while (tile == null);

                Vector3 spawnPosition = surfaceTilemap.GetCellCenterWorld(randomCell);

                // Instantiate the indicator
                GameObject indicator = Instantiate(indicatorPrefab, spawnPosition, Quaternion.identity);
                Destroy(indicator, indicatorDuration);

                // Wait for the indicator duration
                yield return new WaitForSeconds(indicatorDuration);

                // Instantiate the sand worm at the same position
                GameObject sandWorm = Instantiate(sandWormPrefab, spawnPosition, Quaternion.identity);
                activeSandWorms.Add(sandWorm);

                // Destroy the sand worm after its lifetime expires
                Destroy(sandWorm, sandWormLifetime);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
