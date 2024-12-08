using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeblePrefabs;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;

    private void Awake() {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        Debug.Log("Prefabs: " + placeblePrefabs.Length);

        foreach (GameObject prebaf in placeblePrefabs) {
            GameObject newPrefab = Instantiate(prebaf, Vector3.zero, prebaf.transform.rotation);
            newPrefab.name = prebaf.name;
            Debug.Log("Prefab name: " + newPrefab.name);
            spawnedPrefabs.Add(prebaf.name, newPrefab);
            newPrefab.SetActive(false);
        }
    }

    private void OnEnable() {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs) {
        foreach (ARTrackedImage trackedImage in eventArgs.added) {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedPrefabs[trackedImage.referenceImage.name].SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage) {
        string name = trackedImage.referenceImage.name;

        GameObject prefab = spawnedPrefabs[name];
        // prefab.transform.SetPositionAndRotation(trackedImage.transform.position, trackedImage.transform.rotation);
        prefab.transform.position = trackedImage.transform.position;
        //prefab.transform.rotation = Quaternion.Euler(0, trackedImage.transform.rotation.eulerAngles.y, 0);

        prefab.SetActive(true);
        Debug.Log("SetActive true " + name);
    }
}
