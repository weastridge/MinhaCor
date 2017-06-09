using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wve
{
    /// <summary>
    /// conversion methods from CRC Handbook of Chemistry and Physics 1976
    /// </summary>
    public static class WveConvert
    {
        /// <summary>
        /// inches to centimeters 2.54 exactly
        /// </summary>
        /// <param name="inches"></param>
        /// <returns></returns>
        public static double InchesToCm(double inches)
        {
            return inches * 2.5400D;
        }
        /// <summary>
        /// centimeters to inches .39370079
        /// </summary>
        /// <param name="cm"></param>
        /// <returns></returns>
        public static double CmToInches(double cm)
        {
            return cm * .39370079D;
        }

        /// <summary>
        /// meters to feet 3.2808399
        /// </summary>
        /// <param name="meters"></param>
        /// <returns></returns>
        public static double MetersToFeet(double meters)
        {
            return meters * 3.2808399D;
        }

        /// <summary>
        /// /kilograms to pounds 2.2046226
        /// </summary>
        /// <param name="kilograms"></param>
        /// <returns></returns>
        public static double KilogramsToPounds(double kilograms)
        {
            return kilograms * 2.2046226;
        }

        /// <summary>
        /// inches to meters .0254
        /// </summary>
        /// <param name="inches"></param>
        /// <returns></returns>
        public static double InchesToMeters(double inches)
        {
            return inches * .0254;
        }

        /// <summary>
        /// pounds to kilograms, .45359237
        /// </summary>
        /// <param name="pounds"></param>
        /// <returns></returns>
        public static double PoundsToKg(double pounds)
        {
            return pounds * .45359237;
        }

        /// <summary>
        /// temperature (f-32)*5/9
        /// </summary>
        /// <param name="fahrenheit"></param>
        /// <returns></returns>
        public static double FahrenheitToCelsius(double fahrenheit)
        {
            return (fahrenheit - 32) * 5 / 9;
        }

        /// <summary>
        /// temperature c*9/5 + 32
        /// </summary>
        /// <param name="celsius"></param>
        /// <returns></returns>
        public static double CelsiusToFahrenheit(double celsius)
        {
            return (celsius * 9 / 5) + 32;
        }
    }
}
