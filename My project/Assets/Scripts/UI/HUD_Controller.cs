using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

//------------------------------------------------------------------------------
public class HUDController : MonoBehaviour
{
    //--------------------------------------------------------------------------
    private Label _inStockCountLabel;
    private Label _totalCountLabel;
    private Button _clickerButton;

    private GameData _gameData;
    private ClickLogic _clickLogic;

    private UIDocument _UI_Document;

    [SerializeField] private Product _product;

    //--------------------------------------------------------------------------
    void Awake()
    {
        GameObject _gameManager = GameObject.Find("GameManager");
        _gameData = _gameManager.GetComponent<GameData>();
        _clickLogic = _gameManager.GetComponent<ClickLogic>();

        _UI_Document = GetComponent<UIDocument>();
        var root = _UI_Document.rootVisualElement;

        _inStockCountLabel = root.Q<Label>("in-stock-count");
        _totalCountLabel = root.Q<Label>("total-count");
        _clickerButton = root.Q<Button>("clicker-button"); 
    }

    //--------------------------------------------------------------------------
    private void OnEnable()
    {
        _clickerButton.clicked += _clickLogic.OnClick;
        _product.OnProduce += UpdateUICounter;
    }

    //--------------------------------------------------------------------------
    private void OnDisable()
    {
        _product.OnProduce += UpdateUICounter;
        _clickerButton.clicked -= _clickLogic.OnClick;
    }

    //--------------------------------------------------------------------------
    private void UpdateUICounter(int count, int totalCount)
    {
        _inStockCountLabel.text = count.ToString();
        _totalCountLabel.text = totalCount.ToString();
    }
}
