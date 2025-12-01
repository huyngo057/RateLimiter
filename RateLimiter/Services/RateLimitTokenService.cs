namespace RateLimiter.Services;

public class RateLimitTokenService
{
    private const int MaxToken = 10;
    private readonly Dictionary<string, int> _tokens = new();
    
    public bool IsTokenExceedLimit(string ipAddress)
    {
        if (_tokens.TryGetValue(ipAddress, out int value) && value == 0)
        {
            return true;
        }
        ConsumeToken(ipAddress);
        return false;
    }

    public void RenewTokens()
    {
        foreach (var ipAddress in _tokens.Keys.Where(ipAddress => _tokens[ipAddress] < MaxToken))
        {
            _tokens[ipAddress]++;
        }
    }

    private void ConsumeToken(string ipAddress)
    {
        if(!_tokens.ContainsKey(ipAddress)) 
        {   
           AddToken(ipAddress);
        }

        if (_tokens[ipAddress] > 0)
        {
            _tokens[ipAddress]--;
        }
    }
    
    private void AddToken(string ipAddress)
    {
        _tokens.TryAdd(ipAddress, MaxToken);
    }

    private void RemoveToken(string ipAddress)
    {
        _tokens.Remove(ipAddress);
    }
}