using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralData : MonoBehaviour
{

    [SerializeField] private FactoryData _factoryData;

    public float currentCoins = 0f;

    public float globalProductivity = 0f;
    public int totalFactories = 0;
    public int factoryTypeCount = 0;

    private void Update()
    {
        totalFactories = _factoryData.FactoryDictionary.Count;
    }

    private void CalculateFactoryCount()
    {

    }

}
