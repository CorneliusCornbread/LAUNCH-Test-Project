﻿using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CameraToolkit.MultiCamera
{
    /// <summary>
    /// A singleton which manages switching between many cameras.
    /// </summary>
    [DisallowMultipleComponent]
    public class CameraManager : MonoBehaviour
    {
        #region Singleton
        /// <summary>
        /// The currently active singleton instance. Will automatically create a new singleton if one does not exist.
        /// </summary>
        public static CameraManager Instance 
        {
            get 
            {
                //Create an instance of our singleton if it doesn't not already exist
                if (_instance == null)
                {
#if UNITY_EDITOR
                    //To avoid creating another manager if we're quitting.
                    if (EDITOR_isExiting)
                    {
                        return null;
                    }
#endif

                    Debug.LogWarning("No CameraManager singleton found. Creating one from scratch.");

                    GameObject gObj = new GameObject("Camera Manager");
                    _instance = gObj.AddComponent<CameraManager>(); ;
                    
                    return _instance;
                }
                else
                {
                    return _instance;
                }
            }
        }

        private static CameraManager _instance;
        #endregion

        [SerializeField]
        [Tooltip("The index of the serialized camera to set as the active camera on start. Set to -1 to disable.")]
        private int activeStartCameraIndex = -1;

        [SerializeField]
        private List<ManagedCamera> cameras = new List<ManagedCamera>();

        public int ActiveCameraIndex { get; private set; } = -1;

        public int CameraCount => cameras.Count;

        /// <summary>
        /// Gets the current active camera. Returns null if there is no active camera
        /// </summary>
        public ManagedCamera ActiveManagedCamera
        {
            get
            {
                if (ActiveCameraIndex < 0)
                {
                    return null;
                }
                else
                {
                    return cameras[ActiveCameraIndex];
                }
            }
        }

        /// <summary>
        /// Indexes the camera collection within the CameraManager.
        /// </summary>
        /// <param name="i">Index</param>
        /// <returns>managed Cam at that index</returns>
        public ManagedCamera this[int i]
        {
            get { return cameras[i]; }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Debug.LogError("Cannot have more than one instance of CameraManager active at once." +
                    $"Disabling 2nd instance \"{gameObject.name}\"");
                enabled = false;
                return;
            }
        }

        private void Start()
        {
            if (_instance != null && _instance != this) { return; } 

            _instance = this;
            DontDestroyOnLoad(this);

            HashSet<ManagedCamera> hash = new HashSet<ManagedCamera>();

            //Add the cameras to a hash to remove duplicates
            for (int i = 0; i < cameras.Count; i++)
            {
                hash.Add(cameras[i]);
            }

            cameras = hash.ToList();

            //Update the serialized cameras to have the correct index
            for (int i = 0; i < cameras.Count; i++)
            {
                cameras[i].cameraIndex = i;
            }

            if (activeStartCameraIndex > -1)
            {
                cameras[activeStartCameraIndex].SetCameraActive(true);
                ActiveCameraIndex = activeStartCameraIndex;
            }
        }

        /// <summary>
        /// Disables the currently active camera.
        /// </summary>
        public void DisableActiveCamera()
        {
            if (ActiveCameraIndex < 0) { return; }

            cameras[ActiveCameraIndex].SetCameraActive(false);
            ActiveCameraIndex = -1;
        }

        /// <summary>
        /// Changes the active camera to the camera at the given collection index.
        /// </summary>
        /// <param name="cameraIndex">Camera to change to</param>
        public void ChangeActiveCamera(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex > cameras.Count - 1)
            {
                throw new System.ArgumentOutOfRangeException(nameof(cameraIndex));
            }

            if (ActiveCameraIndex > -1)
            {
                cameras[ActiveCameraIndex].SetCameraActive(false);
            }

            cameras[cameraIndex].SetCameraActive(true);
            ActiveCameraIndex = cameraIndex;
        }

        /// <summary>
        /// Changes the active camera to the given camera. Will error out if this camera hasn't been added yet.
        /// </summary>
        /// <param name="cam"></param>
        public void ChangeActiveCamera(ManagedCamera cam)
        {
            if (cam == null)
            {
                throw new System.ArgumentNullException(nameof(cam));
            }

            if (cam.cameraIndex < 0)
            {
                Debug.LogError("Camera passed in ChangeActiveCamera() has not been assigned an valid index." +
                    "Has this item not been added to the CameraManager yet?");
                return;
            }

            ChangeActiveCamera(cam.cameraIndex);
        }

        /// <summary>
        /// Changes the active camera to the next camera in the managed camera collection.
        /// </summary>
        public void ChangeToNextCamera()
        {
            if (cameras.Count == 0)
            {
                Debug.LogWarning("Tried to change to the next camera in managed Cam when there are no cameras setup.");
                return;
            }

            int newInd = ActiveCameraIndex + 1;

            if (newInd > cameras.Count - 1)
            {
                ChangeActiveCamera(0);
            }
            else
            {
                ChangeActiveCamera(newInd);
            }
        }

        /// <summary>
        /// Changes the active camera to the previous camera in the managed camera collection.
        /// </summary>
        public void ChangeToPreviousCamera()
        {
            if (cameras.Count == 0)
            {
                Debug.LogWarning("Tried to change to the previous camera in managed Cam when there are no cameras setup.");
                return;
            }

            int newInd = ActiveCameraIndex + -1;

            if (newInd < 0)
            {
                ChangeActiveCamera(cameras.Count - 1);
            }
            else
            {
                ChangeActiveCamera(newInd);
            }
        }

        /// <summary>
        /// Add a camera to our managed camera collection.
        /// </summary>
        /// <param name="cam"></param>
        public void AddCamera(ManagedCamera cam)
        {
            if (cam == null)
            {
                throw new System.ArgumentNullException(nameof(cam));
            }

            if (cameras.Contains(cam))
            {
                Debug.LogWarning("Tried to add a managed camera to the camera manager when it's already been added.");
                return;
            }

            cameras.Add(cam);
            cam.SetCameraActive(false);
            cam.cameraIndex = cameras.Count - 1;
        }

        /// <summary>
        /// Removes a camera from the camera collection and disables it. Does not destroy the camera.
        /// </summary>
        /// <param name="cameraIndex">Index of camera</param>
        public void RemoveCamera(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex > cameras.Count - 1)
            {
                throw new System.ArgumentOutOfRangeException(nameof(cameraIndex));
            }

            cameras[cameraIndex].SetCameraActive(false);
            cameras[cameraIndex].cameraIndex = -1;
            cameras.RemoveAt(cameraIndex);

            //We have to reset the indexes on the cameras because if we remove anything other than the absolute last index 
            //it'll shift everything.
            for (int i = 0; i < cameras.Count; i++)
            {
                cameras[i].cameraIndex = i;
            }
        }

        /// <summary>
        /// Removes a camera from the camera collection and disables it. Does not destroy the camera.
        /// </summary>
        /// <param name="cam">Camera to remove</param>
        public void RemoveCamera(ManagedCamera cam)
        {
            if (cam == null)
            {
                throw new System.ArgumentNullException(nameof(cam));
            }

            if (cam.cameraIndex < 0)
            {
                Debug.LogError("Camera passed in RemoveCamera() has not been assigned an valid index." +
                    "Has this item not been added to the CameraManager yet?");
                return;
            }

            if (cam != cameras[cam.cameraIndex])
            {
                Debug.LogError("The managed camera does not match it's index in the collection. " +
                    "Has this camera's index been modified?");
                return;
            }

            RemoveCamera(cam.cameraIndex);
        }

        #region Editor Debugging
#if UNITY_EDITOR
        public static bool EDITOR_isExiting = false;

        [InitializeOnLoadMethod]
        static void EditorLoad()
        {
            EDITOR_isExiting = false;
            EditorApplication.playModeStateChanged += EditorPlayModeStateChanged;
        }

        private static void EditorPlayModeStateChanged(PlayModeStateChange obj)
        {
            if (obj == PlayModeStateChange.ExitingPlayMode)
            {
                EDITOR_isExiting = true;
            }
        }

        [ContextMenu("Change To Next Camera")]
        private void EditorNextCam()
        {
            ChangeToNextCamera();
        }

        [ContextMenu("Change To Previous Camera")]
        private void EditorPreviousCam()
        {
            ChangeToPreviousCamera();
        }

        private void OnValidate()
        {
            if (activeStartCameraIndex > -1 && activeStartCameraIndex > cameras.Count - 1)
            {
                Debug.LogError("Editor: Cannot have a active start camera index on the Camera Manager that is larger " +
                        "than the number of serialized cameras.");
            }
        }
#endif
        #endregion
    }
}