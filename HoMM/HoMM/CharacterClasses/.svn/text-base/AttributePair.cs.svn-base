using System;

namespace HoMM.CharacterClasses
{
    /// <summary>
    /// Reprezentuje HitPoints
    /// </summary>
    [Serializable]
    public struct AttributePair
    {
        #region Fields and Properties

        //Fieldy
        int currentValue;
        int maximumValue;
        public int CurrentValue
        {
            get { return currentValue; }
            set { currentValue = value; }
        }
        public int MaximumValue
        {
            get { return maximumValue; }
            set { maximumValue = value; }
        }
        public static AttributePair Zero
        {
            get { return new AttributePair(); }
        }

        #endregion

        #region Constructors

        public AttributePair(ushort maxValue)
        {
            currentValue = maxValue;
            maximumValue = maxValue;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prida zivoty.
        /// Nepresahne nikdy maximalni hodnotu.
        /// </summary>
        /// <param name="value">pocet boduu</param>
        public void Heal(ushort value)
        {
            currentValue += value;
            if (currentValue > maximumValue)
                currentValue = maximumValue;
        }

        /// <summary>
        /// Odebere zivoty.
        /// Nepresahne nikdy do zapornych hodnot.
        /// </summary>
        /// <param name="value">pocet boduu</param>
        public void Damage(ushort value)
        {
            currentValue -= value;
            if (currentValue < 0)
                currentValue = 0;
        }

        /// <summary>
        /// Prenastaveni boduu.
        /// </summary>
        /// <param name="value">pocet boduu</param>
        public void SetCurrent(ushort value)
        {
            currentValue = value;
            if (currentValue > maximumValue)
                currentValue = maximumValue;
        }

        /// <summary>
        /// Prenastaveni maxima boduu. S tim, ze Aktualni HP nebudou vetsi
        /// </summary>
        /// <param name="value"></param>
        public void SetMaximum(ushort value)
        {
            maximumValue = value;
            if (currentValue > maximumValue)
                currentValue = maximumValue;
        }

        /// <summary>
        /// Vylepsi HPcka.
        /// </summary>
        /// <param name="hp">Kolik se prida HP</param>
        /// <param name="max">Kolik se prida MaxHP</param>
        public void Enhance(ushort hp, ushort max)
        {
            currentValue += hp;
            maximumValue += max;
        }

        #endregion
    }
}
