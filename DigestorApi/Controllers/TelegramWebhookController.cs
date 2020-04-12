﻿using System;
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


namespace DigestorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TelegramWebhookController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger<TelegramWebhookController> _logger;
        private static TelegramBotClient Bot;
        
        public TelegramWebhookController(ILogger<TelegramWebhookController> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
            //_logger.LogInformation("constructor logged");
            Bot = new TelegramBotClient(Configuration["TelegramToken"]);
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
            }

            return Ok(update);
        }
    }
}