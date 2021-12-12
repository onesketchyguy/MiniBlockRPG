using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.CharacterSystem
{
    public class RpgCharacterDisplay : MonoBehaviour
    {
        [Tooltip("Leave empty to keep healthbar on death")]
        [SerializeField] private GameObject healthbarParent;        

        [SerializeField] private Image classIcon;
        [SerializeField] private  TextMeshProUGUI namePlateText;
        [SerializeField] private  Slider healthSlider;
        [SerializeField] private  Slider xpSlider;
        [SerializeField] private  TextMeshProUGUI currentLevelText;

        [SerializeField] private RpgCharacter characterToDisplay;

        private void OnEnable()
        {
            characterToDisplay.onInitialized.AddListener(() => UpdateDisplay());
            characterToDisplay.onRewardXP.AddListener(_ => UpdateDisplay());
            characterToDisplay.onTakenDamage.AddListener(_ => UpdateDisplay());
        }

        private void OnDisable()
        {
            characterToDisplay.onInitialized.RemoveListener(() => UpdateDisplay());
            characterToDisplay.onRewardXP.RemoveListener(_ => UpdateDisplay());
            characterToDisplay.onTakenDamage.RemoveListener(_ => UpdateDisplay());
        }

        public void UpdateDisplay()
        {
            if (healthSlider != null)
            {
                if (healthSlider.value != characterToDisplay.m_healthContainer.currentValue)
                {
                    healthSlider.value = characterToDisplay.m_healthContainer.currentValue;
                    if (healthSlider.value - characterToDisplay.m_healthContainer.currentValue != 0)
                    {
                        Invoke(nameof(UpdateDisplay), 0.25f);
                    }

                    if (characterToDisplay.m_healthContainer.currentValue <= 0 && healthbarParent != null 
                        && characterToDisplay.deadTime > Mathf.Epsilon)
                    {
                        healthbarParent.SetActive(false);
                        return;
                    }
                }

                if (healthSlider.maxValue != characterToDisplay.m_healthContainer.maxValue)
                    healthSlider.maxValue = characterToDisplay.m_healthContainer.maxValue;                
            }

            if (xpSlider != null)
            {
                xpSlider.value = characterToDisplay.m_xpContainer.currentValue;
                xpSlider.maxValue = characterToDisplay.m_xpContainer.maxValue;
            }

            if (currentLevelText != null && currentLevelText.text != characterToDisplay.currentLevel.ToString())
                currentLevelText.text = characterToDisplay.currentLevel.ToString();

            if (classIcon != null && classIcon.sprite != characterToDisplay.m_class.classIcon)
                classIcon.sprite = characterToDisplay.m_class.classIcon;

            if (namePlateText != null && namePlateText.text != characterToDisplay.characterName)
                namePlateText.text = characterToDisplay.characterName;
        }
    }
}