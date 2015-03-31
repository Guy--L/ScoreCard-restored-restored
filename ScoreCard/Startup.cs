using Microsoft.Owin;
using Owin;
using ScoreCard;

namespace ScoreCard
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
