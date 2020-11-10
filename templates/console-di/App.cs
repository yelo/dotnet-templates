﻿using consoledi.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace consoledi
{
    internal class App
    {
        private const int SecondsDelay = 10;
        private readonly ILogger<App> _logger;
        private readonly AppSettings _settings;

        public App(ILogger<App> logger, IOptions<AppSettings> settings)
        {
            _logger = logger;
            _settings = settings.Value;
        }

        public async Task Run()
        {
            _logger.LogInformation($"Application running, Settings sample value: {_settings.Sample}");

            var random = new Random();
            while (true)
            {
                _logger.LogDebug($"Random: {random.Next()}, delay loop for {SecondsDelay} seconds.");
                await Task.Delay(new TimeSpan(0, 0, SecondsDelay)).ConfigureAwait(false);
            }
        }
    }
}