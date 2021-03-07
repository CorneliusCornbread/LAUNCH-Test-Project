using UnityEngine;
using UnityEngine.UI;

namespace CameraToolkit
{
	public class FOV : MonoBehaviour
	{
		[SerializeField]
		private Slider mainSlider;
		[SerializeField]
		private int speed;

        public void Start()
        {
			mainSlider.maxValue = 179;
			mainSlider.value = Camera.main.fieldOfView;
		}

        public void SliderMoved()
		{
			Camera.main.fieldOfView = Mathf.MoveTowards(Camera.main.fieldOfView, mainSlider.value, Time.deltaTime * speed);
		}
	}
}