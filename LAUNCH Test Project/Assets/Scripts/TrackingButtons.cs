using UnityEngine;

namespace CameraToolkit
{

	public class TrackingButtons : MonoBehaviour
	{

		public bool isTracking;
		public Interpolator interpolator;
		public Vector3 cameraPosx;

		public void TrackingEnabled(bool isTracking)
        {
			isTracking = true;
			while (isTracking)
            {
				interpolator.targetPosition = Camera.main.transform.localPosition;
            }
		}

		public void TrackingDisabled(bool isTracking)
        {
			isTracking = false;
        }

	}
}