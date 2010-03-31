namespace OpenTK.Math.CompactFramework
{
    /***********************************************************
     *
     * These were added to help the porting from .NET to .NET CF
     * - Pekka Heikura
     * 
     * ********************************************************/

    using System;
    using System.Globalization;

    public static class SingleExtensions
    {
        public static bool TryParse(string s, out float result)
        {
            return TryParse(s, NumberStyles.Float, CultureInfo.CurrentCulture, out result);
        }

        public static bool TryParse(string s, System.Globalization.NumberStyles style, IFormatProvider provider, out float result)
        {
            result = 0;

            try
            {
                result = Single.Parse(s, style, provider);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
