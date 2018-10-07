using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandlersServices.Handlers
{
    public class HandlersManager
    {

        #region [ Singleton Pattern ]

        private HandlersManager()
        {
            InitializeHandlersManager();
        }

        private static HandlersManager _instance;

        public static HandlersManager Instance
        {
            get { return _instance ?? (_instance = new HandlersManager()); }
        }

        #endregion

        private void InitializeHandlersManager()
        {
            UserHandler = new UserHandler();
        }

        #region [ Handlers ]

        private UserHandler _userHandler;

        public UserHandler UserHandler
        {
            get { return _userHandler; }
            set { _userHandler = value; }
        }

        #endregion
    }
}
