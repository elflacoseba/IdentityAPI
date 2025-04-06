namespace Identity.API.Settings
{
    public class LockoutSettings
    {
        /// <summary>
        /// Duración del bloqueo en minutos.
        /// </summary>
        public double DefaultLockoutTimeSpan { get; set; }

        /// <summary>
        /// Intentos fallidos antes de bloquear.
        /// </summary>
        public int MaxFailedAccessAttempts { get; set; }

        /// <summary>
        /// Si aplica bloqueo a usuarios nuevos.
        /// </summary>
        public bool AllowedForNewUsers { get; set; }
    }
}
