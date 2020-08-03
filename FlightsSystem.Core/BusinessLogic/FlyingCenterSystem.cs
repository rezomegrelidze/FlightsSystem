using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using FlightsSystem.Core.Migrations;
using Timer = System.Timers.Timer;

namespace FlightsSystem.Core.BusinessLogic
{
    // Make configuration project 
    public sealed class FlyingCenterSystem
    {
        public static readonly FlyingCenterSystem Instance = new FlyingCenterSystem();

        static readonly List<Ticket> TicketHistory = new List<Ticket>();
        static readonly List<Flight> FlightHistory = new List<Flight>();
        private FlyingCenterSystem()
        {
            Timer timer = new Timer(TimeSpan.FromDays(3).TotalMilliseconds);
            timer.Start();
            timer.AutoReset = true;
            timer.Elapsed += TimerOnElapsed;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            // code goes here
        }
    }
}