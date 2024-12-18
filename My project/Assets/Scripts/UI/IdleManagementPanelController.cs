using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IdleManagementPanelController : MonoBehaviour
{
    [SerializeField] List<IdleLogic> _idleLogics = new List<IdleLogic>();
    List<UIUpgradeIdleProductionCard> _cards = new List<UIUpgradeIdleProductionCard>();

    private UIDocument _UI_Document;

    private void Awake()
    {
        _UI_Document = GetComponent<UIDocument>();
        var root = _UI_Document.rootVisualElement;

        var cardsContainer = root.Q<VisualElement>("idle-production-cards-container");

        foreach (var _idleLogic in _idleLogics)
        {
            var card = new UIUpgradeIdleProductionCard();
            card.Type = _idleLogic.Product.name;
            card.ProductionPerSecond = _idleLogic.Product.ProductionPerSecond.ToString();
            card.ProductionIncrement = _idleLogic.ProductionIncrement.ToString();
            card.PlotsCount = _idleLogic.PlotsCount.ToString();
            card.Price = _idleLogic.UpgradePrice.ToString();

            cardsContainer.Add(card);
            _cards.Add(card);
        }
    }

    private void OnEnable()
    {
        for(int i = 0; i < _cards.Count; i++)
        {
            _idleLogics[i].Product.OnProductionPerSecondChange += _cards[i].OnProductionPerSecondChange;
            _idleLogics[i].OnUpgradePriceChange += _cards[i].OnUpgradePriceChange;
            _idleLogics[i].OnPlotsCountChange += _cards[i].OnPlotsCountChange;
            _idleLogics[i].OnProductionIncrementChange += _cards[i].OnProductionIncrementChange;

            _cards[i].UpgradeButton.clicked += _idleLogics[i].UpgradeAll;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            _idleLogics[i].Product.OnProductionPerSecondChange -= _cards[i].OnProductionPerSecondChange;
            _idleLogics[i].OnUpgradePriceChange -= _cards[i].OnUpgradePriceChange;
            _idleLogics[i].OnPlotsCountChange -= _cards[i].OnPlotsCountChange;
            _idleLogics[i].OnProductionIncrementChange -= _cards[i].OnProductionIncrementChange;

            _cards[i].UpgradeButton.clicked -= _idleLogics[i].UpgradeAll;
        }
    }


}
