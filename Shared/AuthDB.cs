using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace SignApp.Shared
{
    public class AuthDB
    {
        private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
        private static IDatabase db = redis.GetDatabase();
        public static bool CheckUser(string username, string password)
        {   
            if(!db.KeyExists(username))
                return false;
            string hash = HashMaster.Hash256Str(password);
            string storedHash = db.StringGet(username);
            return hash == storedHash;
        }
        public static bool AddUser(string username, string password)
        {      
            if(db.KeyExists(username))
                return false;
            if(username == "" || password == "")
                return false;
            string restrictedChars = "`~!@#$%^&*()_+-=,./<>?;':\"[]{}\\|";
            foreach(char c in restrictedChars)
            {
                if(username.Contains(c))
                    return false;
            }
            string hash = HashMaster.Hash256Str(password);
            return db.StringSet(username, hash);
        }
    }
}