using UnityEngine;

public class FactorySpawner : MonoBehaviour
{
    // Pr�fabriqu�s et param�tres de taille
    public GameObject factoryPrefab;    // Le prefab de l'usine
    public GameObject groundPrefab;     // Le prefab de la terre (tuile)
    public float groundWidth = 4f;      // Largeur de la zone de terre
    public float groundHeight = 2f;     // Hauteur de la zone de terre
    public float tileSize = 1f;         // Taille d'une tuile de terrain
    public float factorySize = 1f;      // Taille d'une usine

    // M�thode pour instancier toutes les usines et leurs terrains
    public void SpawnAllFactories(Vector3[] spawnPositions)
    {
        foreach (Vector3 spawnPosition in spawnPositions)
        {
            SpawnFactoryAndGround(spawnPosition);  // Appel � la fonction qui g�re l'instanciation
        }
    }

    // M�thode pour instancier une usine et ses tuiles de terrain
    void SpawnFactoryAndGround(Vector3 spawnPosition)
    {
        // Instancier l'usine � la position donn�e
        GameObject factory = Instantiate(factoryPrefab, spawnPosition, Quaternion.identity);
        factory.SetActive(true);
        factory.transform.localScale = new Vector3(
            factorySize,
            factorySize,
            factorySize
        );

        // Instancier les tuiles de terrain pour cette usine
        SpawnGroundTiles(spawnPosition);
    }

    // M�thode pour instancier les tuiles de terrain autour de l'usine
    void SpawnGroundTiles(Vector3 spawnPosition)
    {
        int tilesInWidth = Mathf.CeilToInt(groundWidth / tileSize);
        int tilesInHeight = Mathf.CeilToInt(groundHeight / tileSize);

        // Calcul de la position de d�part pour les tuiles
        Vector3 startPosition = new Vector3(
            spawnPosition.x - groundWidth / 2 + tileSize / 2,
            spawnPosition.y - groundHeight / 2 + tileSize / 2,
            spawnPosition.z
        );

        // Cr�ation des tuiles de terrain
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
