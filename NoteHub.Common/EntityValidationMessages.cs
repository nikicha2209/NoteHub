namespace NoteHub.Common
{
    public static class EntityValidationMessages
    {

        public static class Note
        {
            public const string NoteTitleRequiredMessage = "Title is required.";
            public const string NoteContentRequiredMessage = "Content is required.";

            public const string NoteTitleLengthMessage = "Title must be between {0} and {1} characters.";
            public const string NoteContentLengthMessage = "Content must be between {0} and {1} characters.";
        }
       
    }
}
