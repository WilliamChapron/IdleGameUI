using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 2f;         // Vitesse de zoom
    public float minZoom = 5f;           // Zoom minimum
    public float maxZoom = 20f;          // Zoom maximum

    private Camera cam;                  // Référence à la caméra

    public float dragSpeed = 0.5f;  // Vitesse de déplacement de la caméra
    private Vector3 lastMousePosition;  // Dernière position de la souris

    void Start()
    {
        // Obtenir la référence à la caméra principale
        cam = Camera.main;
    }

    void Update()
    {
        // Déplacement de la caméra avec la souris
        MoveCamera();

        // Zoom avec la molette de la souris
        ZoomCamera();
    }

    void MoveCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Enregistrer la position de la souris au moment où l'utilisateur clique
            lastMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            // Calculer le mouvement de la souris pendant que le bouton est maintenu enfoncé
            Vector3 deltaMouse = cam.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition;

            // Déplacer la caméra en fonction du mouvement de la souris
            transform.position -= new Vector3(deltaMouse.x, deltaMouse.y, 0) * dragSpeed;

            // Mettre à jour la dernière position de la souris
            lastMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void ZoomCamera()
    {
        // Récupérer la molette de la souris pour zoomer
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Modifier le champ de vision de la caméra en fonction du scroll
        if (scrollInput != 0f)
        {
            cam.orthographicSize -= scrollInput * zoomSpeed;

            // Limiter les valeurs du zoom
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }
    }
}
