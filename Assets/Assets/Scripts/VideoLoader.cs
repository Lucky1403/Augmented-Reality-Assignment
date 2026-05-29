using System.Collections;
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
        "earthquake_video",
        "earth_video2"
    };

    private int currentVideoIndex = 0;
    private AsyncOperationHandle<VideoClip> currentHandle;
    private bool catalogLoaded = false;
    private bool pendingLoad = false;

    private void Start()
    {
        StartCoroutine(InitializeAddressables());
    }

    private IEnumerator InitializeAddressables()
    {
        var loadHandle = Addressables.LoadContentCatalogAsync(
            "https://augmented--reality-assignment.web.app/catalog_0.1.json"
        );

        yield return loadHandle;

        if (loadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Catalog loaded successfully!");
            catalogLoaded = true;

            if (videoPlayer != null)
            {
                LoadVideo(videoAddresses[currentVideoIndex]);
            }
            else
            {
                pendingLoad = true;
            }
        }
        else
        {
            Debug.LogError("Catalog failed: " + loadHandle.OperationException);
        }
    }

    public void SetVideoPlayer(VideoPlayer player)
    {
        videoPlayer = player;
        Debug.Log("VideoPlayer assigned to VideoLoader!");

        if (catalogLoaded)
        {
            LoadVideo(videoAddresses[currentVideoIndex]);
        }
        else
        {
            Debug.Log("Catalog not ready yet, video will load once catalog finishes.");
        }
    }

    public void LoadVideo(string address)
    {
        if (videoPlayer == null)
        {
            Debug.LogWarning("VideoPlayer is null! Cannot load video.");
            return;
        }

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
                Debug.Log("Video loaded and playing: " + address);
            }
            else
            {
                Debug.LogError("Failed to load video: " + address);
            }
        };
    }

    public void SwitchToVideo(int index)
    {
        if (index >= 0 && index < videoAddresses.Length)
        {
            currentVideoIndex = index;
            LoadVideo(videoAddresses[currentVideoIndex]);
            Debug.Log("Switching to video index: " + index);
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
