namespace Identity.Infrastructure.Settings
{
    public class PasswordSettings
    {
        /// <summary>
        /// Requiere al menos un dígito numérico.
        /// </summary>
        public bool RequireDigit { get; set; }

        /// <summary>
        /// Longitud mínima requerida para la contraseña.
        /// </summary>
        public int RequiredLength { get; set; }

        /// <summary>
        /// Requiere al menos un carácter no alfanumérico
        /// </summary>
        public bool RequireNonAlphanumeric { get; set; }

        /// <summary>
        /// Requiere al menos una letra mayúscula.
        /// </summary>
        public bool RequireUppercase { get; set; }

        /// <summary>
        /// Requiere al menos una letra minúscula.
        /// </summary>
        public bool RequireLowercase { get; set; }

        /// <summary>
        /// Número de caracteres únicos requeridos en la contraseña.
        /// </summary>
        public int RequiredUniqueChars { get; set; }
    }
}
