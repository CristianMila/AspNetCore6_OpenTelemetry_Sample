using System.Diagnostics;

namespace SomeApplicationProject
{
    public class RocketService
    {
        public string LaunchRocketById(int rocketId)
        {
            Activity.Current?.AddTag("tag from appService", "yes");

            return $"Liftoff! Rocket with ID: {rocketId}";
        }
    }
}