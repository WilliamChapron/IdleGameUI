using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MarketIdlePlotCardsLinker : MonoBehaviour
{
    [SerializeField] List<IdleLogic> _idleLogics = new List<IdleLogic>();
    List<UIBuyIdleProductionCard> _cards = new List<UIBuyIdleProductionCard>();

    private UIDocument _UI_Document;

    private void Awake()
    {
        _UI_Document = GetComponent<UIDocument>();
        var root = _UI_Document.rootVisualElement;

        var cardsContainer = root.Q<VisualElement>("idle-production-cards-container");

        foreach (var _idleLogic in _idleLogics)
        {
            var card = new UIBuyIdleProductionCard();
            card.Type = _idleLogic.Product.name;
            //card.Image = _idleLogic.Product.image;
            card.PlotPrice = _idleLogic.PlotPrice.ToString();

            cardsContainer.Add(card);
            _cards.Add(card);
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            _idleLogics[i].OnPlotPriceChange += _cards[i].OnPlotPriceChange;

            _cards[i].BuyButton.clicked += _idleLogics[i].BuyPlot;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            _idleLogics[i].OnPlotPriceChange -= _cards[i].OnPlotPriceChange;

            _cards[i].BuyButton.clicked -= _idleLogics[i].BuyPlot;
        }
    }


}
