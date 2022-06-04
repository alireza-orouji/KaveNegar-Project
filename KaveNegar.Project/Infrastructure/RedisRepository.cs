using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace KaveNegar.Infrastructure
{
    public class RedisRepository
    {
        private RedisEndpoint _redisEndpoint;
        public RedisRepository()
        {
            var host = ConfigurationManager.AppSettings["RedisHost"].ToString();
            var port = Convert.ToInt32(ConfigurationManager.AppSettings["RedisPort"]);
            _redisEndpoint = new RedisEndpoint(host, port);
        }

        public bool IsKeyExists(string key)
        {
            using (var redisClient = new RedisClient(_redisEndpoint))
            {
                if (redisClient.ContainsKey(key))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void SetStrings(string key, string value)
        {
            using (var redisClient = new RedisClient(_redisEndpoint))
            {
                redisClient.SetValue(key, value);
            }
        }

        public string GetStrings(string key)
        {
            using (var redisClient = new RedisClient(_redisEndpoint))
            {
                return redisClient.GetValue(key);
            }
        }
    }
}
