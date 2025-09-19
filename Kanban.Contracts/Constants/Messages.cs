namespace Kanban.Contracts.Constants
{
    public class Messages
    {
        public const string USER_REGISTRING = "Registrando Usuario: Email {0}";
        public const string USER_REGISTRING_OK = "Usuario Registrado Satisfactoriamente {0}";
        public const string USER_LOGIN = "Login Usuario: Email {0}";
        public const string USER_LOGIN_OK = "Login Usuario Satisfactoriamente!";
        public const string USER_GETALL = "Consultando todos los Usuarios";
        public const string USER_GETALL_OK = "Retornando {0} Usuarios";
        public const string USER_NOFOUND_OK = "No se encontraron Usuarios";
        public const string USER_UPDATEROLE = "Actualizando Rol de Usuario {0}, Nuevo Rol {1}";
        public const string USER_UPDATEROLE_OK = "Rol de Usuario Actualizado Satisfactoriamente";
        public const string USER_DELETING = "Eliminando Usuario: Id {0}";
        public const string USER_DELETING_OK = "Usuario Eliminado Satisfactoriamente: Id {0}";
        public const string USER_ACTIVATING = "Activando Usuario Eliminado Id {0}";
        public const string USER_ACTIVATE_OK = "Usuario Activado Satisfactoriamente: Id {0}";

        public const string BOARD_GETALL = "Consultando Tablero";
        public const string BOARD_GETALL_OK = "Retornando Tableros de Usuario {0}";
        public const string BOARD_NOFOUND_OK = "No se encontraron tableros para el Usuario {0}";
        public const string BOARD_CREATING = "Creando Tablero: {0}";
        public const string BOARD_CREATING_OK = "Tablero Creado Satisfactoriamente: {0}";
        public const string BOARD_UPDATING = "Actualizando Tablero: Id {0}";
        public const string BOARD_UPDATING_OK = "Tablero Actualizado Satisfactoriamente: Id {0}";
        public const string BOARD_DELETING = "Eliminando Tablero: Id {0}";

        public const string TASK_GETALL = "Obteniendo Tareas de Tablero {0}";
        public const string TASK_GETALL_OK = "Retornando Tareas de Tablero {0}";
        public const string TASK_GETALL_INACTIVE = "Obteniendo todas las Tareas Inactivas";
        public const string TASK_GETALL_INACTIVE_OK = "Retornando {0} Tareas Inactivas";
        public const string TASK_NOFOUND_OK = "No se encontraron Tareas para el Tablero {0}";
        public const string TASK_CREATE = "Creando Tarea: {0}";
        public const string TASK_CREATE_OK = "Tarea Creada Satisfactoriamente: {0}";
        public const string TASK_UPDATE = "Actualizando Tarea: Id {0}";
        public const string TASK_UPDATE_OK = "Tarea Actualizada Satisfactoriamente: Id {0}";
        public const string TASK_MOVE = "Moviendo Tarea: Id {0} a Columna {1}";
        public const string TASK_MOVE_OK = "Tarea Movida Satisfactoriamente: Id {0} a Columna {1}";
        public const string TASK_DELETE_OK = "Tarea Eliminada Satisfactoriamente: Id {0}";
        public const string TASK_DELETE = "Eliminando Tarea: Id {0}";
        public const string TASK_ACTIVATE = "Activando Tarea Eliminada Id {0}";
        public const string TASK_ACTIVATE_OK = "Tarea Activada Satisfactoriamente: Id {0}";
        public const string TASK_BY_BOARD = "Obteniendo Tareas por Tablero {0}";
        public const string TASK_BY_BOARD_OK = "Retornando Tareas por Tablero {0}";
        public const string TASK_BY_BOARD_NOFOUND_OK = "No se encontraron Tareas para el Tablero {0}";

        public const string TASK_BY_COLUMN = "Obteniendo Tareas por Columna {0}";
        public const string TASK_BY_COLUMN_OK = "Retornando Tareas por Columna {0}";
        public const string TASK_BY_COLUMN_NOFOUNDL_OK = "No se encontraron Tareas para la Columna {0}";
        public const string TASK_BY_USER = "Obteniendo Tareas por Usuario {0}";
        public const string TASK_BY_USER_OK = "Retornando Tareas por Usuario {0}";
        public const string TASK_BY_USER_NOFOUNDL_OK = "No se encontraron Tareas para el Usuario {0}";
        public const string TASK_DELETING = "Eliminando Tarea: Id {0}";
 
        public const string ERROR_USER_REG = "Error al Registrar Usuario: Email {0}";
        public const string ERROR = "Error critico!";

        public const string ERROR_INVALID_USER= "Usuario no valido";
        public const string ERROR_CREATE_USER= "Error al Crear Usuario";
    }
}
