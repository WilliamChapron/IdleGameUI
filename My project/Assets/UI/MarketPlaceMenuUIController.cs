using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class MarketPlaceMenuUIController : MonoBehaviour
{
    [SerializeField] private UIDocument m_uiDocument;
    [SerializeField] private Texture2D handCursorTexture;

    private VisualElement m_category1Container;
    private VisualElement m_category2Container;
    private Button m_category1Button;
    private Button m_category2Button;

    public bool isMarketplaceVisible = false;
    public VisualElement m_root;

    void Start()
    {
        m_root = m_uiDocument.rootVisualElement;

        //m_category1Container = m_root.Q("idle-production-cards-container");
        m_category1Container = m_root.Q("category1-container");
        m_category2Container = m_root.Q("category2-container");

        m_category1Button = m_root.Q<Button>("category1-button");
        m_category2Button = m_root.Q<Button>("category2-button");

        var factoryPriceButtonsByClass = m_root.Query<Button>().Name("factory-price-button").ToList();
        foreach (var button in factoryPriceButtonsByClass)
        {
            AddCursorCallbacks(button);
        }

        var closeButton = m_root.Q<Button>("close-button");
        closeButton.clicked += CloseMenu;
        AddCursorCallbacks(closeButton);

        m_category1Container.style.display = DisplayStyle.Flex;
        m_category2Container.style.display = DisplayStyle.None;

        m_category1Button.clicked += () => ToggleCategory(1);
        m_category2Button.clicked += () => ToggleCategory(2);

        AddCursorCallbacks(m_category1Button);
        AddCursorCallbacks(m_category2Button);

        // Initial visibility state
        m_root.style.opacity = 0; // Set opacity to 1 at the start
        m_root.style.display = DisplayStyle.None;
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
        ToggleMarketplace();
        Debug.LogError("Close menu");
    }

    // Visible/Hidden logic with animation based on opacity
    public void ToggleMarketplace()
    {
        if (m_uiDocument == null)
        {
            Debug.LogError("UIDocument is not initialized in ToggleMarketplace!");
            return;
        }

        isMarketplaceVisible = !isMarketplaceVisible;

        if (isMarketplaceVisible)
        {
            Debug.Log("Marketplace is now visible.");
            m_root.style.opacity = 0;
            m_root.style.display = DisplayStyle.Flex;
            StartCoroutine(FadeIn());
        }
        else
        {
            Debug.Log("Marketplace is now hidden.");
            StartCoroutine(FadeOut());
        }
    }

    // Fade In animation
    private IEnumerator FadeIn()
    {
        float duration = 0.7f; 
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            m_root.style.opacity = Mathf.Lerp(0, 1, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        m_root.style.opacity = 1; 
    }

    // Fade Out animation
    private IEnumerator FadeOut()
    {
        float duration = 0.7f; 
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            m_root.style.opacity = Mathf.Lerp(1, 0, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        m_root.style.opacity = 0; 
        m_root.style.display = DisplayStyle.None; 
    }
}
