using UnityEngine;

public class FactorySpawner : MonoBehaviour
{
    public GameObject factoryPrefab;
    public GameObject groundPrefab;
    public float groundWidth = 4f;
    public float groundHeight = 2f;
    public float tileSize = 1f;
    public float factorySize = 1f;

    public void SpawnFactoryAndGround(Vector3 spawnPosition, string factoryName)
    {
        GameObject factory = Instantiate(factoryPrefab, spawnPosition, Quaternion.identity);
        factory.SetActive(true);
        factory.transform.localScale = new Vector3(factorySize, factorySize, factorySize);
        factory.name = factoryName;

        BoxCollider2D collider = factory.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        factory.tag = "Factory";

        SpawnGroundTiles(spawnPosition);
    }

    void SpawnGroundTiles(Vector3 spawnPosition)
    {
        int tilesInWidth = Mathf.CeilToInt(groundWidth / tileSize);
        int tilesInHeight = Mathf.CeilToInt(groundHeight / tileSize);

        Vector3 startPosition = new Vector3(
            spawnPosition.x - groundWidth / 2 + tileSize / 2,
            spawnPosition.y - groundHeight / 2 + tileSize / 2,
            spawnPosition.z
        );

        for (int i = 0; i < tilesInWidth; i++)
        {
            for (int j = 0; j < tilesInHeight; j++)
            {
                Vector3 tilePosition = new Vector3(startPosition.x + i * tileSize, startPosition.y + j * tileSize - 0.3f, spawnPosition.z);
                GameObject groundTile = Instantiate(groundPrefab, tilePosition, Quaternion.identity);
                groundTile.SetActive(true);
                groundTile.GetComponent<Renderer>().sortingOrder = -1;
            }
        }
    }
}
