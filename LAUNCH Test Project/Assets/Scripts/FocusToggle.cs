using UnityEngine;
using UnityEngine.UI;

namespace CameraToolkit
{
	public class FocusToggle : MonoBehaviour
	{
        [Tooltip("The target the camera should focus on.")]
		public Transform target;

        [SerializeField]
        [Tooltip("An optional interpolator component.")]
        private Interpolator optionalInterpolator;

        [SerializeField]
        private Toggle toggle;

        private void Update()
        {
            if (toggle.isOn == false) { return; }

            if (target == null) { return; }

            if (optionalInterpolator == null)
            {
                transform.LookAt(target.position);
            }
            else
            {
                optionalInterpolator.targetRotation = Quaternion.LookRotation(target.position - transform.position);
            }
            }
    }
}