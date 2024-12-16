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

    [SerializeField] private int _productionResourceCost = 10;
    [SerializeField] private Resource _resource = null;

    [SerializeField] private int _demand = 0;
    [SerializeField] private float _demandPriceRatio = 1.0f;
    [SerializeField] private float _demandSalesRatio = 1.0f;

    private Action<int> _onCountChange;
    private Action<int> _onTotalCountChange;
    private Action<int> _onProductionChange;
    private Action<int, int> _onProduce;
    private Action<int, int, int> _onSell;
    private Action<int> _onPriceChange;
    private Action<int> _onDemandChange;

    //--------------------------------------------------------------------------
    public int TotalCount
    {
        get => _totalCount;
        private set { _totalCount = value; _onTotalCountChange?.Invoke(_totalCount); }
    }

    public int Count
    {
        get => _count;
        private set { _count = value; _onCountChange?.Invoke(_count); }
    }

    public int ProductionPerSecond { 
        get => _productionPerSecond; 
        set => _productionPerSecond = value; 
    }
    public int SellPrice => _sellPrice;
    public Currency Currency => _currency;
    public Resource Resource => _resource;

    //--------------------------------------------------------------------------
    /*
     * @param int: count
     */
    //--------------------------------------------------------------------------
    public event Action<int> OnCountChange
    {
        add { _onCountChange += value; }
        remove { _onCountChange -= value; }
    }

    //--------------------------------------------------------------------------
    /*
     * @param int: totalCount
     */
    //--------------------------------------------------------------------------
    public event Action<int> OnTotalCountChange
    {
        add { _onTotalCountChange += value; }
        remove { _onTotalCountChange -= value; }
    }

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
    /*
     * @param int: newPrice
     */
    //--------------------------------------------------------------------------
    public event Action<int> OnSellPriceChange
    {
        add { _onPriceChange += value; }
        remove { _onPriceChange -= value; }
    }

    //--------------------------------------------------------------------------
    /*
     * @param int: newDemand
     */
    //--------------------------------------------------------------------------
    public event Action<int> OnDemandChange
    {
        add { _onDemandChange += value; }
        remove { _onDemandChange -= value; }
    }

    //--------------------------------------------------------------------------
    public void AdjustPrice(int newPrice)
    {
        if (newPrice < 0) return;
        _sellPrice = newPrice;
        _onPriceChange?.Invoke(_sellPrice);
        _demand = (newPrice == 0) ? Int32.MaxValue - 10 : Mathf.RoundToInt(_demandPriceRatio / _sellPrice);
        _onDemandChange?.Invoke(_demand);
        _salesPerSecond = (newPrice == 0) ? Int32.MaxValue - 10 : Mathf.FloorToInt(_demand / _demandSalesRatio);
    }

    //--------------------------------------------------------------------------
    public void AdjustPriceDemandRatio(float newRatio)
    {
        _demandPriceRatio = newRatio;
        _demand = (_sellPrice == 0) ? Int32.MaxValue : Mathf.RoundToInt(_demandPriceRatio / _sellPrice);
        _onDemandChange?.Invoke(_demand);
        _salesPerSecond = Mathf.FloorToInt(_demand / _demandSalesRatio);
    }

    //--------------------------------------------------------------------------
    public void AdjustSellDemandRatio(float newRatio)
    {
        _demandSalesRatio = newRatio;
        _salesPerSecond = Mathf.FloorToInt(_demand / _demandSalesRatio);
    }

    //--------------------------------------------------------------------------
    public void Sell(int secondsElapsed)
    {
        int salesCount = Mathf.Min(_salesPerSecond * secondsElapsed, Count);
        Count -= salesCount;

        int coinsEarned = salesCount * _sellPrice;
        _currency.Count += coinsEarned;
        _onSell?.Invoke(Count, salesCount, coinsEarned);
    }

    //--------------------------------------------------------------------------
    public void Produce(int amount)
    {
        int resourceCost = amount * _productionResourceCost;
        if (_resource.Count < resourceCost) return;
        Count += amount;
        TotalCount += amount;
        _resource.Count -= resourceCost;
        _onProduce?.Invoke(Count, TotalCount);
    }

    //--------------------------------------------------------------------------
    public void IdleProduce(int secondsElapsed)
    {
        if(_resource.Count <= 0) return;
        int production = _productionPerSecond * secondsElapsed;
        int resourceCost = production * _productionResourceCost;
        if (_resource.Count < resourceCost)
        {
            production = Mathf.FloorToInt(_resource.Count / _productionResourceCost);
            resourceCost = production * _resource.Count;
        }
        Count += production;
        TotalCount += production;
        _resource.Count -= resourceCost;
        _onProduce?.Invoke(Count, TotalCount);
    }
}