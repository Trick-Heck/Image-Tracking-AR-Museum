using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class MecanicaImage : MonoBehaviour
{
    public List<GameObject> ObjectsToPlace;
    private Dictionary<string, GameObject> LibraryAndPrefabs;  
    private Dictionary<string, GameObject> SpawnedObjects;  
    private ARTrackedImageManager arTrackedImageManager;
    private IReferenceImageLibrary refLibrary;

    public Button clearButton; 

    void Awake()
    {
        arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        SpawnedObjects = new Dictionary<string, GameObject>();

        if (clearButton != null)
        {
            clearButton.onClick.AddListener(ClearAllObjects);
        }
    }
    private void OnEnable()
    {
        arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }
    private void OnDisable()
    {
        arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }
    private void Start()
    {
        refLibrary = arTrackedImageManager.referenceLibrary;
        LoadObjectDictionary();
    }
    void LoadObjectDictionary()
    {
        LibraryAndPrefabs = new Dictionary<string, GameObject>();
        for (int i = 0; i < refLibrary.count; i++)
        {
            LibraryAndPrefabs.Add(refLibrary[i].name, ObjectsToPlace[i]);
        }
    }
    void ActivateTrackedObject(string _imageName, ARTrackedImage trackedImage)
    {
        if (!SpawnedObjects.ContainsKey(_imageName))
        {
            GameObject prefab = LibraryAndPrefabs[_imageName];

            GameObject spawnedObject = Instantiate(prefab);

            Vector3 cameraForward = Camera.main.transform.forward; 
            Vector3 positionInFrontOfCamera = trackedImage.transform.position + cameraForward * 0.3f;
            spawnedObject.transform.position = positionInFrontOfCamera;

            spawnedObject.transform.LookAt(Camera.main.transform);
            // Evitar que el objeto rote en su propio eje Z
            spawnedObject.transform.eulerAngles = new Vector3(0, spawnedObject.transform.eulerAngles.y, 0);
            // Guardar el objeto instanciado
            SpawnedObjects[_imageName] = spawnedObject;

            DisplayInfo displayInfo = spawnedObject.GetComponent<DisplayInfo>();
            if (displayInfo != null)
            {
                displayInfo.ShowInfo();
            }
        }
    }
    public void OnImageChanged(ARTrackedImagesChangedEventArgs _args)
    {
        foreach (ARTrackedImage addedImage in _args.added)
        {
            ActivateTrackedObject(addedImage.referenceImage.name, addedImage);
        }
        foreach (ARTrackedImage updatedImage in _args.updated)
        {
            if (updatedImage.trackingState == TrackingState.Tracking)
            {
                ActivateTrackedObject(updatedImage.referenceImage.name, updatedImage);
            }
        }
        // Maneja las imágenes eliminadas
        foreach (ARTrackedImage removedImage in _args.removed)
        {
            string imageName = removedImage.referenceImage.name;
            if (SpawnedObjects.ContainsKey(imageName))
            {
                Destroy(SpawnedObjects[imageName]);
                SpawnedObjects.Remove(imageName);
            }
        }
    }
    public void ClearAllObjects()
    {
        foreach (var obj in SpawnedObjects.Values)
        {
            Destroy(obj);
        }
        SpawnedObjects.Clear();
    }
}
