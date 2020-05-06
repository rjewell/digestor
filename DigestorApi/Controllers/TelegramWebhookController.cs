using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using DigestorApi.Models;



namespace DigestorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TelegramWebhookController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger<TelegramWebhookController> _logger;
        private static TelegramBotClient Bot;
        private readonly MessageLogContext MessageLog;
        
        public TelegramWebhookController(ILogger<TelegramWebhookController> logger, IConfiguration configuration, MessageLogContext context)
        {
            _logger = logger;
            Configuration = configuration;
            //_logger.LogInformation("constructor logged");
            Bot = new TelegramBotClient(Configuration["TelegramToken"]);
            MessageLog = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Telegram.Bot.Types.Update update)
        {
            //_logger.LogInformation("post logged");
            var message = update.Message;
            if (message == null || message.Type != MessageType.Text)
                return Ok();

            switch (message.Text.Split(' ').First())
            {
                // Send inline keyboard
                case "/info":
                    await Bot.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "DigestorBot - commands coming..."
                    );
                    break;
                case "/summary":
                    var count = (from t in MessageLog.Messages
                        where t.ChatId == (int)message.Chat.Id
                        select t).Count();
                    await Bot.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: $@"DigestorBot Summary
==================
Total Messages: { count }"
                    );
                    break;
                default:

                    var msg = new Message{
                        ChatId = (int)message.Chat.Id,
                        Sender = message.Chat.Username,
                        Content = message.Text
                    };
                    MessageLog.Messages.Add(msg);
                    MessageLog.SaveChanges();
                    Console.Out.Write(message.Text);
                    break;
            }

            return Ok(update);
        }
    }
}
