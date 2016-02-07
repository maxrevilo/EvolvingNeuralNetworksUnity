using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraControl : BaseBehaviour {

    public float scale = 1f;
    public float scrollSpeed = 1f;


    private float scaleCorrection = 400;
    private Vector3 initialDragPosition;
    private Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        float zoomScale = camera.orthographicSize / scaleCorrection;

        if (Input.GetMouseButton(1))
        {
            Vector2 dragDelta = (Input.mousePosition - initialDragPosition) * scale * zoomScale;
            transform.Translate(-dragDelta.x, -dragDelta.y, 0);
        }

        initialDragPosition = Input.mousePosition;

        float scroll = Input.mouseScrollDelta.y;
        camera.orthographicSize += scroll * scrollSpeed;
    }
}
