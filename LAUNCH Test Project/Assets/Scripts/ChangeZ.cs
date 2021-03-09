using UnityEngine;
using UnityEngine.UI;

namespace CameraToolkit
{
	public class ChangeZ : MonoBehaviour
	{

		[SerializeField]
		private Button ZPlus, ZMinus;

		private void Start()
		{
			ZPlus.onClick.AddListener(OnZPlusPressed);
			ZMinus.onClick.AddListener(OnZMinusPressed);
		}

		public void OnZPlusPressed()
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
		}

		public void OnZMinusPressed()
		{
			transform.position = new Vector3(transform.position.x, transform.position.y,
				transform.position.z - 1);
		}
	}
}