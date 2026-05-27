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
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                touchPosition = touch.position;
            }

            if(_arRaycastManager.Raycast(touchPosition,hits,TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                if(spawnedObject == null)
                {
                    spawnedObject = Instantiate(prefabObject, hitPose.position, Quaternion.identity);

                    // Use the wall's surface normal as the quad's forward direction
                    // hitPose.forward points INTO the wall; we want the quad facing OUT (toward camera)
                    Vector3 wallNormal = hitPose.rotation * Vector3.up; // AR vertical planes use Y as normal

                    // Make the quad face along the wall normal (toward the camera side)
                    spawnedObject.transform.rotation = Quaternion.LookRotation(-wallNormal, Vector3.up);

                    // Offset slightly from the wall so it doesn't z-fight
                    spawnedObject.transform.position += wallNormal * 0.01f;
                }
                // else
                // {
                //     spawnedObject.transform.position = hitPose.position;
                //     spawnedObject.transform.rotation = hitPose.rotation;
                // }
            }
        }
    }
}