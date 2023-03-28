namespace Monedero.Helpers
{
    public class ScreenHelper
    {
        public static double GetAdjustedSize(double baseFontSize)
        {
            double scaleFactor = 1;

            // Get the width of the screen
            double screenWidth = DeviceDisplay.MainDisplayInfo.Width;

            // Apply scale factor based on screen size
            if (screenWidth >= 768 && screenWidth < 1024) // tablet portrait
            {
                scaleFactor = 2;
            }
            else if (screenWidth >= 1024 && screenWidth < 1280) // tablet landscape
            {
                scaleFactor = 1.5;
            }
            else if (screenWidth >= 1280) // desktop / tv
            {
                scaleFactor = 2.5;
            }

            // Adjust font size based on scale factor
            return baseFontSize * scaleFactor;
        }
    }
}
