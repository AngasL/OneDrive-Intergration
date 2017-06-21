using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using OneDriveIntergration.Models;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace OneDriveIntergration.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<AuthenticationResult> GetAuthenticationResult(string code)
        {
            using (var httpClient = new HttpClient())
            {
                var postData = string.Format(
                        Constants.AuthorizationTokenEndpointFormat,
                        Constants.ClientId,
                        Constants.ClientSecret,
                        code,
                        HttpUtility.UrlEncode(Constants.RedirectUri),
                        Constants.GrantType,
                        Constants.Scope);

                var contentData = new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await httpClient.PostAsync(Constants.AuthorizationTokenEndpoing, contentData);
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<AuthenticationResult>(responseContent);
            }
        }

        public async Task<IList<Document>> GetDocuments(string accessToken)
        {
            var result = new List<Document>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    var oneDriveResponse = client.GetAsync(Constants.TargetFolder).Result;
                    if (oneDriveResponse.IsSuccessStatusCode)
                    {
                        var rawContent = await oneDriveResponse.Content.ReadAsStringAsync();
                        var parsedResult = JObject.Parse(rawContent);
                        foreach (var item in parsedResult["value"])
                        {
                            var id = item["id"].ToString();
                            var cid = id.Split('!')[0];
                            var parentId = item["parentReference"]["id"].ToString();
                            var mimeType = item["file"]["mimeType"].ToString();
                            var name = item["name"].ToString();
                            result.Add(new Document
                            {
                                Name = name,
                                ResId = id,
                                CId = cid,
                                ParId = parentId,
                                Extention = GetExtention(mimeType),
                                FileSize = item["size"].ToString(),
                                LastModifiedBy = item["lastModifiedBy"]["user"]["displayName"].ToString(),
                                LastModifedTime = item["lastModifiedDateTime"].ToString(),
                                DownloadUrl = item["@microsoft.graph.downloadUrl"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }

            return result;
        }

        #region Drives
        public async Task<string> GetDefaultDrive(string accessToken)
        {
            return await GetHttpResponse("https://graph.microsoft.com/v1.0/me/drive", accessToken);
        }

        public async Task<string> ListAvailableDrives(string accessToken)
        {
            return await GetHttpResponse("https://graph.microsoft.com/v1.0/me/drives", accessToken);
        }

        public async Task<string> ListSharedDrives(string accessToken)
        {
            return await GetHttpResponse("https://graph.microsoft.com/v1.0/me/drive/shared", accessToken);
        }

        public async Task<string> ListRecentFiles(string accessToken)
        {
            return await GetHttpResponse("https://graph.microsoft.com/v1.0/me/drive/recent", accessToken);
        }

        #endregion

        #region private methods
        private string GetExtention(string mimeType)
        {
            var result = string.Empty;
            switch (mimeType)
            {
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                    result = "Word";
                    break;
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                    result = "Excel";
                    break;
                default:
                    result = "Word";
                    break;
            }

            return result;
        }

        private async Task<string> GetHttpResponse(string targetUrl, string accessToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync(targetUrl);

                return await response.Content.ReadAsStringAsync();
            }
        }
        #endregion
    }
}