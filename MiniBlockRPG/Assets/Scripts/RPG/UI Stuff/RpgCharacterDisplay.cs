using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.CharacterSystem
{
    public class RpgCharacterDisplay : MonoBehaviour
    {
        public Image classIcon;
        public TextMeshProUGUI namePlateText;
        public Slider healthSlider;
        public Slider xpSlider;
        public TextMeshProUGUI currentLevelText;

        public RpgCharacter characterToDisplay;

        public void UpdateDisplay()
        {
            healthSlider.value = characterToDisplay.m_healthContainer.currentValue;
            healthSlider.maxValue = characterToDisplay.m_healthContainer.maxValue;

            xpSlider.value = characterToDisplay.m_xpContainer.currentValue;
            xpSlider.maxValue = characterToDisplay.m_xpContainer.maxValue;

            if (currentLevelText.text != characterToDisplay.currentLevel.ToString())
                currentLevelText.text = characterToDisplay.currentLevel.ToString();

            if (classIcon.sprite != characterToDisplay.m_class.classIcon)
                classIcon.sprite = characterToDisplay.m_class.classIcon;

            if (namePlateText.text != characterToDisplay.characterName)
                namePlateText.text = characterToDisplay.characterName;
        }

        private void Update()
        {
            // FIXME: Use callbacks instead of Update
            UpdateDisplay();
        }
    }
}