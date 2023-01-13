using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using HarperNetClient.models;

namespace SignApp.Shared
{
    public abstract class AuthDB : User
    {

    }

    public interface IHarperConfiguration
    {
        HarperDbConfiguration GetHarperConfigurations();
    }

    public class HarperConfigurations : IHarperConfiguration
    {
        private IConfiguration _config;
        public HarperConfigurations(IConfiguration configs)
        {
            _config = configs;
        }
        public HarperDbConfiguration GetHarperConfigurations()
        {
            var dbConfigs = _config.GetSection("ConnectionString").Get<HarperDbConfiguration>();
            return dbConfigs;
        }
    }

    public class User
    {
        public string username { get; set; }
        public string passwordHash { get; set; }
        public string docIds { get; set; }
        public string privateSignature { get; set; }
        public string publicSignature { get; set; }

        public User()
        {
            this.username = "";
            this.passwordHash = "";
            this.docIds = "";
            this.privateSignature = "";
            this.publicSignature = "";
        }
        public User(string username, string password)
        {
            this.username = username;
            this.passwordHash = HashMaster.Hash256Str(password);
            this.docIds = "";
            this.privateSignature = "";
            this.publicSignature = "";
        }
    }
}