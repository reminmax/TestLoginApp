namespace TestLoginApp.Helpers
{
    public class ConstantValues
    {
        public static string BaseUri = "https://api.iq.academy/api";
        public static readonly string LoginUri = "/account/login";
        public static readonly string LogoutUri = "/account/logout";
        public static readonly string ProfileUri = "/account/profile";

        public static string LoginLabelText = "Вход";
        public static string LoginButtonText = "Войти";
        public static string LogoutLabelText = "Выйти";
        public static string EmailPlaceholderText = "Введите email или телефон";
        public static string PasswordPlaceholderText = "Введите пароль";
        public static string DontHaveAnAccountLabelText = "Нет аккаунта?";
        public static string RegisterLabelText = " Зарегистрироваться";
        public static string HelloText = "Привет";

        public static string LoginOrPassIncorrect = "Пользователь с такими данными не найден.";
        public static string NeedActivation = "Необходимо отправить код, отправленный на указанный номер телефона.";
        public static string UserBanned = "Пользовать заблокирован.";
        public static string TockenNotFound = "Передан неверный токен.";

    }
}
