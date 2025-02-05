namespace NoteHub.Common
{
    public static class EntityValidationConstants
    {
        public static class Note
        {
            public const int NoteTitleMinLength = 3;
            public const int NoteTitleMaxLength = 100;

            public const int NoteContentMinLength = 10;
            public const int NoteContentMaxLength = 4000;
        }
    }
}
