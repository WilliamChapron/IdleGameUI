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
        GenerateRandomFactories(5);  // G�n�rer 5 usines apr�s l'initialisation
    }

    // G�n�rer des usines avec des positions al�atoires
    private void GenerateRandomFactories(int numberOfFactories)
    {
        List<Vector3> positions = new List<Vector3>();

        for (int i = 0; i < numberOfFactories; i++)
        {
            // G�n�rer une position al�atoire
            Vector3 randomPosition = new Vector3(
                Random.Range(-10f, 10f),  // Position X al�atoire entre -10 et 10
                0f,                       // Position Y, ici on reste � 0
                Random.Range(-10f, 10f)   // Position Z al�atoire entre -10 et 10
            );

            // Cr�er une usine avec une position al�atoire
            Factory newFactory = new Factory(
                "Factory " + (i + 1),      // Nom de l'usine
                100f,                      // Prix d'achat de l'usine
                50f,                       // Prix d'am�lioration de l'usine
                5f,                        // Productivit� par seconde
                2f,                        // Productivit� par clic
                randomPosition             // Position de l'usine
            );

            // Ajouter l'usine � la liste des usines
            AddFactory(newFactory);

            // Ajouter la position de l'usine � la liste
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
