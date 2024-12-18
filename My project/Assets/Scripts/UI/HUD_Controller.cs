using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

//------------------------------------------------------------------------------
public class HUDController : MonoBehaviour
{
    //--------------------------------------------------------------------------
    private Label _inStockCountLabel;
    private Label _totalCountLabel;
    private Label _sellPriceLabel;
    private Label _plotsCountLabel;
    private Label _resourceCountLabel;
    private Label _coinsCountLabel;
    private Label _upgradeLevelLabel;
    private Label _upgradePriceLabel;
    private Label _productionPerSecondLabel;

    private Button _clickerButton;
    private Button _lowerPriceButton;
    private Button _increasePriceButton;
    private Button _buyPlotButton;
    private Button _buyResourceButton;
    private Button _upgradeAllButton;

    private ClickLogic _clickLogic;
    private IdleLogic _idleLogic;

    private UIDocument _UI_Document;

    [SerializeField] private Product _product;

    //--------------------------------------------------------------------------
    void Awake()
    {
        GameObject _gameManager = GameObject.Find("GameManager");
        _clickLogic = _gameManager.GetComponent<ClickLogic>();
        _idleLogic = _gameManager.GetComponent<IdleLogic>();

        _UI_Document = GetComponent<UIDocument>();
        var root = _UI_Document.rootVisualElement;

        _inStockCountLabel = root.Q<Label>("in-stock-count");
        _totalCountLabel = root.Q<Label>("total-count");
        _clickerButton = root.Q<Button>("clicker-button");
        _sellPriceLabel = root.Q<Label>("sell-price");
        _plotsCountLabel = root.Q<Label>("spots-count");
        _resourceCountLabel = root.Q<Label>("resource-count");
        _coinsCountLabel = root.Q<Label>("coins-count");
        _upgradeLevelLabel = root.Q<Label>("upgrade-level");
        _upgradePriceLabel = root.Q<Label>("upgrade-price");
        _productionPerSecondLabel = root.Q<Label>("production-per-second");

        _lowerPriceButton = root.Q<Button>("lower-price-button");
        _increasePriceButton = root.Q<Button>("increase-price-button");
        _buyPlotButton = root.Q<Button>("buy-spot-button");
        _buyResourceButton = root.Q<Button>("buy-resource-button");
        _upgradeAllButton = root.Q<Button>("upgrade-all-button");
    }

    //--------------------------------------------------------------------------
    private void OnEnable()
    {
        _product.OnCountChange += OnProductCountChange;
        _product.OnTotalCountChange += OnTotalProductCountChange;
        _product.OnSellPriceChange += OnSellPriceChange;
        _product.Resource.OnCountChange += OnResourceCountChange;
        _idleLogic.OnPlotsCountChange += OnPlotsCountChange;
        _product.Currency.OnCountChange += OnCoinsCountChange;

        _idleLogic.OnUpgradeAllPlots += OnUpgradeLevelChange;
        _idleLogic.OnUpgradePriceChange += OnUpgradePriceChange;
        _product.OnProductionPerSecondChange += OnProductionPerSecondChange;

        _clickerButton.clicked += _clickLogic.OnClick;
        _lowerPriceButton.clicked += () => AdjustPrice(-1);
        _increasePriceButton.clicked += () => AdjustPrice(1);

        _buyPlotButton.clicked += _idleLogic.BuyPlot;
        _buyResourceButton.clicked += () => _product.Resource.Buy(100);

        _upgradeAllButton.clicked += _idleLogic.UpgradeAll;
    }

    //--------------------------------------------------------------------------
    private void OnDisable()
    {
        _clickerButton.clicked -= _clickLogic.OnClick;
        _product.OnCountChange -= OnProductCountChange;
        _product.OnTotalCountChange -= OnTotalProductCountChange;
        _product.OnSellPriceChange -= OnSellPriceChange;
        _product.Resource.OnCountChange -= OnResourceCountChange;
        _idleLogic.OnPlotsCountChange -= OnPlotsCountChange;
        _product.Currency.OnCountChange -= OnCoinsCountChange;

        _idleLogic.OnUpgradeAllPlots -= OnUpgradeLevelChange;
        _idleLogic.OnUpgradePriceChange -= OnUpgradePriceChange;
        _product.OnProductionPerSecondChange -= OnProductionPerSecondChange;

        _lowerPriceButton.clicked -= () => AdjustPrice(-1);
        _increasePriceButton.clicked -= () => AdjustPrice(1);

        _buyPlotButton.clicked -= _idleLogic.BuyPlot;
        _buyResourceButton.clicked -= () => _product.Resource.Buy(100);

        _upgradeAllButton.clicked -= _idleLogic.UpgradeAll;
    }

    //--------------------------------------------------------------------------
    private void OnProductCountChange(int count)
    {
        _inStockCountLabel.text = count.ToString();
    }

    //--------------------------------------------------------------------------
    private void OnTotalProductCountChange(int totalCount)
    {
        _totalCountLabel.text = totalCount.ToString();
    }

    //--------------------------------------------------------------------------
    private void UpdateProductCounter(int count, int totalCount)
    {
        _inStockCountLabel.text = count.ToString();
        _totalCountLabel.text = totalCount.ToString();
    }

    //--------------------------------------------------------------------------
    private void OnSellPriceChange(int sellPrice)
    {
        _sellPriceLabel.text = sellPrice.ToString();
    }

    //--------------------------------------------------------------------------
    private void OnPlotsCountChange(int plotsCount)
    {
        _plotsCountLabel.text = plotsCount.ToString();
    }

    //--------------------------------------------------------------------------
    private void OnResourceCountChange(int resourceCount)
    {
        _resourceCountLabel.text = resourceCount.ToString();
    }

    //--------------------------------------------------------------------------
    private void OnCoinsCountChange(int coinsCount)
    {
        _coinsCountLabel.text = coinsCount.ToString();
    }

    //--------------------------------------------------------------------------
    private void AdjustPrice(int increment)
    {
        _product.AdjustPrice(_product.SellPrice + increment);
    }

    //--------------------------------------------------------------------------
    private void OnUpgradeLevelChange(int upgradeLevel, int _)
    {
        _upgradeLevelLabel.text = upgradeLevel.ToString();
    }

    //--------------------------------------------------------------------------
    private void OnProductionPerSecondChange(int productionPerSecond)
    {
        _productionPerSecondLabel.text = productionPerSecond.ToString();
    }

    //--------------------------------------------------------------------------
    private void OnUpgradePriceChange(int upgradePrice)
    {
        _upgradePriceLabel.text = upgradePrice.ToString();
    }
}
