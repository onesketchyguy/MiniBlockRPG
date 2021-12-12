using UnityEngine;
using RPG.ClassSystem;
using RPG.StatSystem;

namespace RPG.CharacterSystem
{
    public class RpgCharacter : RpgDamagable
    {
        private const float EXP_RANGE = 15.0f;

        public UnityEngine.Events.UnityEvent onInitialized;

        public string characterName = "Gene Riccharacter";
        public RpgClassObject m_class;

        [SerializeField] private int xpReward = 100;
        [SerializeField] private int xpRewardRange = 0;

        public StatContainer m_xpContainer { get; private set; }

        public uint currentLevel = 1;

        public float deadTime = 0.0f;

        public MonoBehaviour[] disableBehavioursOnDeath = null;

        private bool isDead = false;

        public UnityEngine.Events.UnityEvent<float> onRewardXP;

        public void Initialize()
        {
            InitializeDamagable(m_class.initMaxHealth);
            m_xpContainer = new StatContainer(500);
            m_xpContainer.Empty();            

            onInitialized?.Invoke();
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
                deadTime += Time.deltaTime;

                if (isDead) return;

                GetComponent<Animator>().SetTrigger("Die");
                GetComponent<Animator>().SetLayerWeight(1, 0);

                foreach (var item in disableBehavioursOnDeath)
                {
                    item.enabled = false;
                }

                var collisions = Physics.OverlapSphere(transform.position, EXP_RANGE, 1 << LayerMask.NameToLayer("Actor"));

                foreach (var item in collisions)
                {
                    var other = item.GetComponent<RpgCharacter>();
                    if (other != null && other != this)
                        other.RewardXP(xpReward + Random.Range(-xpRewardRange, xpRewardRange));
                }

                isDead = true;
            }
            else
            {
                deadTime = 0.0f;
            }
        }

        public void RewardXP(int xp)
        {
            m_xpContainer.ModifyValue(xp);
            onRewardXP?.Invoke(xp);
        }
    }
}