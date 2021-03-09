using UnityEngine;
using UnityEngine.UI;

namespace CameraToolkit
{
	public class ChangeY : MonoBehaviour
	{

		[SerializeField]
		private Button YPlus, YMinus;


        private void Start()
        {
			YPlus.onClick.AddListener(OnYPlusPressed);
			YMinus.onClick.AddListener(OnYMinusPressed);
		}

        public void OnYPlusPressed()
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + 1,
				transform.position.z);
		}

		public void OnYMinusPressed()
		{
			transform.position = new Vector3(transform.position.x, transform.position.y - 1,
				transform.position.z);
		}
	}
}