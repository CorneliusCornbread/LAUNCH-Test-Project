
using UnityEngine;
using UnityEngine.UI;

namespace CameraToolkit
{
    public class FOV : MonoBehaviour
    {
        [SerializeField]
        private Slider mainSlider;
        [SerializeField]
        private int speed = 250;

        public void Start()
        {
            mainSlider.maxValue = 179;
            mainSlider.value = 55;
            mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }

        
        public void ValueChangeCheck()
        {
            Camera.main.fieldOfView = Mathf.MoveTowards(Camera.main.fieldOfView, mainSlider.value, Time.deltaTime * speed);
        }
    }
}