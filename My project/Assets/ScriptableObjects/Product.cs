using System;
using UnityEngine;

//------------------------------------------------------------------------------
[CreateAssetMenu(fileName = "Product", menuName = "ScriptableObjects/Product")]
public class Product : ScriptableObject
{
    //--------------------------------------------------------------------------
    [SerializeField] private int _totalCount = 0;
    [SerializeField] private int _count = 0;

    [SerializeField] private int _productionPerSecond = 0;
    [SerializeField] private int _salesPerSecond = 0;

    [SerializeField] private int _sellPrice = 0;
    [SerializeField] private Currency _currency = null;

    [SerializeField] private int _demand = 0;
    [SerializeField] private float _priceDemandRatio = 1.0f;
    [SerializeField] private float _demandSalesRatio = 1.0f;

    private Action<int, int> _onProduce;
    private Action<int, int, int> _onSell;

    //--------------------------------------------------------------------------
    public int TotalCount => _totalCount;
    public int Count => _count;
    public int ProductionPerSecond => _productionPerSecond;
    public int SellPrice => _sellPrice;
    public Currency Currency => _currency;

    //--------------------------------------------------------------------------
    /*
     * @param int: count
     * @param int: totalCount
     */
    //--------------------------------------------------------------------------
    public event Action<int, int> OnProduce
    {
        add { _onProduce += value; }
        remove { _onProduce -= value; }
    }

    //--------------------------------------------------------------------------
    /*
     * @param int: count
     * @param int: salesCount
     * @param int: coinsEarned
     */
    //--------------------------------------------------------------------------
    public event Action<int, int, int> OnSell
    {
        add { _onSell += value; }
        remove { _onSell -= value; }
    }

    //--------------------------------------------------------------------------
    public void AdjustPrice(int newPrice)
    {
        _sellPrice = newPrice;
        _demand = Mathf.RoundToInt(_sellPrice / _priceDemandRatio);
        _salesPerSecond = Mathf.RoundToInt(_demand / _demandSalesRatio);
    }

    //--------------------------------------------------------------------------
    public void AdjustPriceDemandRatio(float newRatio)
    {
        _priceDemandRatio = newRatio;
        _demand = Mathf.RoundToInt(_sellPrice / _priceDemandRatio);
        _salesPerSecond = Mathf.RoundToInt(_demand / _demandSalesRatio);
    }

    //--------------------------------------------------------------------------
    public void AdjustSellDemandRatio(float newRatio)
    {
        _demandSalesRatio = newRatio;
        _salesPerSecond = Mathf.RoundToInt(_demand / _demandSalesRatio);
    }

    //--------------------------------------------------------------------------
    public void Sell(int secondsElapsed)
    {
        int salesCount = Mathf.Min(_salesPerSecond * secondsElapsed, _count);
        _count -= salesCount;

        int coinsEarned = salesCount * _sellPrice;
        _currency.Count += coinsEarned;
        _onSell?.Invoke(_count, salesCount, coinsEarned);
    }

    //--------------------------------------------------------------------------
    public void Produce(int count)
    {
        _count += count;
        _totalCount += count;
        _onProduce?.Invoke(_count, _totalCount);
    }

    //--------------------------------------------------------------------------
    public void IdleProduce(int secondsElapsed)
    {
        int production = _productionPerSecond * secondsElapsed;
        _count += production;
        _totalCount += production;
        _onProduce?.Invoke(_count, _totalCount);
    }
}