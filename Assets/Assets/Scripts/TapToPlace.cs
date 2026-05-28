using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlace : MonoBehaviour
{
    [SerializeField] private GameObject prefabObject;
    private GameObject spawnedObject;
    private Vector2 touchPosition;
    private ARRaycastManager _arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log("Touch detected: " + touch.phase);

            if (touch.phase == TouchPhase.Began)
            {
                touchPosition = touch.position;
                Debug.Log("Touch began at: " + touchPosition);
            }

            if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinBounds | TrackableType.PlaneWithinPolygon | TrackableType.PlaneEstimated))
            {
                Debug.Log("Raycast HIT! Hits count: " + hits.Count);
                Pose hitPose = hits[0].pose;

                if (spawnedObject == null)
                {
                    Debug.Log("Spawning object...");
                    spawnedObject = Instantiate(prefabObject, hitPose.position, Quaternion.identity);

                    Vector3 wallNormal = hitPose.rotation * Vector3.up;
                    spawnedObject.transform.rotation = Quaternion.LookRotation(-wallNormal, Vector3.up);
                    spawnedObject.transform.position += wallNormal * 0.01f;
                    Debug.Log("Object spawned!");
                }
                // else
                // {
                //     spawnedObject.transform.position = hitPose.position;
                //     spawnedObject.transform.rotation = hitPose.rotation;
                // }
            }
            else
            {
                Debug.Log("Raycast MISSED");
            }
        }
    }
}