using System;
using System.Collections.Generic;

namespace Hjemmeoppgave_Entur_Github
{
    public class Departure
    {
        public Departure(string origin, string municipality, int passengers, DateTime start)
        {
            this.origin = origin;
            this.municipality = municipality;
            this.passengers = passengers;
            this.start = start;
        }
        public string origin { get; set; }
        public string municipality { get; set; }
        public int passengers { get; set; }
        public DateTime start { get; set; }
    }
    public class TrainSystem
    {
        private List<Departure> departures = new List<Departure>();
        public void createDeparture(Departure departure)
        {
            departures.Add(departure);
        }

        public string getTravelTime(string origin, string destination)
        {
            Departure departure = null;

            foreach (Departure departure1 in departures)
            {
                if (departure1.origin == origin)
                    departure = departure1;

                if (departure1.origin == destination)
                {
                    if (departure != null)
                    {
                        return (departure1.start - departure.start).ToString();
                    }
                }
            }
            return "Not Valid";
        }

        public List<Tuple<string, DateTime>> getAvailableTrain(string origin, DateTime time)
        {
            List<Tuple<string, DateTime>> data = new List<Tuple<string, DateTime>>();
            foreach (Departure departure in departures)
            {
                if (departure.origin == origin && departure.start < time)
                    data.Add(new Tuple<string, DateTime>(departure.origin, departure.start));
            }
            return data;
        }

        public int getPassengers(string origin)
        {
            int passengers = 0;
            foreach (Departure departure in departures)
            {
                if (departure.origin == origin)
                    break;

                passengers += departure.passengers;
            }
            return passengers;
        }

        public int calculateOperatorReward(string origin)
        {
            int amount = 0;
            foreach (Departure departure in departures)
            {
                if (departure.origin == origin)
                    break;

                amount += getPassengers(origin) * 100;
            }

            return amount;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            TrainSystem trainSystem = new TrainSystem();

            Console.WriteLine("Oppgave 1: Oppretter togavgang. (createDeparture)");

            trainSystem.createDeparture(new Departure("Lillehammer", "Lillehammer", 300, DateTime.Parse("00:00:00")));
            trainSystem.createDeparture(new Departure("Moelv", "Ringsaker", 175, DateTime.Parse("00:30:00")));
            trainSystem.createDeparture(new Departure("Brumunddal", "Ringsaker", 350, DateTime.Parse("01:00:00")));
            trainSystem.createDeparture(new Departure("Stange", "Stange", 200, DateTime.Parse("01:20:00")));
            trainSystem.createDeparture(new Departure("Tangen", "Stange", 200, DateTime.Parse("01:40:00")));
            trainSystem.createDeparture(new Departure("Oslo Lufthavn", "Ullensaker", 800, DateTime.Parse("02:00:00")));
            trainSystem.createDeparture(new Departure("Oslo S", "Oslo", 0, DateTime.Parse("02:30:00")));
            trainSystem.createDeparture(new Departure("Tangen", "Oslo", 200, DateTime.Parse("01:24:00")));
            trainSystem.createDeparture(new Departure("Tangen", "Trondheim", 200, DateTime.Parse("04:30:00")));
            trainSystem.createDeparture(new Departure("Tangen", "Oslo", 200, DateTime.Parse("05:30:00")));

            Console.WriteLine("Oppgave 2: {0}", trainSystem.getTravelTime("Lillehammer", "Stange"));

            trainSystem.getAvailableTrain("Tangen", DateTime.Parse("05:00:00")).ForEach(delegate (Tuple<string, DateTime> data)
            {
                Console.WriteLine("Oppgave 3: Stop: {0}, Time {1}", data.Item1, data.Item2);
            });

            Console.WriteLine("Oppgave 4: {0}", trainSystem.getPassengers("Oslo S"));

            Console.WriteLine("Oppgave 5: {0}", trainSystem.calculateOperatorReward("Tangen"));
            Console.ReadLine();

        }
    }
}
