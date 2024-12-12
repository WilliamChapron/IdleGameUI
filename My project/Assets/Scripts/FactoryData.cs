using UnityEngine;
using System.Collections.Generic;

public class FactoryData : MonoBehaviour
{
    private Dictionary<int, Factory> factoryDictionary;
    public Dictionary<int, Factory> FactoryDictionary => factoryDictionary;
    private List<Factory> factories;
    private Factory m_curFactory;
    private int nextFactoryId = 0;

    //
    [SerializeField] GameObject m_factoryClickMenuI;

    public List<Factory> GetFactories()
    {
        return factories;
    }

    public void AddFactory(Factory factory)
    {
        factories.Add(factory);
    }

    private void Start()
    {
        factories = new List<Factory>();
        factoryDictionary = new Dictionary<int, Factory>();
        GenerateRandomFactories(5);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Factory"))
            {
                string factoryName = hit.collider.name;
                int factoryId = int.Parse(factoryName.Split('_')[1]);

                Debug.Log("ID de l'usine cliquée : " + factoryId);

                if (factoryDictionary.ContainsKey(factoryId))
                {
                    Factory clickedFactory = factoryDictionary[factoryId];
                    Debug.Log("Usine cliquée : " + clickedFactory.factoryName);
                    m_curFactory = clickedFactory;


                    FactoryClickUIController uiController = m_factoryClickMenuI.GetComponent<FactoryClickUIController>();
                    uiController.OpenMenu(m_curFactory.position, m_curFactory.factoryName, m_curFactory.productivityPerSecond);
                }
            }
        }
    }

    private void GenerateRandomFactories(int numberOfFactories)
    {
        FactorySpawner factoryPlacer = GetComponent<FactorySpawner>();
        for (int i = 0; i < numberOfFactories; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-10f, 10f),
                0f,
                Random.Range(-10f, 10f)
            );

            string factoryName = "Factory_" + nextFactoryId;

            Factory newFactory = new Factory(
                factoryName,
                100f,
                50f,
                5f,
                2f,
                randomPosition
            );

            AddFactory(newFactory);
            factoryDictionary.Add(nextFactoryId, newFactory);

            Debug.Log("Usine ajoutée : " + factoryName + " avec ID : " + nextFactoryId);

            factoryPlacer.SpawnFactoryAndGround(newFactory.position, factoryName);

            nextFactoryId++;
        }
    }
}
