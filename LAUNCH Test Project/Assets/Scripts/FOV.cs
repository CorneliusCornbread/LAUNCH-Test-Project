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
			mainSlider.value = Camera.main.fieldOfView / 179;

		}

        public void SliderMoved()
		{
			
			Camera.main.fieldOfView = Mathf.MoveTowards(Camera.main.fieldOfView, mainSlider.value*179, Time.deltaTime * speed);

			Debug.Log(mainSlider.value * 179);
		}
	}
}