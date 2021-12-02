using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZavod.Services
{
    public static class Service<T>
    {
        private static T _instance;

        public static void RegisterService(T instance)
        {
            if (_instance != null)
            {
                new Exception("Service already exist");
            }
            _instance = instance;
        }

        public static T GetService()
        {
            if (_instance == null)
            {
                new Exception("Service is null");
            }
            return _instance;
        }
    }
}
