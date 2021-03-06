using UnityEngine;

namespace RPG.CharacterSystem
{
    public class RpgDamagable : MonoBehaviour
    {
        public StatSystem.StatContainer m_healthContainer { get; protected set; }

        public GameObject hitEffect = null;

        public UnityEngine.Events.UnityEvent<float> onTakenDamage;

        public void InitializeDamagable(int health)
        {
            m_healthContainer = new StatSystem.StatContainer(health);
            m_healthContainer.Fill(); // Not necisary
        }

        public void ModifyHealth(float value, Vector3? pointOfContact = null)
        {
            if (m_healthContainer.isEmpty && value < 0) return;
            m_healthContainer.ModifyValue(value);

            if (value < 0)
            {
                if (pointOfContact != null && hitEffect != null)
                {
                    Instantiate(hitEffect, (Vector3)pointOfContact, Quaternion.identity);
                    onTakenDamage?.Invoke(value);
                }
            }
        }
    }
}