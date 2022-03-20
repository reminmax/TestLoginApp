namespace TestLoginApp.Helpers
{
    public static class ErrorMessageHelper
    {
        public static string GetErrorMessageText(string error)
        {
            switch (error)
            {
                case "LOGIN_OR_PASS_INCORRECT":
                    return ConstantValues.LoginOrPassIncorrect;
                case "NEED_ACTIVATION":
                    return ConstantValues.NeedActivation;
                case "USER_BANNED":
                    return ConstantValues.UserBanned;
                case "TOKEN_NOT_FOUND":
                    return ConstantValues.TockenNotFound;
                default:
                    return "Неизвестная ошибка";
            }
        }

    }
}
