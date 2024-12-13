using UnityEngine;
using UnityEngine.UIElements;

public class MarketPlaceMenuUIController : MonoBehaviour
{
    [SerializeField] UIDocument m_uiDocument;

    private VisualElement m_category1Container;
    private VisualElement m_category2Container;
    private Button m_category1Button;
    private Button m_category2Button;


    void Start()
    {

        var rootVisualElement = m_uiDocument.rootVisualElement;


        m_category1Container = rootVisualElement.Q("category1-container");
        m_category2Container = rootVisualElement.Q("category2-container");

        m_category1Button = rootVisualElement.Q<Button>("category1-button");
        m_category2Button = rootVisualElement.Q<Button>("category2-button");

        m_category1Container.style.display = DisplayStyle.Flex;
        m_category2Container.style.display = DisplayStyle.None;

        m_category1Button.clicked += () => ToggleCategory(1);
        m_category2Button.clicked += () => ToggleCategory(2);
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
}
