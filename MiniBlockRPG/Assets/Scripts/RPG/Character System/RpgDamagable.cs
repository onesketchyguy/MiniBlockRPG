using UnityEngine;

namespace RPG.CharacterSystem
{
    public class RpgDamagable : MonoBehaviour
    {       
        public StatSystem.StatContainer m_healthContainer;

        public void InitializeDamagable(int health)
        {
            m_healthContainer = new StatSystem.StatContainer(health);
            m_healthContainer.Fill(); // Not necisary
        }
    }
}