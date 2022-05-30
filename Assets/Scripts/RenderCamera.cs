using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCamera : MonoBehaviour
{
    public GameObject otherPortal;
    public Camera portalCamera;
    public Transform player;

    private MeshRenderer portalMeshRenderer;
    private RenderTexture renderTexture;
    private Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = player.GetComponentInChildren<Camera>();
        playerCamera.enabled = true;

        portalMeshRenderer = GetComponent<MeshRenderer>();
        CreateCameraTexture();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePortalCameraPosition();
        portalCamera.Render();
    }

    public void UpdatePortalCameraPosition()
    {
        // Position sync
        portalCamera.transform.position = new Vector3(
            transform.position.x + otherPortal.transform.position.x - playerCamera.transform.position.x,
            playerCamera.transform.position.y,
            transform.position.z + otherPortal.transform.position.z - playerCamera.transform.position.z
        );

        // Rotation sync
        Vector3 rotDiff = playerCamera.transform.eulerAngles - otherPortal.transform.eulerAngles;
        portalCamera.transform.eulerAngles = new Vector3(
            transform.eulerAngles.x + (rotDiff.x),
            transform.eulerAngles.y - 180f + (rotDiff.y),
            transform.eulerAngles.z + (rotDiff.z)
        );
    }

    public void CreateCameraTexture()
    {
        if (renderTexture == null)
        {
            if (renderTexture != null)
            {
                renderTexture.Release();
            }
            renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
            portalCamera.targetTexture = renderTexture;
            portalMeshRenderer.material.SetTexture("_MainTex", renderTexture);
        }

    }
}
