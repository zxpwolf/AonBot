using Microsoft.Bot.Sample.SimpleEchoBot.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Microsoft.Bot.Sample.SimpleEchoBot.Services
{
    [Serializable]
    public class QnAmakerService
    {
        public QnAMakerResult GetMessageFromQnAMaker(string message)
        {
            QnAMakerResult response;
            string responseString = string.Empty;

            var knowledgebaseId = "4e000085-3499-404e-ab0e-ddc3fe49b70f"; // Use knowledge base id created.
            var qnamakerSubscriptionKey = "3f901ef2-7be3-4a54-9b92-d50af27b8155"; //Use subscription key assigned to you.

            //Build the URI
            Uri qnamakerUriBase = new Uri("https://aonqna.azurewebsites.net/qnamaker/knowledgebases/"+ knowledgebaseId + "/generateAnswer");
            //var builder = new UriBuilder($"{qnamakerUriBase}/knowledgebases/{knowledgebaseId}/generateAnswer");

            //Add the question as part of the body
            var postBody = $"{{\"question\": \"{message}\"}}";
            try
            {
                //Send the POST request
                using (WebClient client = new WebClient())
                {
                    //Set the encoding to UTF8
                    client.Encoding = System.Text.Encoding.UTF8;

                    //Add the subscription key header
                    client.Headers.Add("Authorization", "EndpointKey "+qnamakerSubscriptionKey);
                    client.Headers.Add("Content-Type", "application/json");
                    responseString = client.UploadString(qnamakerUriBase, postBody);
                }


                response = JsonConvert.DeserializeObject<QnAMakerResult>(responseString);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to deserialize QnA Maker response string.");
            }

            return response;
        }
    }
}