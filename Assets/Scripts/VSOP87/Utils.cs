namespace Osim.VSOP87
{
    public static class Utils
    {
        // Astronomical unit in meters
        public static double AstronomicalUnit = 149597870700.0;

        // Converts Julian Ephemeris Date to Julian millenia from the epoch 2000
        // -------------------------------------------------------------------------------------------------------------------
        public static double JDEtoJ2000(double jde)
        {
            return (jde - 2451545.0) / 365250.0;
        }
    }
}
