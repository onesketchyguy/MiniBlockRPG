using UnityEngine;

namespace RPG.CharacterSystem
{
    [RequireComponent(typeof(Collider))]
    public class RpgDamager : MonoBehaviour
    {
        public float damage = 10.0f;

        private RpgDamagable parent;

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
                other.m_healthContainer.ModifyValue(-damage);
                debug += $" for {damage} dmg!";
            }

            Debug.Log(debug);
        }
    }
}