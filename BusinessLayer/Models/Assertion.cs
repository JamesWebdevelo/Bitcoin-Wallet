using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models
{
    /// <summary>
    /// Assertions class
    /// </summary>
    public static class Assertion
    {
        #region Methods
        public static void AssertArgumentsLenght(int length, int min, int max)
        {
            if (length < min)
            {
                //Exit($"Not enough arguments are specified, minimum: {min}");
            }
            if (length > max)
            {
                //Exit($"Too many arguments are specified, maximum: {max}");
            }
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="length"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public void AssertArgumentsLenght(int length, int min, int max)
    {
        if (length < min)
        {
            Exit($"Not enough arguments are specified, minimum: {min}");
        }
        if (length > max)
        {
            Exit($"Too many arguments are specified, maximum: {max}");
        }
    }
}
