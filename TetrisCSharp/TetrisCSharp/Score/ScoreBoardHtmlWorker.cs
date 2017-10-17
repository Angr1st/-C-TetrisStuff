using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace TetrisCSharp
{
    /// <summary>
    /// Shows The Score of all Games.
    /// </summary>
    class ScoreBoardHtmlWorker
    {
        private const string basePath = "http://csharptetrisscoreboardprovider.azurewebsites.net/";
        private const string apibasePath = "api/tetrisscoreboard";
        private const string getTopTen = "gettopten";
        private const string getAddToList = "/GetAddToList";

        public bool SubmitScore(string name, int score)
        {
            bool result = false;
            WebRequest myWebrequest;
            myWebrequest = WebRequest.Create(apibasePath + getAddToList + '/' + name + '/' + score.ToString());
            Stream objStream;
            objStream = myWebrequest.GetResponse().GetResponseStream();
            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            int i = 0;

            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                    bool.TryParse(sLine, out result);
            }
            objReader.Close();

            return result;
        }

        public List<KeyValuePair<string, int>> GetTopTen()
        {
            List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();

            WebRequest myWebrequest;
            myWebrequest = WebRequest.Create(apibasePath + getTopTen);
            Stream objStream;
            objStream = myWebrequest.GetResponse().GetResponseStream();
            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            int i = 0;

            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                {
                    string name= "failure";
                    int score = 0;
                    var results = sLine.Split(',');
                    if (results.Count() == 2)
                    {
                        foreach (var item in results)
                        {
                          var indexOfDP = item.IndexOf(':');
                          var indexOfSK = item.IndexOf('}');

                            if (indexOfSK == -1)
                            {
                               name = item.Substring(item.IndexOf('"')).TrimEnd('"');
                            }
                            else
                            {
                                int.TryParse(item.Substring(item.IndexOf('=')).TrimEnd('}', ']'), out score);
                            }
                        }
                    }
                    result.Add(new KeyValuePair<string, int>(name, score));

                }
            }
            objReader.Close();

            return result;

        }
    }
}
