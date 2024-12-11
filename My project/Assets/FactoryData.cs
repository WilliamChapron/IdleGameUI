using UnityEngine;
using System.Collections.Generic;

public class FactoryData : MonoBehaviour
{
    private List<Factory> factories;

    public List<Factory> GetFactories()
    {
        return factories;
    }

    public void AddFactory(Factory factory)
    {
        factories.Add(factory);
    }

    // Le constructeur est maintenant vide
    public FactoryData()
    {
        factories = new List<Factory>();
    }

    // Utiliser Start au lieu du constructeur pour initialiser
    private void Start()
    {
        GenerateRandomFactories(5);  // Générer 5 usines après l'initialisation
    }

    // Générer des usines avec des positions aléatoires
    private void GenerateRandomFactories(int numberOfFactories)
    {
        List<Vector3> positions = new List<Vector3>();

        for (int i = 0; i < numberOfFactories; i++)
        {
            // Générer une position aléatoire
            Vector3 randomPosition = new Vector3(
                Random.Range(-10f, 10f),  // Position X aléatoire entre -10 et 10
                0f,                       // Position Y, ici on reste à 0
                Random.Range(-10f, 10f)   // Position Z aléatoire entre -10 et 10
            );

            // Créer une usine avec une position aléatoire
            Factory newFactory = new Factory(
                "Factory " + (i + 1),      // Nom de l'usine
                100f,                      // Prix d'achat de l'usine
                50f,                       // Prix d'amélioration de l'usine
                5f,                        // Productivité par seconde
                2f,                        // Productivité par clic
                randomPosition             // Position de l'usine
            );

            // Ajouter l'usine à la liste des usines
            AddFactory(newFactory);

            // Ajouter la position de l'usine à la liste
            positions.Add(randomPosition);
        }

        // Appeler SpawnAllFactories avec le tableau de positions
        FactorySpawner factoryPlacer = GetComponent<FactorySpawner>();
        if (factoryPlacer != null)
        {
            factoryPlacer.SpawnAllFactories(positions.ToArray());  // Envoie le tableau de positions
        }
    }
}
