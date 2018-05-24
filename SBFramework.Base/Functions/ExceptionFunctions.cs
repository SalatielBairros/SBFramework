using System;

namespace SBFramework.Base.Functions
{
    public static class ExceptionFunctions
    {
        public static void SupressException(this Action act)
        {
            try
            {
                act();
            }
            catch
            {
                // ignored
            }
        }
    }
}
