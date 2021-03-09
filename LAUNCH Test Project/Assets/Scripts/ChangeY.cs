using UnityEngine;
<<<<<<< Updated upstream
using UnityEngine.UI;

=======
using UnityEngine.UI;

>>>>>>> Stashed changes
namespace CameraToolkit
{
	public class ChangeY : MonoBehaviour
	{

		[SerializeField]
<<<<<<< Updated upstream
		private Button YPlus, YMinus;


        private void Start()
        {
			YPlus.onClick.AddListener(OnYPlusPressed);
			YMinus.onClick.AddListener(OnYMinusPressed);
		}

=======
		private Button YPlus, YMinus;

        private void Start()
        {
			YPlus.onClick.AddListener(OnYPlusPressed);
			YMinus.onClick.AddListener(OnYMinusPressed);
		}

>>>>>>> Stashed changes
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