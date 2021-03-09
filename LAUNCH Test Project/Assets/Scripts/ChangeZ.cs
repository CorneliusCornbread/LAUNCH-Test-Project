using UnityEngine;
<<<<<<< Updated upstream
using UnityEngine.UI;

=======
using UnityEngine.UI;

>>>>>>> Stashed changes
namespace CameraToolkit
{
	public class ChangeZ : MonoBehaviour
	{

		[SerializeField]
<<<<<<< Updated upstream
		private Button ZPlus, ZMinus;

		private void Start()
=======
		private Button ZPlus, ZMinus;

		private void Start()
		{
			ZPlus.onClick.AddListener(OnZPlusPressed);
			ZMinus.onClick.AddListener(OnZMinusPressed);
		}

		public void OnZPlusPressed()
>>>>>>> Stashed changes
		{
			ZPlus.onClick.AddListener(OnZPlusPressed);
			ZMinus.onClick.AddListener(OnZMinusPressed);
		}

<<<<<<< Updated upstream
		public void OnZPlusPressed()
=======
		public void OnZMinusPressed()
>>>>>>> Stashed changes
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