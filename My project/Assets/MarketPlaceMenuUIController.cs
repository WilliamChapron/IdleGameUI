using UnityEngine;
using UnityEngine.UIElements;

using System.Collections;


public class MarketPlaceMenuUIController : MonoBehaviour
{
    [SerializeField] UIDocument m_uiDocument;
    [SerializeField] private Texture2D handCursorTexture; 

    private VisualElement m_category1Container;
    private VisualElement m_category2Container;
    private Button m_category1Button;
    private Button m_category2Button;

    private bool isMarketplaceVisible = false; 

    void Start()
    {
        var rootVisualElement = m_uiDocument.rootVisualElement;

        m_category1Container = rootVisualElement.Q("category1-container");
        m_category2Container = rootVisualElement.Q("category2-container");

        m_category1Button = rootVisualElement.Q<Button>("category1-button");
        m_category2Button = rootVisualElement.Q<Button>("category2-button");


        var factoryPriceButtonsByClass = rootVisualElement.Query<Button>().Name("factory-price-button").ToList();
        foreach (var button in factoryPriceButtonsByClass)
        {
            AddCursorCallbacks(button);
        }

        var closeButton = rootVisualElement.Q<Button>("close-button");
        closeButton.clicked += CloseMenu; 
        AddCursorCallbacks(closeButton);



        m_category1Container.style.display = DisplayStyle.Flex;
        m_category2Container.style.display = DisplayStyle.None;

        m_category1Button.clicked += () => ToggleCategory(1);
        m_category2Button.clicked += () => ToggleCategory(2);

        AddCursorCallbacks(m_category1Button);
        AddCursorCallbacks(m_category2Button);


        // Visible/Hidden logic
        rootVisualElement.AddToClassList("marketplace-hidden");

    }

    void ToggleCategory(int categoryNumber)
    {
        if (categoryNumber == 1)
        {
            m_category1Container.style.display = DisplayStyle.Flex;
            m_category2Container.style.display = DisplayStyle.None;
        }
        else if (categoryNumber == 2)
        {
            m_category1Container.style.display = DisplayStyle.None;
            m_category2Container.style.display = DisplayStyle.Flex;
        }
    }


    // Cursor func
    private void AddCursorCallbacks(Button button)
    {
        button.RegisterCallback<MouseEnterEvent>(evt => ActivateCursor());
        button.RegisterCallback<MouseLeaveEvent>(evt => DeactivateCursor());
    }

    private void ActivateCursor()
    {
        UnityEngine.Cursor.SetCursor(handCursorTexture, Vector2.zero, CursorMode.Auto);
    }

    private void DeactivateCursor()
    {
        UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    // Close menu
    private void CloseMenu()
    {
        //m_uiDocument.enabled = false;
        ToggleMarketplace();
    }


    // Visible/Hidden logic

    public void ToggleMarketplace()
    {
        var root = m_uiDocument.rootVisualElement;
        isMarketplaceVisible = !isMarketplaceVisible;

        if (isMarketplaceVisible)
        {
            Debug.Log("ON");
            root.RemoveFromClassList("marketplace-hidden");
            root.AddToClassList("marketplace-visible");

            Invoke(nameof(EnableGameObject), 0.7f);
        }
        else
        {
            Debug.Log("OFF");
            root.RemoveFromClassList("marketplace-visible");
            root.AddToClassList("marketplace-hidden");

            Invoke(nameof(DisableGameObject), 0.7f);
        }
    }

    public void EnableGameObject()
    {
        gameObject.SetActive(true);
    }

    public void DisableGameObject()
    {
        gameObject.SetActive(false);
    }

}
