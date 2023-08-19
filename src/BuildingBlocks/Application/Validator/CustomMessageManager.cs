namespace CXChange.BuildingBlocks.Application.Validator
{
    public class CustomMessageManager : FluentValidation.Resources.LanguageManager
    {
        public CustomMessageManager()
        {
            AddTranslation("en", "NotEmptyValidator", "'{PropertyName}' is required.");
            AddTranslation("en-US", "NotEmptyValidator", "'{PropertyName}' is required.");
            AddTranslation("en-GB", "NotEmptyValidator", "'{PropertyName}' is required.");
            AddTranslation("en", "ExactLengthValidator", "'{PropertyName}' length not valid.");
            AddTranslation("en-US", "ExactLengthValidator", "'{PropertyName}' length not valid.");
            AddTranslation("en-GB", "ExactLengthValidator", "'{PropertyName}' length not valid.");

        }
    }
}
