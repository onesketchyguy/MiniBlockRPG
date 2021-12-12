using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UIEffects
{
    public class TextSetter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textObject = null;

        [SerializeField] private string frontString = "";
        [SerializeField] private string middleString = "";
        [SerializeField] private string finalString = "";

        public void SetFrontString(string value)
        {
            frontString = value;
            UpdateText();
        }

        public void SetFrontString(float value)
        {
            frontString = value.ToString();
            UpdateText();
        }

        public void SetMiddleString(string value)
        {
            middleString = value;
            UpdateText();
        }

        public void SetMiddleString(float value)
        {
            middleString = value.ToString();
            UpdateText();
        }

        public void SetFinalString(string value)
        {
            finalString = value;
            UpdateText();
        }

        public void SetFinalString(float value)
        {
            finalString = value.ToString();
            UpdateText();
        }

        public void UpdateText()
        {
            textObject.text = frontString + middleString + finalString;
        }
    }
}