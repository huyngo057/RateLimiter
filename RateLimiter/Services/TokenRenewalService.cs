namespace RateLimiter.Services;

public class TokenRenewalService : BackgroundService
{
    // but services here so we can get tokens data, will be removed when tokens are stored separately.
    private readonly RateLimitTokenService _service;
    private readonly TimeSpan _renewalInterval = TimeSpan.FromSeconds(1);
    
    public TokenRenewalService(RateLimitTokenService service)
    {
        _service = service;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(_renewalInterval, stoppingToken);
            _service.RenewTokens();
        }
    }
}