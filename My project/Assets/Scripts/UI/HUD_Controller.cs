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
    private Label _spotsCountLabel;
    private Label _resourceCountLabel;
    private Label _coinsCountLabel;

    private Button _clickerButton;
    private Button _lowerPriceButton;
    private Button _increasePriceButton;
    private Button _buySpotButton;
    private Button _buyResourceButton;

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
        _spotsCountLabel = root.Q<Label>("spots-count");
        _resourceCountLabel = root.Q<Label>("resource-count");
        _coinsCountLabel = root.Q<Label>("coins-count");

        _lowerPriceButton = root.Q<Button>("lower-price-button");
        _increasePriceButton = root.Q<Button>("increase-price-button");
        _buySpotButton = root.Q<Button>("buy-spot-button");
        _buyResourceButton = root.Q<Button>("buy-resource-button");
    }

    //--------------------------------------------------------------------------
    private void OnEnable()
    {
        _clickerButton.clicked += _clickLogic.OnClick;
        _product.OnCountChange += UpdateProductCount;
        _product.OnTotalCountChange += UpdateTotalProductCount;
        _product.OnSellPriceChange += UpdateSellPriceCounter;
        _product.Resource.OnCountChange += UpdateResourceCounter;
        _idleLogic.OnNewSpot += UpdateSpotsCounter;
        _product.Currency.OnCountChange += UpdateCoinsCounter;

        _lowerPriceButton.clicked += () => AdjustPrice(-1);
        _increasePriceButton.clicked += () => AdjustPrice(1);

        _buySpotButton.clicked += _idleLogic.BuySpot;
        _buyResourceButton.clicked += () => _product.Resource.Buy(100);
    }

    //--------------------------------------------------------------------------
    private void OnDisable()
    {
        _clickerButton.clicked -= _clickLogic.OnClick;
        _product.OnCountChange -= UpdateProductCount;
        _product.OnTotalCountChange -= UpdateTotalProductCount;
        _product.OnSellPriceChange -= UpdateSellPriceCounter;
        _product.Resource.OnCountChange -= UpdateResourceCounter;
        _idleLogic.OnNewSpot -= UpdateSpotsCounter;
        _product.Currency.OnCountChange -= UpdateCoinsCounter;

        _lowerPriceButton.clicked -= () => AdjustPrice(-1);
        _increasePriceButton.clicked -= () => AdjustPrice(1);

        _buySpotButton.clicked -= _idleLogic.BuySpot;
        _buyResourceButton.clicked -= () => _product.Resource.Buy(100);
    }

    //--------------------------------------------------------------------------
    private void UpdateProductCount(int count)
    {
        _inStockCountLabel.text = count.ToString();
    }

    //--------------------------------------------------------------------------
    private void UpdateTotalProductCount(int totalCount)
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
    private void UpdateSellPriceCounter(int sellPrice)
    {
        _sellPriceLabel.text = sellPrice.ToString();
    }

    //--------------------------------------------------------------------------
    private void UpdateSpotsCounter(int spotsCount)
    {
        _spotsCountLabel.text = spotsCount.ToString();
    }

    //--------------------------------------------------------------------------
    private void UpdateResourceCounter(int resourceCount)
    {
        _resourceCountLabel.text = resourceCount.ToString();
    }

    //--------------------------------------------------------------------------
    private void UpdateCoinsCounter(int coinsCount)
    {
        _coinsCountLabel.text = coinsCount.ToString();
    }

    //--------------------------------------------------------------------------
    private void AdjustPrice(int increment)
    {
        _product.AdjustPrice(_product.SellPrice + increment);
    }
}
