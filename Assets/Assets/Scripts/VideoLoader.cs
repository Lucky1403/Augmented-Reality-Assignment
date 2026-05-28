using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class VideoLoader : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    [SerializeField]
    private string[] videoAddresses =
    {
        "earthquake_video"
        // We will add second video here later
    };

    private int currentVideoIndex = 0;
    private AsyncOperationHandle<VideoClip> currentHandle;

    private void Start()
    {
        StartCoroutine(InitializeAddressables());
        Debug.Log("VideoLoader START called!");
    }

    private IEnumerator InitializeAddressables()
    {
        // Load the remote catalog from Firebase
        var loadHandle = Addressables.LoadContentCatalogAsync(
            "https://augmented--reality-assignment.web.app/catalog_0.1.json"
        );

        yield return loadHandle;

        if (loadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Catalog loaded successfully from Firebase!");
            LoadVideo(videoAddresses[currentVideoIndex]);
        }
        else
        {
            Debug.LogError("Failed to load catalog from Firebase!");
        }
    }

    public void LoadVideo(string address)
    {
        if (currentHandle.IsValid())
        {
            Addressables.Release(currentHandle);
        }

        currentHandle = Addressables.LoadAssetAsync<VideoClip>(address);

        currentHandle.Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                videoPlayer.clip = handle.Result;
                videoPlayer.Play();
                Debug.Log("Video loaded successfully: " + address);
            }
            else
            {
                Debug.LogError("Failed to load video: " + address);
            }
        };
    }

    public void SwitchVideo()
    {
        currentVideoIndex = (currentVideoIndex + 1) % videoAddresses.Length;
        LoadVideo(videoAddresses[currentVideoIndex]);
    }

    public void SwitchToVideo(int index)
    {
        if (index >= 0 && index < videoAddresses.Length)
        {
            currentVideoIndex = index;
            LoadVideo(videoAddresses[currentVideoIndex]);
            Debug.Log("Switched to video: " + videoAddresses[index]);
        }
    }

    private void OnDestroy()
    {
        if (currentHandle.IsValid())
        {
            Addressables.Release(currentHandle);
        }
    }
}