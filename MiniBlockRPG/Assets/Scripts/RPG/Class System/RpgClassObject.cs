using UnityEngine;

namespace RPG.ClassSystem
{
    [CreateAssetMenu(menuName = "RPG/Class", fileName = "New Blank Class Object", order = 0)]
    public class RpgClassObject : ScriptableObject
    {
        public string classTitle;
        public string classDesc;
        public Sprite classIcon;

        public int initMaxHealth = 100;
        public int healthIncreasePerLevel = 10;
    }
}