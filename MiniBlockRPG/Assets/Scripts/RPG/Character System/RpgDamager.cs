using UnityEngine;

namespace RPG.CharacterSystem
{
    [RequireComponent(typeof(Collider))]
    public class RpgDamager : MonoBehaviour
    {
        public float damage = 10.0f;

        private RpgDamagable parent;
        
        private const float MIN_TIME_BETWEEN_ATTACKS = 0.3f;
        private float lastAttack = 0.0f;

        private RpgDamagable lastHit = null;

        private void OnValidate()
        {
            var col = GetComponent<Collider>();

            if (col != null)
            {
                col.isTrigger = true;
            }
        }

        private void Initialize()
        {
            parent = GetComponentInParent<RpgDamagable>();
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (parent == null) Initialize();

            var other = collision.gameObject.GetComponent<RpgDamagable>();

            string debug = $"{gameObject.name} hit {collision.gameObject.name}";

            if (other != null && other != parent)
            {
                if (Time.timeSinceLevelLoad - lastAttack < MIN_TIME_BETWEEN_ATTACKS && other == lastHit) return;
                lastAttack = Time.timeSinceLevelLoad;
                lastHit = other;

                other.ModifyHealth(-damage, collision.ClosestPoint(transform.position));
                debug += $" for {damage} dmg!";
            }

            Debug.Log(debug);
        }
    }
}