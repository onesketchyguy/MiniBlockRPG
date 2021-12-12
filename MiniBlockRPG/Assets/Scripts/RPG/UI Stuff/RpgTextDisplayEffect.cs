using System.Collections;
using TMPro;
using UnityEngine;

namespace RPG.UIEffects
{
    public class RpgTextDisplayEffect : MonoBehaviour
    {
        [SerializeField] private Color damageColor = Color.red;
        [SerializeField] private Color healColor = Color.white;

        [SerializeField] private TextMeshProUGUI textComponent = null;
        [SerializeField] private float animateTime = 1.0f;

        [SerializeField] private bool destroyOnFinished = true;

        public void Initialize(float value, Color? color = null)
        {
            if (color != null)
            {
                textComponent.color = (Color)color;
            }
            else
            {
                textComponent.color = (value < 0) ? damageColor : healColor;
            }
            textComponent.text = (value < 0) ? $"{-value}" : $"+{value}";
            StartCoroutine(Animate());
        }

        IEnumerator Animate()
        {
            float time = 0.0f;

            while (time < animateTime)
            {
                yield return null;
                time += Time.deltaTime;

                textComponent.transform.position = textComponent.transform.position + Vector3.up * Time.deltaTime;
                textComponent.alpha = (animateTime - time) / animateTime;
            }

            yield return null;

            if (destroyOnFinished)
            {
                Destroy(gameObject);
            }
        }
    }
}