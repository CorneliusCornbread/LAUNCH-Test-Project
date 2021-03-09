using UnityEngine;
using UnityEngine.UI;

namespace CameraToolkit
{
	public class DisableFocus : MonoBehaviour
	{
		[SerializeField]
		private Focus focus;

        [SerializeField]
        private Toggle toggle;

        public void ToggleValueChanged()
        {
            if (toggle.isOn == false)
            {
                focus.enabled = false;
            }
        }

    }
}