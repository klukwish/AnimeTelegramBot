using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using telegramBot.Constants;
using telegramBot.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace telegramBot.Clients
{
    public class Client
    {
        private HttpClient _httpClient;
        private string _address;

        public Client()
        {
            _address = Constant.myApiAddress;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_address);
        }

        public async Task<Anime> AnimeByTitle(string title)
        {
            var result = await _httpClient.GetAsync($"/AnimeByName?name={title}");

            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return null;
            }

            string responseBody = await result.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<Anime>(responseBody);

            return model;
        }

        public async Task<String> GetWikiUrl(string anime_title, string language)
        {
            var result = await _httpClient.GetAsync($"/WikiByName?name={anime_title}");
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return null;
            }

            string responseBody = await result.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<Wiki>(responseBody);

            string final_url;
            
            if(language == "en")
            {
                return model.url;
            }

            string text = model.url;
            string pattern = "en";
            string target = language;
            Regex r = new Regex(pattern);
            var url = r.Replace(text, target);
            pattern = @"\w+$";
            target = anime_title;
            Regex rg = new Regex(pattern);
            final_url = rg.Replace(url, target);

            return final_url;
        }

        public async Task<List<AnimeList>> AnimeByGenre(int genre_id)
        {
            var result = await _httpClient.GetAsync($"/AnimeByGenre?genre={genre_id}");
            string responseBody = await result.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<AnimeList>(responseBody);

            List<AnimeList> aniList = new List<AnimeList>();

            if(model.anime.Count() == 100)
            {
                Random rnd = new Random();
                List<int> list = new List<int>();
                for (int i = 0; i <10; i++)
                {
                    int count = rnd.Next(0, 100);
                    if (list.Contains(count))
                    {
                        while (list.Contains(count))
                        {
                            count = rnd.Next(0, 100);
                        }
                    }
                    list.Add(count);
                }
                foreach(int number in list)
                {
                    aniList.Add(model.anime[number]);
                }
                return aniList;
            }
            else if (model.anime.Count() > 10)
            {
                for(int i = 0; i < 10; i++)
                {
                    aniList.Add(model.anime[i]);
                }
                return aniList;
            }
            else
            {
                return model.anime;
            }

        }
        
        public async Task<AnimeList> RandomAnime()
        {
            var result = await _httpClient.GetAsync($"/AnimeByRandomGenre");
            string responseBody = await result.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<AnimeList>(responseBody);

            return model.anime[0];
        }

        public async Task<Top> Trends(string choice)
        {
            var result = await  _httpClient.GetAsync($"/TopAnime?subtype={choice}");
            string responseBody = await result.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<Top>(responseBody);

            return model;
        }

        public async Task<bool> PostDataToDB(string user_id, string main_title)
        {
            DataForDB dataForDB = new DataForDB();
            dataForDB.user_id = user_id;
            dataForDB.main_title = main_title;

            string json = JsonConvert.SerializeObject(dataForDB);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var post = await _httpClient.PostAsync("/PostAnimeToDB", data);

            var postcontent = post.Content.ReadAsStringAsync().Result;

            if(postcontent == "Value has been successfully added to DB")
            {
                return true;
            }

            return false;
        }

        public async Task<List<DataForDB>> GetMyList()
        {
            var result = await _httpClient.GetAsync("/GetAllFavorite");
            string responseBody = await result.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<List<DataForDB>>(responseBody);

            if(model.Count() == 0)
            {
                return null;
            }

            return model;
        }

        public async Task<bool> DeleteDataFromDB(string user_id, string main_title)
        {

            var result = await _httpClient.DeleteAsync($"/DeleteFromDB?user_id={user_id}&main_title={main_title}");

            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return false;
            }

            return true;

        }

        public async Task<Anime> Remainder(string user_id)
        {
            var result = await _httpClient.GetAsync($"/Remainder?user_id={user_id}");

            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return null;
            }
            string responseBody = await result.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<Anime>(responseBody);
            return model;


        }
    }
}
