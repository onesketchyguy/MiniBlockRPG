using UnityEngine;
using RPG.ClassSystem;
using RPG.StatSystem;

namespace RPG.CharacterSystem
{
    public class RpgCharacter : RpgDamagable
    {
        public string characterName = "Gene Riccharacter";
        public RpgClassObject m_class;        
        public StatContainer m_xpContainer;

        public uint currentLevel = 1;

        public float deadTime = 0.0f;

        public MonoBehaviour[] disableBehavioursOnDeath = null;

        public void Initialize()
        {
            InitializeDamagable(m_class.initMaxHealth);
            m_xpContainer = new StatContainer(1000);
            m_xpContainer.Empty();
        }

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            // FIXME: Move over to an event system

            if (m_healthContainer.isEmpty)
            {
                GetComponent<Animator>().SetTrigger("Die");
                GetComponent<Animator>().SetLayerWeight(1, 0);
                deadTime += Time.deltaTime;

                foreach (var item in disableBehavioursOnDeath)
                {
                    item.enabled = false;
                }
            }
            else
            {
                deadTime = 0.0f;
            }
        }
    }
}