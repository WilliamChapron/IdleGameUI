using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimatedGIFController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;

    // Trois listes pour les trois GIFs
    [SerializeField] private List<Texture2D> gifFrames;
    [SerializeField] private List<Texture2D> gif2Frames;

    // Vitesse d'animation pour chaque GIF
    [SerializeField] private float frameRate = 1f;
    [SerializeField] private float frameRateGif2 = 1f;

    private VisualElement imageElement;
    private VisualElement leftGift;

    private int currentImageFrameIndex = 0;
    private int currentLeftGiftFrameIndex = 0;

    private float frameTimeGif1;
    private float frameTimeGif2;

    void Start()
    {
        // Vérification de la validité des listes de frames des GIFs
        if (gifFrames == null || gifFrames.Count == 0 || gif2Frames == null || gif2Frames.Count == 0)
        {
            Debug.LogError("One or more GIF frames are missing!");
            return;
        }

        // Calcul des temps entre chaque frame pour chaque GIF
        frameTimeGif1 = 1f / frameRate;
        frameTimeGif2 = 1f / frameRateGif2;

        var root = uiDocument.rootVisualElement;

        // Récupération des éléments UI
        imageElement = root.Q<VisualElement>("start-menu-container");
        leftGift = root.Q<VisualElement>("left-gif");

        // Lancer les coroutines d'animation pour chaque GIF
        StartCoroutine(AnimateGIF(frameTimeGif1, imageElement, gifFrames, currentImageFrameIndex));
        StartCoroutine(AnimateGIF(frameTimeGif2, leftGift, gif2Frames, currentLeftGiftFrameIndex));
    }

    // Coroutine d'animation de chaque GIF avec des paramètres spécifiques
    private IEnumerator AnimateGIF(float frameTime, VisualElement element, List<Texture2D> gifFrames, int currentFrameIndex)
    {
        while (true)
        {
            // Applique la texture du GIF à l'élément
            element.style.backgroundImage = new StyleBackground(gifFrames[currentFrameIndex]);

            // Passe à la frame suivante
            currentFrameIndex = (currentFrameIndex + 1) % gifFrames.Count;

            // Attend avant de passer à la prochaine frame
            yield return new WaitForSeconds(frameTime);
        }
    }
}
