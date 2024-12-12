using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 2f;         // Vitesse de zoom
    public float minZoom = 5f;           // Zoom minimum
    public float maxZoom = 20f;          // Zoom maximum

    private Camera cam;                  // R�f�rence � la cam�ra

    public float dragSpeed = 0.5f;  // Vitesse de d�placement de la cam�ra
    private Vector3 lastMousePosition;  // Derni�re position de la souris

    void Start()
    {
        // Obtenir la r�f�rence � la cam�ra principale
        cam = Camera.main;
    }

    void Update()
    {
        // D�placement de la cam�ra avec la souris
        MoveCamera();

        // Zoom avec la molette de la souris
        ZoomCamera();
    }

    void MoveCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Enregistrer la position de la souris au moment o� l'utilisateur clique
            lastMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            // Calculer le mouvement de la souris pendant que le bouton est maintenu enfonc�
            Vector3 deltaMouse = cam.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition;

            // D�placer la cam�ra en fonction du mouvement de la souris
            transform.position -= new Vector3(deltaMouse.x, deltaMouse.y, 0) * dragSpeed;

            // Mettre � jour la derni�re position de la souris
            lastMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void ZoomCamera()
    {
        // R�cup�rer la molette de la souris pour zoomer
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Modifier le champ de vision de la cam�ra en fonction du scroll
        if (scrollInput != 0f)
        {
            cam.orthographicSize -= scrollInput * zoomSpeed;

            // Limiter les valeurs du zoom
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }
    }
}
