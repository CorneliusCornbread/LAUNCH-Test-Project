using UnityEngine;
using UnityEngine.UI;

namespace CameraToolkit
{
	public class DisableFocus : MonoBehaviour
	{

		[SerializeField]
		Toggle toggle;

		[SerializeField]
		Focus focus;

        private void Start()
        {
            focus.enabled = false;
            toggle.onValueChanged.AddListener(delegate
            {
                ToggleValueChanged();
            });
        }
        private void ToggleValueChanged()
        {
            if (toggle.isOn == false)
            {
                focus.enabled = false;
            } else
            {
                focus.enabled = true;
            }
        }
    }
}