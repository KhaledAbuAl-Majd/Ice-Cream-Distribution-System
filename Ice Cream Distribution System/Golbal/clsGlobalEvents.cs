namespace MoneyMindManagerGlobal
{
    public static class clsGlobalEvents
    {
        public static event Action<string> OnErrorOccured;

        /// <summary>
        /// Raise OnErrorOccured event
        /// </summary>
        /// <param name="message">the error message</param>
        /// <param name="logIt">log it at logger class</param>
        public static void RaiseErrorEvent(string message)
        {
            //if (logIt)
            //{
            //    Task.Run(() => clsLogger.LogAtEventLog(message));
            //}

            OnErrorOccured?.Invoke(message);
        }
    }
}
