namespace Identity.Infrastructure.Settings
{
    public class PasswordSettings
    {
        /// <summary>
        /// Requiere al menos un d�gito num�rico.
        /// </summary>
        public bool RequireDigit { get; set; }

        /// <summary>
        /// Longitud m�nima requerida para la contrase�a.
        /// </summary>
        public int RequiredLength { get; set; }

        /// <summary>
        /// Requiere al menos un car�cter no alfanum�rico
        /// </summary>
        public bool RequireNonAlphanumeric { get; set; }

        /// <summary>
        /// Requiere al menos una letra may�scula.
        /// </summary>
        public bool RequireUppercase { get; set; }

        /// <summary>
        /// Requiere al menos una letra min�scula.
        /// </summary>
        public bool RequireLowercase { get; set; }

        /// <summary>
        /// N�mero de caracteres �nicos requeridos en la contrase�a.
        /// </summary>
        public int RequiredUniqueChars { get; set; }
    }
}
