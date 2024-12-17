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
    private int _upgradeLevel = 0;
    private int _upgradePrice = 10;

    private Action<int> _onNewSpot;
    private Action<int, int> _onUpgradeAllSpots;
    private Action<int> _onUpgradePriceChange;


    public int UpgradePrice
    {
        get => _upgradePrice;
        set { _upgradePrice = value; _onUpgradePriceChange?.Invoke(_upgradePrice); }
    }

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
    /*
     * @param int: upgradeLevel
     */
    //--------------------------------------------------------------------------
    public event Action<int, int> OnUpgradeAllSpots
    {
        add { _onUpgradeAllSpots += value; }
        remove { _onUpgradeAllSpots -= value; }
    }

    //--------------------------------------------------------------------------
    /*
     * @param int: upgradePrice
     */
    public event Action<int> OnUpgradePriceChange
    {
        add { _onUpgradePriceChange += value; }
        remove { _onUpgradePriceChange -= value; }
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
            ComputeSpotPrice();
            _onNewSpot?.Invoke(_spotsCount);
        }
    }

    //--------------------------------------------------------------------------
    public void UpgradeAll()
    {
        if (_product.Currency.Count >= UpgradePrice)
        {
            _product.Currency.Count -= UpgradePrice;
            _upgradeLevel++;
            ComputeUpgradePrice();
            
            /* Increment the production per second of the product */
            int oldSpotProductionPerSecond = _spotProductionPerSecond;
            _spotProductionPerSecond *= 2;
            int _spotProductionPerSecondIncrement = _spotProductionPerSecond - oldSpotProductionPerSecond;
            _product.ProductionPerSecond += _spotsCount * _spotProductionPerSecondIncrement;

            _onUpgradeAllSpots?.Invoke(_upgradeLevel, _spotProductionPerSecondIncrement);
        }
    }

    //--------------------------------------------------------------------------
    private void ComputeSpotPrice()
    {
        _spotPrice = (int)Mathf.Exp(_spotsCount) + 1;
    }

    //--------------------------------------------------------------------------
    private void ComputeUpgradePrice()
    {
        UpgradePrice = (int)Mathf.Exp(_upgradeLevel) + 1;
    }
}