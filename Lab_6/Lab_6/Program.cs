using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public enum FlightStatus
{
    OnTime,
    Delayed,
    Cancelled,
    Boarding,
    InFlight
}

public class Flight
{
    public string FlightNumber { get; set; }
    public string Airline { get; set; }
    public string Destination { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public string Gate { get; set; }
    public FlightStatus Status { get; set; }
    public TimeSpan Duration { get; set; }
    public string AircraftType { get; set; }
    public string Terminal { get; set; }
}

public class FlightInformationSystem
{
    private List<Flight> flights;

    public FlightInformationSystem()
    {
        flights = new List<Flight>();
    }

    public void AddFlight(Flight flight)
    {
        flights.Add(flight);
    }

    public bool RemoveFlight(string flightNumber)
    {
        Flight flightToRemove = flights.Find(f => f.FlightNumber == flightNumber);
        if (flightToRemove != null)
        {
            flights.Remove(flightToRemove);
            return true;
        }
        return false;
    }

    public List<Flight> SearchFlights(Predicate<Flight> condition)
    {
        return flights.FindAll(condition);
    }

    public void LoadFlightsFromJson(string filePath)
    {
        try
        {
            string json = File.ReadAllText(filePath);
            var data = JsonConvert.DeserializeObject<Dictionary<string, List<Flight>>>(json);

            if (data.TryGetValue("flights", out var flightsList))
            {
                flights = flightsList;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading flights from JSON: {ex.Message}");
        }
    }

    public void DisplayFlightInfo(List<Flight> flights)
    {
        foreach (var flight in flights)
        {
            Console.WriteLine($"Flight {flight.FlightNumber} - {flight.Airline}");
            Console.WriteLine($"Departure: {flight.DepartureTime}, Arrival: {flight.ArrivalTime}");
            Console.WriteLine($"Status: {flight.Status}, Duration: {flight.Duration}");
            Console.WriteLine($"Destination: {flight.Destination}, Terminal: {flight.Terminal}");
            Console.WriteLine();
        }
    }

    public List<Flight> GetFlightsByAirline(string airline)
    {
        return flights.FindAll(f => f.Airline == airline).OrderBy(f => f.DepartureTime).ToList();
    }

    public List<Flight> GetDelayedFlights()
    {
        return flights.FindAll(f => f.Status == FlightStatus.Delayed).OrderBy(f => f.DepartureTime).ToList();
    }

    public List<Flight> GetFlightsByDate(DateTime date)
    {
        return flights.FindAll(f => f.DepartureTime.Date == date.Date).OrderBy(f => f.DepartureTime).ToList();
    }

    public List<Flight> GetFlightsByTimeRange(DateTime startTime, DateTime endTime, string destination)
    {
        return flights.FindAll(f =>
            f.DepartureTime >= startTime &&
            f.DepartureTime <= endTime &&
            f.ArrivalTime >= startTime &&
            f.ArrivalTime <= endTime &&
            f.Destination == destination
        ).OrderBy(f => f.DepartureTime).ToList();
    }

    public List<Flight> GetRecentArrivals(TimeSpan timeRange)
    {
        DateTime currentTime = DateTime.Now;
        DateTime startTime = currentTime - timeRange;
        return flights.FindAll(f => f.ArrivalTime >= startTime).OrderBy(f => f.ArrivalTime).ToList();
    }
}

class Program
{
    static void Main()
    {
        FlightInformationSystem flightSystem = new FlightInformationSystem();
        flightSystem.LoadFlightsFromJson("flights.json");

        Console.WriteLine("All flights:");
        flightSystem.DisplayFlightInfo(flightSystem.SearchFlights(_ => true));

        Console.WriteLine("WizAir flights:");
        flightSystem.DisplayFlightInfo(flightSystem.GetFlightsByAirline("WizAir"));

        Console.WriteLine("Delayed flights:");
        flightSystem.DisplayFlightInfo(flightSystem.GetDelayedFlights());

        Console.WriteLine("Flights on 2023-06-12:");
        flightSystem.DisplayFlightInfo(flightSystem.GetFlightsByDate(new DateTime(2023, 6, 12)));

        Console.WriteLine("Flights from 2023-05-01 to 2023-05-31 to Kharkiv:");
        flightSystem.DisplayFlightInfo(
            flightSystem.GetFlightsByTimeRange(
                new DateTime(2023, 5, 1),
                new DateTime(2023, 5, 31, 23, 59, 59),
                "Kharkiv"
            )
        );

        Console.WriteLine("Recent arrivals in the last hour:");
        flightSystem.DisplayFlightInfo(flightSystem.GetRecentArrivals(TimeSpan.FromHours(1)));
    }
}
