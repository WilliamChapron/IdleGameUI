using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 5.0f;
    public float baseZoomSpeed = 200.0f;
    public float minZoom = 5f;
    public float maxZoom = 200f;
    public float moveSpeed = 10f;
    public float targetDistance = 20f;

    private Vector3 lastMousePosition;
    private float currentRotationX = 0f;
    private float currentRotationY = 0f;
    private Transform targetObject;
    private bool isMovingToTarget = false;

    void Update()
    {
        if (GameManager.Instance.isGamePaused) return;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("GreenHouse"))
                {
                    targetObject = hit.transform;
                    isMovingToTarget = true;
                }
            }
        }

        if (isMovingToTarget && targetObject != null)
        {
            MoveCameraToTarget(targetObject.position);
        }

        RotateCamera();
        ZoomCamera();
    }

    void RotateCamera()
    {
        if (Input.GetMouseButton(1))
        {
            float deltaX = Input.GetAxis("Mouse X");
            float deltaY = Input.GetAxis("Mouse Y");

            currentRotationX += deltaX * rotationSpeed;
            currentRotationY -= deltaY * rotationSpeed;

            currentRotationY = Mathf.Clamp(currentRotationY, -130f, 130f);

            transform.rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0f);
        }
    }

    void ZoomCamera()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0f)
        {

            Debug.Log("distance" + transform.position.y);

            float distanceFactor = Mathf.InverseLerp(minZoom, maxZoom, transform.position.y);

            float adjustedZoomSpeed = baseZoomSpeed * distanceFactor;

            Debug.Log(adjustedZoomSpeed);


            transform.position += transform.forward * scrollInput * adjustedZoomSpeed;

            float clampedY = Mathf.Clamp(transform.position.y, minZoom, maxZoom);
            transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
        }
    }

    void MoveCameraToTarget(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - transform.position;
        directionToTarget.Normalize();

        Vector3 targetCameraPosition = targetPosition - directionToTarget * targetDistance;

        transform.position = Vector3.MoveTowards(transform.position, targetCameraPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetCameraPosition) < 50f)
        {
            isMovingToTarget = false;
        }
    }
}
