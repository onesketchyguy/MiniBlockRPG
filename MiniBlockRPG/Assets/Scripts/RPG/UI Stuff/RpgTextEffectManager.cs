using UnityEngine;

namespace RPG.UIEffects
{
    public class RpgTextEffectManager : MonoBehaviour
    {
        [SerializeField] private GameObject textEffectPrefab = null;

        public void SpawnDamageEffect(float damage)
        {
            var rand = Random.insideUnitSphere * 0.5f;
            var effect = Instantiate(textEffectPrefab, transform.position + rand, Quaternion.identity);
            var displayEffect = effect.GetComponent<RpgTextDisplayEffect>();

            if (displayEffect)
            {
                displayEffect.Initialize(damage);
            }
        }

        public void SpawnXPEffect(float xp)
        {
            var rand = Random.insideUnitSphere * 0.5f;
            var effect = Instantiate(textEffectPrefab, transform.position + rand, Quaternion.identity);
            var displayEffect = effect.GetComponent<RpgTextDisplayEffect>();

            if (displayEffect)
            {
                displayEffect.Initialize(xp, Color.yellow);
            }
        }
    }
}