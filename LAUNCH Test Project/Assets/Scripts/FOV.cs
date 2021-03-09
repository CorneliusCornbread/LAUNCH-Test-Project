using UnityEngine;
using UnityEngine.UI;

namespace CameraToolkit
{
	public class FOV : MonoBehaviour
	{
		[SerializeField]
		private Camera camera;
		[SerializeField]
		private Slider mainSlider;
		[SerializeField]
		private int speed = 250;

        private void Start()
        {

			mainSlider.onValueChanged.AddListener(delegate { SliderMoved(); });

			mainSlider.maxValue = 179;
			Debug.Log(camera.fieldOfView);
			mainSlider.value = camera.fieldOfView;
		}

        private void SliderMoved()
		{
			Camera.main.fieldOfView = Mathf.MoveTowards(Camera.main.fieldOfView, mainSlider.value, Time.deltaTime * speed);
		}
	}
}