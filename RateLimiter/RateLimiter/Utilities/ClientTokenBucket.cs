using System.Net;
using System.Timers;

namespace RateLimiter.Utilities
{
    public class ClientTokenBucket: IDisposable
    {
        private const byte MaxTokens = 10;

        private System.Timers.Timer heartBeat;
        private bool disposedValue = false;

        Dictionary <IPAddress, int> tokenList = new Dictionary <IPAddress, int>();

        public ClientTokenBucket()
        {
            heartBeat = new System.Timers.Timer(1000);
            heartBeat.Elapsed += OnTimerExpired;
            heartBeat.AutoReset = true;
            heartBeat.Enabled = true;
        }

        public bool ValidateClient(IPAddress address)
        {
            if (tokenList.ContainsKey(address))
            {
                bool valid = tokenList[address] > 0;
                if (valid) 
                    tokenList[address]--;
                return valid;
            }
            else
            {
                tokenList.Add(address, 9);
                return true;
            }
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    heartBeat.Stop();
                    heartBeat.Dispose();
                }

                this.disposedValue = true;
            }
        }

        private void OnTimerExpired(object? sender, ElapsedEventArgs e)
        {
            //// Console.Write($"Timer Expired at {DateTime.Now}. Clients: ");
            foreach (var token in tokenList)
            {
                Console.Write($"{token.Key} ");
                if (token.Value >= MaxTokens)
                {
                    tokenList.Remove(token.Key);
                }
                else
                {
                    tokenList[token.Key]++;
                }
            }
            Console.WriteLine();
        }
    }
}
