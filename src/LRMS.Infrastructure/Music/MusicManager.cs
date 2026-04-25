using Microsoft.Extensions.Hosting;
using Yandex.Music.Client;
using Yandex.Music.Api.Common.Ynison;
using Microsoft.Extensions.Logging;
using LRMS.Application.SpaceState.Dto;
using LRMS.Infrastructure.Persistence;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using LRMS.Infrastructure.Options;

namespace LRMS.Infrastructure.Music;

public class MusicManager : BackgroundService
{
    private readonly YandexMusicClient _client;
    private readonly ILogger<MusicManager> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private CurrentTrackDto _currentTrackDto;

    public MusicManager(ILogger<MusicManager> logger, IServiceScopeFactory scopeFactory, IOptions<YandexMusicOptions> options)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        var client = new YandexMusicClient();
        if (!client.Authorize(options.Value.Token))
        {
            _logger.LogError("Не удалось авторизоваться.");
            return;
        }

        _client = client;
        _client.ConnectToYnison();
        _client.Ynison.OnReceive += YnisonOnReceive;
        _client.Ynison.Connect();
    }

    private void YnisonOnReceive(YnisonPlayer player, YnisonPlayer.ReceiveEventArgs args)
    {
        var currentTrack = GetCurrentTack(args);
        if (_currentTrackDto.Name == currentTrack.Name && _currentTrackDto.ImageUrl == currentTrack.ImageUrl)
            return;

        _logger.LogTrace("{trackTitle} - {artists}", currentTrack.Name, string.Join(", ", currentTrack.Authors));
        _currentTrackDto = currentTrack;

        SaveInDb(currentTrack);
    }

    private CurrentTrackDto GetCurrentTack(YnisonPlayer.ReceiveEventArgs args)
    {
        var index = args.State.PlayerState.PlayerQueue.CurrentPlayableIndex;
        var currentTrackId = args.State.PlayerState.PlayerQueue.PlayableList[index].PlayableId;
        var track = _client.GetTrack(currentTrackId);
        string coverUrl = $"https://{track.CoverUri.Replace("%%", "400x400")}";
        List<string> artists = [.. track.Artists.Select(a => a.Name)];
        return new CurrentTrackDto(track.Title, artists, coverUrl);
    }

    private void SaveInDb(CurrentTrackDto currentTrack)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<LrmsDbContext>();
        var spaceState = dbContext.SpaceStates.First();
        spaceState.CurrentTrack = JsonSerializer.Serialize(currentTrack);
        dbContext.Update(spaceState);
        dbContext.SaveChanges();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _client.Ynison.Disconnect();
        _client.Ynison.OnReceive -= YnisonOnReceive;
        _client.Ynison.Dispose();
        base.Dispose();
    }
}
