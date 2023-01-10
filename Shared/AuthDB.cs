using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace SignApp.Shared
{
    public abstract class AuthDB : User
    {
        //add user to Harper Database
        public async void addUser(string username, string password)
        {
            HttpRequestMessage requestMessage = generateMessage(new User(username, password));
            HttpResponseMessage responseMessage;
            HttpClient http = new HttpClient();;
            http.DefaultRequestHeaders.Add("Authorization", "YWRtaW46NjQ0YjU0YmJhYmIyN2VlZWU1YmZhNzhkNmIzZDA5MzUzNWVlM2QzZWU1NDQ0MGNmZjhkZjA0OWMyMjAyN2I3MQ==");
            responseMessage = await http.SendAsync(requestMessage);
        }

        public async void addUser(User user){
            HttpRequestMessage requestMessage = generateMessage(user);
            HttpResponseMessage responseMessage;
            HttpClient http = new HttpClient();;
            http.DefaultRequestHeaders.Add("Authorization", "YWRtaW46NjQ0YjU0YmJhYmIyN2VlZWU1YmZhNzhkNmIzZDA5MzUzNWVlM2QzZWU1NDQ0MGNmZjhkZjA0OWMyMjAyN2I3MQ==");
            responseMessage = await http.SendAsync(requestMessage);
        }

        public HttpRequestMessage generateMessage(User user){
            HttpRequestMessage requestMessage;
            HttpClient http;
            var postBody = new //our request body
            {
                operation = "insert",
                schema = "dev",
                table = "users",
                records = new[]
                { new
                    {
                        username = user.username,
                        passwordHash = user.passwordHash,
                        docIds = user.docIds,
                        privateSignature = user.privateSignature,
                        publicSignature = user.publicSignature
                    }
                }
            };
            requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(@"https://userdata-signapp.harperdbcloud.com") //DB URl
            };
            
            requestMessage.Content = System.Net.Http.Json.JsonContent.Create(postBody);
            return requestMessage;
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