// TapToPlace.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Video;

public class TapToPlace : MonoBehaviour
{
    [SerializeField] private GameObject prefabObject;
    [SerializeField] private VideoLoader videoLoader;
    [SerializeField] private GameObject uiButtons;

    private GameObject spawnedObject;
    private Vector2 touchPosition;
    private ARRaycastManager _arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();

        if (uiButtons != null)
            uiButtons.SetActive(false);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchPosition = touch.position;
            }

            if (_arRaycastManager.Raycast(
                touchPosition,
                hits,
                TrackableType.PlaneWithinBounds |
                TrackableType.PlaneWithinPolygon |
                TrackableType.PlaneEstimated))
            {
                Pose hitPose = hits[0].pose;

                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(
                        prefabObject,
                        hitPose.position,
                        Quaternion.identity
                    );

                    Vector3 wallNormal = hitPose.rotation * Vector3.up;
                    spawnedObject.transform.rotation =
                        Quaternion.LookRotation(-wallNormal, Vector3.up);
                    spawnedObject.transform.position += wallNormal * 0.01f;

                    VideoPlayer vp = spawnedObject.GetComponent<VideoPlayer>();
                    if (vp != null && videoLoader != null)
                    {
                        videoLoader.SetVideoPlayer(vp);
                        Debug.Log("VideoPlayer found on prefab and assigned!");
                    }
                    else
                    {
                        if (vp == null)
                            Debug.LogError("No VideoPlayer component found on prefab!");
                        if (videoLoader == null)
                            Debug.LogError("VideoLoader reference is missing on TapToPlace!");
                    }

                    if (uiButtons != null)
                    {
                        uiButtons.SetActive(true);
                        Debug.Log("UI Buttons shown!");
                    }
                }
            }
        }
    }
}