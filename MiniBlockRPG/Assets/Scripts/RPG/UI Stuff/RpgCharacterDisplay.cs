using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.CharacterSystem
{
    public class RpgCharacterDisplay : MonoBehaviour
    {
        [Tooltip("Leave empty to keep healthbar on death")]
        [SerializeField] private GameObject healthbarParent;
        [SerializeField] private GameObject onTakenDamageTextPrefab;

        [SerializeField] private Image classIcon;
        [SerializeField] private  TextMeshProUGUI namePlateText;
        [SerializeField] private  Slider healthSlider;
        [SerializeField] private  Slider xpSlider;
        [SerializeField] private  TextMeshProUGUI currentLevelText;

        [SerializeField] private RpgCharacter characterToDisplay;

        public void UpdateDisplay()
        {
            if (healthSlider != null)
            {
                if (healthSlider.value != characterToDisplay.m_healthContainer.currentValue)
                {
                    if (characterToDisplay.m_healthContainer.currentValue <= 0)
                    {
                        healthbarParent.SetActive(false);
                        return;
                    }

                    if (onTakenDamageTextPrefab != null)
                    {
                        var item = Instantiate(onTakenDamageTextPrefab, healthSlider.transform);
                        item.transform.position = item.transform.position + (Vector3)Random.insideUnitCircle * 0.1f;
                        var textObject = item.GetComponent<TextMeshProUGUI>();
                        textObject.text = $"{healthSlider.value - characterToDisplay.m_healthContainer.currentValue}";
                        Destroy(textObject, 0.5f);
                    }

                    healthSlider.value = characterToDisplay.m_healthContainer.currentValue;
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

        private void Update()
        {
            // FIXME: Use callbacks instead of Update
            UpdateDisplay();
        }
    }
}