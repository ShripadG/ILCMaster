using employeeservice.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using employeeservice.Common;

namespace employeeservice.Services
{
    /// <summary>
    /// This class contains methods to connect to db for CRUD operations
    /// </summary>
    public class CloudantService : ICloudantService
    {
        /// <summary>
        /// For credentials of db
        /// </summary>
        private readonly Creds _cloudantCreds;

        /// <summary>
        /// The url encoder
        /// </summary>
        private readonly UrlEncoder _urlEncoder;

        /// <summary>
        /// To connect to http service
        /// </summary>
        private readonly IHttpClientFactory _factory;

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="creds"></param>
        /// <param name="urlEncoder"></param>
        /// <param name="factory"></param>
        public CloudantService(Creds creds, UrlEncoder urlEncoder, IHttpClientFactory factory)
        {
            _cloudantCreds = creds;
            _urlEncoder = urlEncoder;
            _factory = factory;
        }

        /// <summary>
        /// This method creates new db or inserts into it.
        /// </summary>
        /// <param name="item">the item to be inserted</param>
        /// <param name="dbname">the db in which insertion needs to be made</param>
        /// <returns></returns>
        public async Task<dynamic> CreateAsync(dynamic item, string dbname)
        {
            string jsonInString = JsonConvert.SerializeObject(item);

            var _client = _factory.CreateClient("cloudant");

            var response = await _client.PostAsync(_client.BaseAddress + dbname, new StringContent(jsonInString, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                return responseJson;
            }
            else if (Equals(response.ReasonPhrase, "Object Not Found")) //need to create database
            {
                var contents = new StringContent("", Encoding.UTF8, "application/json");
                response = await _client.PutAsync(_client.BaseAddress + dbname, contents); //creating database using PUT request
                if (response.IsSuccessStatusCode) //if successful, try POST request again
                {
                    response = await _client.PostAsync(_client.BaseAddress + dbname, new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsStringAsync();
                        return responseJson;
                    }
                }

            }

            string msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
            Console.WriteLine(msg);
            return JsonConvert.SerializeObject(new { msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase });
        }

        /// <summary>
        /// This method updates the given item/record
        /// </summary>
        /// <param name="item">the item to be updated</param>
        /// <param name="dbname">the db in which update needs to be made</param>
        /// <returns></returns>
        public async Task<dynamic> UpdateAsync(dynamic item, string dbname)
        {
            string jsonInString = JsonConvert.SerializeObject(item);

            var _client = _factory.CreateClient("cloudant");

            var response = await _client.PutAsync(_client.BaseAddress + dbname + "/" + _urlEncoder.Encode(item._id) + "?rev=" + _urlEncoder.Encode(item._rev), new StringContent(jsonInString, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                return responseJson;
            }

            string msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
            Console.WriteLine(msg);
            return JsonConvert.SerializeObject(new { msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase });
        }

        /// <summary>
        /// This method gets all records from given db.
        /// </summary>
        /// <param name="dbname">the db from which select needs to be performed</param>
        /// <returns></returns>
        public async Task<dynamic> GetAllAsync(string dbname)
        {
            var _client = _factory.CreateClient("cloudant");
            var response = await _client.GetAsync(_client.BaseAddress + dbname + "/_all_docs?include_docs=true");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else if (Equals(response.ReasonPhrase, "Object Not Found")) //need to create database
            {
                var contents = new StringContent("", Encoding.UTF8, "application/json");
                response = await _client.PutAsync(_client.BaseAddress + dbname, contents); //creating database using PUT request
                if (response.IsSuccessStatusCode) //if successful, try GET request again
                {
                    response = await _client.GetAsync(_client.BaseAddress + dbname + "/_all_docs?include_docs=true");
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                }

            }

            string msg = "Failure to GET. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
            Console.WriteLine(msg);
            return JsonConvert.SerializeObject(new { msg = msg });
        }

        /// <summary>
        /// This method is used to get by id
        /// </summary>
        /// <param name="id">the id of the record to be fetched</param>
        /// <param name="dbname">the db from which record needs to be fetched.</param>
        /// <returns></returns>
        public async Task<dynamic> GetByIdAsync(string id, string dbname)
        {
            var _client = _factory.CreateClient("cloudant");
            //var response = await _client.GetAsync(_client.BaseAddress + _dbName + "/" + _urlEncoder.Encode(id) + "?rev=" + _urlEncoder.Encode(rev));
            var response = await _client.GetAsync(_client.BaseAddress + dbname + "/" + _urlEncoder.Encode(id));
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            string msg = "Failure to GET. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
            Console.WriteLine(msg);
            return JsonConvert.SerializeObject(new { msg = msg });
        }

        /// <summary>
        /// This method is used to delete the record with given id and rev
        /// </summary>
        /// <param name="id">id of the record to be deleted</param>
        /// <param name="rev">revision number of the record to be fetched. It must be latest.</param>
        /// <param name="dbname">db from which deletion needs to be performed.</param>
        /// <returns></returns>
        public async Task<dynamic> DeleteAsync(string id, string rev, string dbname)
        {
            var _client = _factory.CreateClient("cloudant");
            var response = await _client.DeleteAsync(_client.BaseAddress + dbname + "/" + _urlEncoder.Encode(id) + "?rev=" + _urlEncoder.Encode(rev));
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            string msg = "Failure to DELETE. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
            Console.WriteLine(msg);
            return JsonConvert.SerializeObject(new { msg = msg });
        }
        /// <summary>
        /// This method is used to delete the all the record from table
        /// </summary>
        /// <param name="dbname">db from which deletion needs to be performed.</param>
        /// <returns></returns>
        public async Task<dynamic> DeleteAsync(string dbname)
        {
            var _client = _factory.CreateClient("cloudant");
            var response = await _client.DeleteAsync(_client.BaseAddress + dbname);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            string msg = "Failure to DELETE. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
            Console.WriteLine(msg);
            return JsonConvert.SerializeObject(new { msg = msg });
        }

        /// <summary>
        /// This method is used to perform bulk insertion
        /// </summary>
        /// <returns>returns success/failed response</returns>
        public async Task<dynamic> BulkUpload(string dbname, string jsonfilepath)
        {
            var jsonText = File.ReadAllText(jsonfilepath);
            // add at the start { "docs":
            // add at the end }
            jsonText =  @"{ ""docs"": " +  jsonText + " } ";
            //var jsonInString = JsonConvert.DeserializeObject(jsonText);
            var _client = _factory.CreateClient("cloudant");
            var response = await _client.PostAsync(_client.BaseAddress + dbname + "/_bulk_docs", new StringContent(jsonText, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                return responseJson;
            }
            else if (Equals(response.ReasonPhrase, "Object Not Found")) //need to create database
            {
                var contents = new StringContent("", Encoding.UTF8, "application/json");
                response = await _client.PutAsync(_client.BaseAddress + dbname, contents); //creating database using PUT request
                if (response.IsSuccessStatusCode) //if successful, try POST request again
                {
                    response = await _client.PostAsync(_client.BaseAddress + dbname + "/_bulk_docs", new StringContent(jsonText, Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsStringAsync();
                        return responseJson;
                    }
                }
            }

            string msg = "Failure to bulk upload. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
            Console.WriteLine(msg);
            return JsonConvert.SerializeObject(new { msg = msg });
        }
    }
}
