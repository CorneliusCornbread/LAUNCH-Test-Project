using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CameraToolkit
{
	public class ChangeX : MonoBehaviour
	{

		[SerializeField]
		private Button XPlus, XMinus;


		private void Start()
		{
			XPlus.onClick.AddListener(OnXPlusPressed);
			XMinus.onClick.AddListener(OnXMinusPressed);
		}

		public void OnXPlusPressed()
		{
			transform.position = new Vector3(transform.position.x + 1, transform.position.y,
				transform.position.z);
		}

		public void OnXMinusPressed()
		{
			transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
		}
	}
}