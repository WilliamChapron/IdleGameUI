using System;
using System.Collections;
using UnityEngine;

//------------------------------------------------------------------------------
public class IdleLogic : MonoBehaviour
{
    //--------------------------------------------------------------------------
    [SerializeField] private Product _product;

    private int _plotsCount = 0;
    private int _plotProductionPerSecond = 1;
    private int _plotPrice = 10;
    private int _upgradeLevel = 0;
    private int _upgradePrice = 10;

    private Action<int> _onPlotsCountChange;
    private Action<int, int> _onUpgradeAllPlots;
    private Action<int> _onUpgradePriceChange;
    private Action<int> _onProductionIncrementChange;

    public Product Product => _product;

    public int PlotsCount
    {
        get => _plotsCount;
        set { _plotsCount = value; _onPlotsCountChange?.Invoke(_plotsCount); }
    }

    public int UpgradePrice
    {
        get => _upgradePrice;
        set { _upgradePrice = value; _onUpgradePriceChange?.Invoke(_upgradePrice); }
    }

    public int ProductionIncrement => 0;


    //--------------------------------------------------------------------------
        /*
         * @param int: plotsCount
         */
        //--------------------------------------------------------------------------
    public event Action<int> OnPlotsCountChange
    {
        add { _onPlotsCountChange += value; }
        remove { _onPlotsCountChange -= value; }
    }


    //--------------------------------------------------------------------------
    /*
     * @param int: upgradeLevel
     */
    //--------------------------------------------------------------------------
    public event Action<int, int> OnUpgradeAllPlots
    {
        add { _onUpgradeAllPlots += value; }
        remove { _onUpgradeAllPlots -= value; }
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
    /*
     * @param int: productionIncrement
     */
    //--------------------------------------------------------------------------
    public event Action<int> OnProductionIncrementChange
    {
        add { _onProductionIncrementChange += value; }
        remove { _onProductionIncrementChange -= value; }
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
    public void BuyPlot()
    {
        if (_product.Currency.Count >= _plotPrice)
        {
            _product.Currency.Count -= _plotPrice;
            PlotsCount++;
            _product.ProductionPerSecond += _plotProductionPerSecond;
            ComputePlotPrice();
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
            int oldPlotProductionPerSecond = _plotProductionPerSecond;
            _plotProductionPerSecond *= 2;
            int _plotProductionPerSecondIncrement = _plotProductionPerSecond - oldPlotProductionPerSecond;
            _product.ProductionPerSecond += PlotsCount * _plotProductionPerSecondIncrement;

            _onUpgradeAllPlots?.Invoke(_upgradeLevel, _plotProductionPerSecondIncrement);
        }
    }

    //--------------------------------------------------------------------------
    private void ComputePlotPrice()
    {
        _plotPrice = (int)Mathf.Exp(PlotsCount) + 1;
    }

    //--------------------------------------------------------------------------
    private void ComputeUpgradePrice()
    {
        UpgradePrice = (int)Mathf.Exp(_upgradeLevel) + 1;
    }
}