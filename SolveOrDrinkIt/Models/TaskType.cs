using Foolproof;
using Microsoft.Ajax.Utilities;

namespace SolveOrDrinkIt.Models
{
    public enum TaskType
    {
        Drink,
        DrinkAll,
        Task,
        TaskAll
    };

    public static class TaskTypeExtention
    {
        public static string UserPlaceholder = "$user";
        public static string DrinkPlaceholder = "$drink";

        public static ValidationResult Validate(this TaskType type, string text)
        {
            if (text.Contains(DrinkPlaceholder))
            {
                string errorMessage = "";
                bool isValid = true;
                switch (type)
                {
                    case TaskType.Drink:
                    case TaskType.Task:
                        isValid = text.Contains(UserPlaceholder);
                        errorMessage = $"Text must contain the user placeholder: {UserPlaceholder}";
                        break;
                    case TaskType.DrinkAll:
                    case TaskType.TaskAll:
                        isValid = !text.Contains(UserPlaceholder);
                        errorMessage = $"Text mustn't contain the user placeholder: {UserPlaceholder}";
                        break;
                }

                return new ValidationResult()
                {
                    IsValid = isValid,
                    ErrorMessage = errorMessage
                };
            }

            return new ValidationResult()
            {
                IsValid = false,
                ErrorMessage = $"Text must contain the drink placeholder: {DrinkPlaceholder}"
            };
        }

        public static bool IsSingleUser(this TaskType type)
        {
            switch (type)
            {
                case TaskType.Drink:
                case TaskType.Task:
                    return true;
                case TaskType.DrinkAll:
                case TaskType.TaskAll:
                    return false;
                default:
                    return true;
            }
        }

        public static bool IsCompletable(this TaskType type)
        {
            switch (type)
            {
                case TaskType.Drink:
                case TaskType.DrinkAll:
                    return false;
                case TaskType.Task:
                case TaskType.TaskAll:
                    return true;
                default:
                    return true;
            }
        }
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class TaskTypeValidation : ModelAwareValidationAttribute
    {
        static TaskTypeValidation()
        {
            Register.Attribute(typeof(TaskTypeValidation));
        }

        public override bool IsValid(object value, object container)
        {
            if (value != null)
            {
                var text = (string) value;
                var model = (TaskViewModel) container;
                ValidationResult validationResult = model.type.Validate(text);
                ErrorMessage = validationResult.ErrorMessage;
                return validationResult.IsValid;
            }
            return false;
        }
    }
}