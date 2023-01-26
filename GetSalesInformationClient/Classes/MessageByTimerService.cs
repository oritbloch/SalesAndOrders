using Microsoft.AspNetCore.SignalR;
using System.Timers;

namespace MessageService
{
    public class TimerHub : Hub
    {
        private System.Timers.Timer _timer;
        private readonly IConfiguration _config;

        public TimerHub(IConfiguration configuration)
        {
            _config = configuration;
            _timer = new System.Timers.Timer(_config.GetValue<int>("IntervalForTiks"));
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Send the message to all clients connected to the hub
            Clients.All.SendAsync("ReceiveMessage", "Hello with tick in time:" + DateTime.Now.ToString());
        }
    }
}


