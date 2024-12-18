using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class StartMenuUIController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;

    private VisualElement startMenuContainer; 

    private Button playButton;

    private VisualElement smokeEffect;


    [SerializeField] private VideoPlayer videoPlayer;  
    [SerializeField] private RenderTexture renderTexture; 

    void Start()
    {
        var root = uiDocument.rootVisualElement;

        // Initialisation des éléments UI
        startMenuContainer = root.Q<VisualElement>("start-menu-container");
        smokeEffect = root.Q<VisualElement>("smoke-effect");

        playButton = root.Q<Button>("play-button");
        playButton.clicked += OnPlayButtonClicked;
    }

    private void OnPlayButtonClicked()
    {
        startMenuContainer.style.display = DisplayStyle.None;

        smokeEffect.style.display = DisplayStyle.Flex;

        videoPlayer.isLooping = true;
        videoPlayer.targetTexture = renderTexture;
        videoPlayer.Play();

        Invoke(nameof(HideSmokeEffect), 5f);
    }

    private void HideSmokeEffect()
    {
        smokeEffect.style.display = DisplayStyle.None;
    }
}
