namespace RPG.StatSystem
{
    public class StatContainer
    {
        public StatContainer(int max)
        {            
            currentValue = maxValue = max;
        }

        public int maxValue { get; private set; }
        public float currentValue { get; private set; }

        public bool isOverflow => currentValue > maxValue;
        public bool isFull => currentValue == maxValue;
        public bool isEmpty => currentValue <= 0;

        public void Empty()
        {
            currentValue = 0;
        }

        public void Fill()
        {
            currentValue = maxValue;
        }

        public void ModifyValue(float mod)
        {
            currentValue += mod;            
        }

        public void ModifyMaxValue(int mod, bool manipulateContainer = false)
        {
            var oldMax = maxValue;
            maxValue = mod;

            if (manipulateContainer == true)
            {
                currentValue += maxValue - oldMax;
            }
        }
    }
}