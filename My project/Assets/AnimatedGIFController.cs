using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimatedGIFController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private List<Texture2D> gifFrames;
    [SerializeField] private float frameRate = 1f; 

    private VisualElement imageElement;
    private int currentFrameIndex = 0;
    private float frameTime;

    void Start()
    {
        if (gifFrames == null || gifFrames.Count == 0)
        {
            Debug.LogError("No GIF frames assigned!");
            return;
        }


        frameTime = 1f / frameRate;

        var root = uiDocument.rootVisualElement;
        imageElement = root.Q<VisualElement>("start-menu-container");

        StartCoroutine(AnimateGIF());
    }

    private IEnumerator AnimateGIF()
    {
        while (true)
        {
            imageElement.style.backgroundImage = new StyleBackground(gifFrames[currentFrameIndex]);

            currentFrameIndex = (currentFrameIndex + 1) % gifFrames.Count;


            yield return new WaitForSeconds(frameTime);
        }
    }
}
