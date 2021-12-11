using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIEffects
{
    public class SliderValueFollower : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        [SerializeField] private UnityEvent<float> onValueChanged;
        [Tooltip("Delay before sending event.")]
        [SerializeField] private float valueDelay = 0.0f;

        [SerializeField] private UnityEvent<float> onMaxValueChanged;

        private void OnEnable()
        {
            slider.onValueChanged.AddListener(ListenValueChanged);
        }

        private void OnDisable()
        {
            slider.onValueChanged.RemoveListener(ListenValueChanged);
        }

        private void ListenValueChanged(float v)
        {
            onMaxValueChanged?.Invoke(slider.maxValue);
            CancelInvoke(nameof(InvokeValueChanged));
            Invoke(nameof(InvokeValueChanged), valueDelay);
        }

        private void InvokeValueChanged()
        {
            onValueChanged?.Invoke(slider.value);
        }
    }
}