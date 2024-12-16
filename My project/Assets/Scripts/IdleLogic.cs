using System;
using System.Collections;
using UnityEngine;

//------------------------------------------------------------------------------
public class IdleLogic : MonoBehaviour
{
    //--------------------------------------------------------------------------
    [SerializeField] private Product _product;

    private int _spotsCount = 0;
    private int _spotProductionPerSecond = 1;
    private int _spotPrice = 10;

    private Action<int> _onNewSpot;

    //--------------------------------------------------------------------------
    /*
     * @param int: spotsCount
     */
    //--------------------------------------------------------------------------
    public event Action<int> OnNewSpot
    {
        add { _onNewSpot += value; }
        remove { _onNewSpot -= value; }
    }

    //--------------------------------------------------------------------------
    void Start()
    {
        // Workaround to compute the initial salesPerSecond and demand.
        _product.AdjustPrice(_product.SellPrice);

        StartCoroutine(Produce(1));
    }

    //--------------------------------------------------------------------------
    IEnumerator Produce(int intervalInSeconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalInSeconds);
            _product.IdleProduce(intervalInSeconds);
        }
    }

    //--------------------------------------------------------------------------
    public void BuySpot()
    {
        if (_product.Currency.Count >= _spotPrice)
        {
            _product.Currency.Count -= _spotPrice;
            _spotsCount++;
            _product.ProductionPerSecond += _spotProductionPerSecond;
            _spotPrice = (int)Mathf.Exp(_spotsCount) + 1;
            _onNewSpot?.Invoke(_spotsCount);
        }
    }
}