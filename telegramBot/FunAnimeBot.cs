using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Exceptions;
using telegramBot.Constants;
using telegramBot.Models;
using telegramBot.ApiCalls;
using System.Text.RegularExpressions;

namespace telegramBot
{
    public class FunAnimeBot
    {
        TelegramBotClient botClient = new TelegramBotClient(Constant.botToken);
        CancellationToken cancellationToken = new CancellationToken();
        ReceiverOptions receiverOptions = new ReceiverOptions { AllowedUpdates = { } };

        public Anime anime = new Anime();
        List<Anime> animes = new List<Anime>();

        public async Task Start()
        {
            botClient.StartReceiving(HandlerUpdateAsync, HandlerError, receiverOptions, cancellationToken);
            var botMe = await botClient.GetMeAsync();
            Console.WriteLine("працює" + botMe.Username);
            
        }

        private Task HandlerError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Error TG:\n {apiRequestException.ErrorCode}  +" +
                $"\n {apiRequestException.Message}",_ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private async Task HandlerUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if(update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await HandlerMessage(botClient, update.Message); 
            }
            if(update.Type == UpdateType.CallbackQuery)
            {
                await HandlerCallbackQuery(botClient, update.CallbackQuery);
            }
            

        }
        async Task HandlerCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            if (callbackQuery.Data == "Help")
            {
                await SendHelpMessage(callbackQuery);
                return;
            }
            else if (callbackQuery.Data == "ByTitle")
            {
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Enter anime title!");
                return;
            }
            else if( callbackQuery.Data == "Show more info about anime")
            {
                await ShowMoreInfoAboutAnime(callbackQuery);
                return;
            }
            else if(callbackQuery.Data == "Wiki")
            {
                await SendWikiLanguageMessage(callbackQuery);
                return;
            }
            else if(callbackQuery.Data == "ByGenre")
            {
                await ShowGenres(callbackQuery);
                return;
            }
            else if(callbackQuery.Data == "Random")
            {
                await RandomAnimeReccomendation(callbackQuery);
            }
            else if(callbackQuery.Data == "1")
            {
                await ShowAnimeListByGenre(callbackQuery, 1);
                return;
            }
            else if (callbackQuery.Data == "2")
            {
                await ShowAnimeListByGenre(callbackQuery, 2);
                return;
            }
            else if (callbackQuery.Data == "24")
            {
                await ShowAnimeListByGenre(callbackQuery, 24);
                return;
            }
            else if (callbackQuery.Data == "4")
            {
                await ShowAnimeListByGenre(callbackQuery, 4);
                return;
            }
            else if (callbackQuery.Data == "42")
            {
                await ShowAnimeListByGenre(callbackQuery, 42);
                return;
            }
            else if (callbackQuery.Data == "6")
            {
                await ShowAnimeListByGenre(callbackQuery, 6);
                return;
            }
            else if (callbackQuery.Data == "7")
            {
                await ShowAnimeListByGenre(callbackQuery, 7);
                return;
            }
            else if (callbackQuery.Data == "8")
            {
                await ShowAnimeListByGenre(callbackQuery, 8);
                return;
            }
            else if (callbackQuery.Data == "41")
            {
                await ShowAnimeListByGenre(callbackQuery, 41);
                return;
            }
            else if (callbackQuery.Data == "10")
            {
                await ShowAnimeListByGenre(callbackQuery, 10);
                return;
            }
            else if (callbackQuery.Data == "11")
            {
                await ShowAnimeListByGenre(callbackQuery, 11);
                return;
            }
            else if (callbackQuery.Data == "13")
            {
                await ShowAnimeListByGenre(callbackQuery, 13);
                return;
            }
            else if (callbackQuery.Data == "14")
            {
                await ShowAnimeListByGenre(callbackQuery, 14);
                return;
            }
            else if (callbackQuery.Data == "17")
            {
                await ShowAnimeListByGenre(callbackQuery, 17);
                return;
            }
            else if (callbackQuery.Data == "18")
            {
                await ShowAnimeListByGenre(callbackQuery, 18);
                return;
            }
            else if (callbackQuery.Data == "19")
            {
                await ShowAnimeListByGenre(callbackQuery, 19);
                return;
            }
            else if (callbackQuery.Data == "37")
            {
                await ShowAnimeListByGenre(callbackQuery, 37);
                return;
            }
            else if (callbackQuery.Data == "21")
            {
                await ShowAnimeListByGenre(callbackQuery, 21);
                return;
            }
            else if (callbackQuery.Data == "22")
            {
                await ShowAnimeListByGenre(callbackQuery, 22);
                return;
            }
            else if (callbackQuery.Data == "23")
            {
                await ShowAnimeListByGenre(callbackQuery, 23);
                return;
            }
            else if (callbackQuery.Data == "26")
            {
                await ShowAnimeListByGenre(callbackQuery, 26);
                return;
            }
            else if (callbackQuery.Data == "40")
            {
                await ShowAnimeListByGenre(callbackQuery, 40);
                return;
            }
            else if (callbackQuery.Data == "28")
            {
                await ShowAnimeListByGenre(callbackQuery, 28);
                return;
            }
            else if (callbackQuery.Data == "29")
            {
                await ShowAnimeListByGenre(callbackQuery, 29);
                return;
            }
            else if (callbackQuery.Data == "30")
            {
                await ShowAnimeListByGenre(callbackQuery, 30);
                return;
            }
            else if (callbackQuery.Data == "31")
            {
                await ShowAnimeListByGenre(callbackQuery, 31);
                return;
            }
            else if (callbackQuery.Data == "32")
            {
                await ShowAnimeListByGenre(callbackQuery, 32);
                return;
            }
            else if (callbackQuery.Data == "36")
            {
                await ShowAnimeListByGenre(callbackQuery, 36);
                return;
            }
            else if(callbackQuery.Data == "Trends")
            {
                await ChooseSubtypeForTrends(callbackQuery);
                return;
            }
            else if(callbackQuery.Data == "bypopularity")
            {
                await ShowTrends(callbackQuery, "bypopularity");
                return;
            }
            else if(callbackQuery.Data == "upcoming")
            {
                await ShowTrends(callbackQuery, "upcoming");
                return;
            }
            else if(callbackQuery.Data == "airing")
            {
                await ShowTrends(callbackQuery, "airing");
                return;
            }
            else if(callbackQuery.Data == "Add to list")
            {
                await AddToList(callbackQuery);
                return;
            }
            else if (callbackQuery.Data.StartsWith("Delete"))
            {
                string text = callbackQuery.Data;
                Regex regex = new Regex(@"[0-9]+$");
                Match match = regex.Match(text);
                string number_in_list = match.Value;
                int number_list = Convert.ToInt32(number_in_list);
                string main_title = animes[number_list].main_title;
                await Delete(callbackQuery, main_title);
                return;
            }

        }
        private async Task HandlerMessage(ITelegramBotClient botClient, Message message)
        {
            await Remainder(message);
            if (message.Text == "/start")
            {

                SendStartMessage(message, botClient);
                return;
            }
            else if (message.Text == "/help")
            {
                SendHelpMessage(message, botClient);
                return;
            }
            else if (message.Text == "Search")
            {

                InlineKeyboardMarkup keyboardMarkup = new
                (
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Search anime by title", "ByTitle")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Reccomendation by genre", "ByGenre")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Random reccomedation", "Random")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Trends", "Trends")
                    }
                }
                );

                await botClient.SendTextMessageAsync(message.Chat.Id, "Choose between following options:", replyMarkup: keyboardMarkup);
                return;
            }
            else if (message.Text == "My list")
            {
                ShowMyList(message);
                return;
            }
            else if (message.Text != null)
            {
                string title = message.Text;
                SearchByTitle(message, title);
            }
        }

        public async Task SendStartMessage(Message message, ITelegramBotClient botClient)
        {

            InlineKeyboardMarkup keyboardMarkup = new
                (
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("More information", "Help")
                    }
                }
                );

            await botClient.SendTextMessageAsync(message.Chat.Id, "Hello! Welcome to AnimeBot! \n" +
                "Here you can do several functions: \n" +
                  " 💫 search anime by title \n" +
                  " 💫 get anime reccomendations by genre\n" +
                  " 💫 get random anime reccomendation\n" +
                  " 💫 get trends\n" +
                  " 💫 save your favourite anime to your own list\n" +
                  " 💫 get a remainder when new series of your favourits are released\n" +
                  "If you need any help about using this bot, please choose button under this message!\n" +
                  "Enjoy your time!❣", replyMarkup: keyboardMarkup);
            ReplyKeyboardMarkup replyKeyboardMarkup = new
                  (
                  new[]
                  {
                        new KeyboardButton [] {"Search", "My list"}
                  }
                  )
            {
                ResizeKeyboard = true
            };
            await botClient.SendTextMessageAsync(message.Chat.Id, "Check your keyborad!!", replyMarkup: replyKeyboardMarkup);
        }

        public async Task SendHelpMessage(CallbackQuery callbackQuery)
        {
            string helpmessage = HelpMessage();
            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, helpmessage);
        }

        public async Task SendHelpMessage(Message message, ITelegramBotClient botClient)
        {
            string helpmessage = HelpMessage();
            await botClient.SendTextMessageAsync(message.Chat.Id, helpmessage);
        }

        private string HelpMessage()
        {
            return "There are two buttons on your keyboard.\n" +
                "If you choose Search you will have next options: \n" +
                "💫 search anime by title: just choose necessary button and right after enter anime title (it can be both it`s English or English-Japanese title, " +
                "for example Attack on titan or Shingeki no Kyojin) \n" +
                "💫 get anime reccomendations by genre: just choose necessary button and then choose genre \n" +
                "💫 get random anime reccomendation: just choose necessary button\n" +
                "💫 save your favourite anime to your own list: to do this you need to search anime by title and then a necessary button (Add to favourit) will appear under the message that you will receive \n" +
                "If you choose another button on your keyboard(My list) you will get list of your chosen anime.\n" +
                "If you need to see this message again, just send /help .\n" +
                "Have a great day!❣";
        }

        public async Task SearchByTitle(Message message, string title)
        {
            anime = await ApiCall.GetAnimeByTitle(title);

            if (anime == null)
            {
                await  botClient.SendTextMessageAsync(message.Chat.Id, "There is no anime with such title. Try again!");
                return;
            }


            string text = $"{anime.main_title} \n" +
                $"Status: {anime.status} \n" +
                $"Amount of OVA: {anime.ova_count} \n" +
                $"Genres:\n";
            foreach(string genre in anime.listofgenres)
            {
                text += $" - {genre}\n";
            }


            InlineKeyboardMarkup keyboardMarkup = new
               (
               new[]
               {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("See more", "Show more info about anime"),
                        InlineKeyboardButton.WithCallbackData("See in Wiki", "Wiki")
                    },
                   new[]
                   {
                       InlineKeyboardButton.WithCallbackData("Add to favourit", "Add to list")
                   }
               }
               );


            string image = $"{anime.data[0].attributes.posterImage.medium}";

            await botClient.SendPhotoAsync(chatId: message.Chat.Id,
                photo: $"{image}",
                caption: text,
                cancellationToken: cancellationToken, 
                replyMarkup: keyboardMarkup);

            await botClient.SendTextMessageAsync(message.Chat.Id, $"Description: {anime.data[0].attributes.synopsis}");

        }

        public async Task ShowMoreInfoAboutAnime(CallbackQuery callbackQuery)
        {
            for(int i = 0; i < anime.data.Count; i++)
            {
                string image = anime.data[i].attributes.posterImage.medium;
                string text = "";
                if (anime.data[i].attributes.endDate != null)
                {
                        text = $"{anime.data[i].attributes.titles.en_jp}\n" +
                        $"Status: {anime.data[i].attributes.status}\n" +
                        $"Start date:{anime.data[i].attributes.startDate}\n" +
                        $"End date: {anime.data[i].attributes.endDate}\n" +
                        $"Rating: {anime.data[i].attributes.averageRating}\n" +
                        $"Age rating: {anime.data[i].attributes.ageRating}\n" +
                        $"Type:{anime.data[i].attributes.subtype}";
                }
                else if (anime.data[i].attributes.endDate == null)
                {
                    text = $"{anime.data[i].attributes.titles.en_jp}\n" +
                        $"Status: {anime.data[i].attributes.status}\n" +
                        $"Status:{anime.data[i].attributes.status}\n" +
                        $"Rating: {anime.data[i].attributes.averageRating}\n" +
                        $"Age rating: {anime.data[i].attributes.ageRating}\n" +
                        $"Type:{anime.data[i].attributes.subtype}";
                }

                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: $"{image}",
                caption: text,
                cancellationToken: cancellationToken);
            }
        }


        public async Task SendWikiLanguageMessage(CallbackQuery callbackQuery)
        {
            string url_uk =await ApiCall.SearchWiki("uk", anime.main_title);
            string url_en = await ApiCall.SearchWiki("en", anime.main_title);
            string url_cs = await ApiCall.SearchWiki("cs", anime.main_title);
            string url_de = await ApiCall.SearchWiki("de", anime.main_title);
            string url_fr = await ApiCall.SearchWiki("fr", anime.main_title);
            string url_es = await ApiCall.SearchWiki("es", anime.main_title);
            string url_pl = await ApiCall.SearchWiki("pl", anime.main_title);
            string url_it = await ApiCall.SearchWiki("it", anime.main_title);
            string url_ja = await ApiCall.SearchWiki("ja", anime.main_title);

            if (url_uk == null && url_en == null && url_cs == null &&
                url_de == null && url_fr == null && url_es == null &&
                url_pl == null && url_it == null && url_ja == null)
            {
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Unfortunately, there is no article.");
                return;
            }
            InlineKeyboardMarkup keyboardMarkup = new
                (
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Ukrainian", url_uk),
                        InlineKeyboardButton.WithUrl("English", url_en),
                        InlineKeyboardButton.WithUrl("Czech", url_cs)
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("German", url_de),
                        InlineKeyboardButton.WithUrl("French", url_fr),
                        InlineKeyboardButton.WithUrl("Spanish", url_es)
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Polish", url_pl),
                        InlineKeyboardButton.WithUrl("Italian", url_it),
                        InlineKeyboardButton.WithUrl("Japanese", url_ja)
                    }
                }
                );

            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Choose language:", replyMarkup: keyboardMarkup);
        }

        public async Task ShowGenres(CallbackQuery callbackQuery)
        {
            InlineKeyboardMarkup keyboardMarkup = new
               (
               new[]
               {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Action", "1"),
                        InlineKeyboardButton.WithCallbackData("Adventure", "2"),
                        InlineKeyboardButton.WithCallbackData("Sci Fi", "24"),
                        InlineKeyboardButton.WithCallbackData("Comedy", "4")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Seinen", "42"),
                        InlineKeyboardButton.WithCallbackData("Demons", "6"),
                        InlineKeyboardButton.WithCallbackData("Mystery", "7"),
                        InlineKeyboardButton.WithCallbackData("Drama","8")

                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Suspense", "41"),
                        InlineKeyboardButton.WithCallbackData("Fantasy", "10"),
                        InlineKeyboardButton.WithCallbackData("Game", "11"),
                        InlineKeyboardButton.WithCallbackData("Historical", "13")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Horror", "14"),
                        InlineKeyboardButton.WithCallbackData("Martial Arts", "17"),
                        InlineKeyboardButton.WithCallbackData("Mecha", "18"),
                        InlineKeyboardButton.WithCallbackData("Music", "19")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Supernatural", "37"),
                        InlineKeyboardButton.WithCallbackData("Samurai", "21"),
                        InlineKeyboardButton.WithCallbackData("Romance", "22"),
                        InlineKeyboardButton.WithCallbackData("School", "23")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Girls Love", "26"),
                        InlineKeyboardButton.WithCallbackData("Psychological", "40"),
                        InlineKeyboardButton.WithCallbackData("Boys Love", "28"),
                        InlineKeyboardButton.WithCallbackData("Space", "29")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Sports", "30"),
                        InlineKeyboardButton.WithCallbackData("Super Power", "31"),
                        InlineKeyboardButton.WithCallbackData("Vampire", "32"),
                        InlineKeyboardButton.WithCallbackData("Slice Of Life", "36")
                    }
               }
               );
            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Genres🌸", replyMarkup: keyboardMarkup);
        }

        public async Task ShowAnimeListByGenre(CallbackQuery callbackQuery, int genre_id)
        {
            var aniList = await ApiCall.AnimeByGenre(genre_id);

            string text = "Results: \n";

            for(int i = 0; i < aniList.Count(); i++)
            {
                text += $"{i+1}. {aniList[i].title}\n";
            }

            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, text);
        }

        public async Task RandomAnimeReccomendation(CallbackQuery callbackQuery)
        {
            var anime = await ApiCall.RandomAnime();
            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Your reccomendation: {anime.title}");
        }

        public async Task ChooseSubtypeForTrends(CallbackQuery callbackQuery)
        {
            InlineKeyboardMarkup keyboardMarkup = new
                (
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("By Popularity", "bypopularity")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Upcoming", "upcoming")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Airing", "airing")
                    }
                }
                );
            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Choose an option:", replyMarkup: keyboardMarkup);
        }

        public async Task ShowTrends(CallbackQuery callbackQuery, string choice)
        {
            var top = await ApiCall.Trends(choice);

            string text = "Top: \n";
            if(top.top.Count() >= 20)
            {
                for(int i = 0; i <20; i++)
                {
                    text += $"{i+1}. {top.top[i].title} \n";
                }
            }
            else 
            {
                for (int i = 0; i < top.top.Count(); i++)
                {
                    text += $"{i + 1}. {top.top[i].title} \n";
                }
            }

            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, text);
        }

        public async Task AddToList(CallbackQuery callbackQuery)
        {
            var result =await ApiCall.AddToUserList($"{callbackQuery.Message.Chat.Id}", anime.main_title);

            if (result)
            {
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Anime has been successfully added to your list!");
                return;
            }

            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Unfortunately, bot is not able to add this anime to your list😔");
        }

        public async Task ShowMyList(Message message)
        {
            animes = new List<Anime>();
            var result = await ApiCall.GetMyList();

            List<DataForDB> user_list = new List<DataForDB>();

            foreach(var item in result)
            {
                if(item.user_id == $"{message.Chat.Id}")
                {
                    user_list.Add(item);
                }
            }

            foreach(var item in user_list)
            {
                Anime anime = await ApiCall.GetAnimeByTitle(item.main_title);

                animes.Add(anime);

            }

            for(int i = 0; i < animes.Count(); i++)
            {
                string text = $"{animes[i].main_title} \n" +
                $"Status: {animes[i].status} \n" +
                $"Amount of OVA: {animes[i].ova_count} \n" +
                $"Genres:\n";
                foreach (string genre in animes[i].listofgenres)
                {
                    text += $" - {genre}\n";
                }

                InlineKeyboardMarkup keyboardMarkup = new
                (
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Delete", $"Delete{i}"),
                    },
                }
                );
                await botClient.SendTextMessageAsync(message.Chat.Id, text, replyMarkup: keyboardMarkup);
            }

        }

        public async Task Delete(CallbackQuery callbackQuery, string main_title)
        {
            var result = await ApiCall.DeleteData($"{callbackQuery.Message.Chat.Id}", main_title);

            if (!result)
            {
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Cannot delete that anime😔");
                return;
            }

            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Anime has been deleted from your list.");
        }

        public async Task Remainder(Message message)
        {
            var result = await ApiCall.GetRemainder($"{message.Chat.Id}");

            if (result == null)
            {
               return;
            }
            else
            {
               await botClient.SendTextMessageAsync(message.Chat.Id, $"The new serie of {result.main_title} is on!");
               return;
            }

        }
    }
}
