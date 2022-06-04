using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace KaveNegar.Infrastructure
{
    public class RedisRepository
    {
        private readonly RedisEndpoint _redisEndpoint;
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

        public void Set<T>(string key, T value)
        {
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            using (var redisClient = new RedisClient(_redisEndpoint))
            {
                redisClient.SetValue(key, data);
            }
        }

        public T Get<T>(string key)
        {
            using (var redisClient = new RedisClient(_redisEndpoint))
            {
                var data = redisClient.GetValue(key);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
            }

        }

        public bool RemoveStrings(string key)
        {
            using (var redisClient = new RedisClient(_redisEndpoint))
            {
                return redisClient.Remove(key);
            }
        }
    }
}
