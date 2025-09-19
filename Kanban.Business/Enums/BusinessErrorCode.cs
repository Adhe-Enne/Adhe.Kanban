namespace Kanban.Business.Enums
{
    public enum BusinessErrorCode
    {
        None= 0,
        Internal,
        UserEmailExists,
        UserNotFound,
        InvalidPassword,
        Unauthorized,
        UserAlreadyExists,
        InvalidRole,
        EntityNotFound,
        BoardNotFound,
        ColumnNotFound,
        TaskNotFound,
        ValidationError,
        Forbidden,
        Conflict,
        InternalError
        // Agrega aquí más códigos según tus necesidades
    }
}
