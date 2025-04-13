using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Camera))]
public class MaintainHorizontalFOV : MonoBehaviour
{
    [SerializeField] float referenceHorizontalFOV = 37.7715f; // Match current FOV

    private Camera cam;

    void OnEnable()
    {
        cam = GetComponent<Camera>();
        UpdateFOV();
    }

    void Update()
    {
        UpdateFOV();
    }

    void UpdateFOV()
    {
        if (cam == null) cam = GetComponent<Camera>();

        // get camera info
        float viewportWidth = cam.rect.width * Screen.width;
        float viewportHeight = cam.rect.height * Screen.height;
        float aspect = viewportWidth / viewportHeight;

        // keep horizontal framing
        float hFOVRad = referenceHorizontalFOV * Mathf.Deg2Rad;
        float vFOVRad = 2f * Mathf.Atan(Mathf.Tan(hFOVRad / 2f) / aspect);

        cam.fieldOfView = vFOVRad * Mathf.Rad2Deg;
    }
}
