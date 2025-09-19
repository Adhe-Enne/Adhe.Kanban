using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Contracts.Constants
{
    public class Api
    {
        public const string BASE = "api";
        public const string VERSION = "v1";
        public const string PREFIX = BASE + "/" + VERSION;
        public const string AUTH = PREFIX + "/auth";
        public const string USERS = PREFIX + "/users";
        public const string BOARDS = PREFIX + "/boards";
        public const string COLUMNS = PREFIX + "/columns";
        public const string CARDS = PREFIX + "/cards";
    }
}
