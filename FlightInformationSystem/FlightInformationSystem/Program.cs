using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// Оголошення класу FlightStatus (enum)
public enum FlightStatus
{
    OnTime,
    Delayed,
    Cancelled,
    Boarding,
    InFlight
}

// Оголошення класу Flight
public class Flight
{
    public string FlightNumber { get; set; }
    public string Airline { get; set; }
    public string Destination { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public string Gate { get; set; }
    public FlightStatus Status { get; set; }  // Використання типу FlightStatus
    public TimeSpan Duration { get; set; }
    public string AircraftType { get; set; }
    public string Terminal { get; set; }
}

class Program
{
    static void Main()
    {
        List<Flight> flights = new List<Flight>
        {
            new Flight
            {
                FlightNumber = "AA963",
                Airline = "WizAir",
                Destination = "Kyiv",
                DepartureTime = DateTime.Parse("2023-06-12T15:33:28"),
                ArrivalTime = DateTime.Parse("2023-06-12T17:54:28"),
                Gate = "A1",
                Status = FlightStatus.Boarding,
                Duration = TimeSpan.FromHours(2) + TimeSpan.FromMinutes(21),
                AircraftType = "Airbus A320",
                Terminal = "1"
            },
            new Flight
            {
                FlightNumber = "AF473",
                Airline = "WizAir",
                Destination = "Barcelona",
                DepartureTime = DateTime.Parse("2023-08-23T09:14:45"),
                ArrivalTime = DateTime.Parse("2023-08-23T16:22:45"),
                Gate = "A22",
                Status = FlightStatus.Boarding,
                Duration = TimeSpan.FromHours(7) + TimeSpan.FromMinutes(08),
                AircraftType = "AN 148",
                Terminal = "2"
            },
            new Flight
            {
                FlightNumber = "AF474",
                Airline = "MAU",
                Destination = "Kharkiv",
                DepartureTime = DateTime.Parse("2023-10-12T08:21:13"),
                ArrivalTime = DateTime.Parse("2023-10-12T11:01:13"),
                Gate = "F6",
                Status = FlightStatus.Boarding,
                Duration = TimeSpan.FromHours(2) + TimeSpan.FromMinutes(40),
                AircraftType = "AN 148",
                Terminal = "2"
            },
            new Flight
            {
                FlightNumber = "AF418",
                Airline = "MAU",
                Destination = "Dnipro",
                DepartureTime = DateTime.Parse("2023-05-19T02:21:51"),
                ArrivalTime = DateTime.Parse("2023-05-19T11:20:51"),
                Gate = "D29",
                Status = FlightStatus.InFlight,
                Duration = TimeSpan.FromHours(8) + TimeSpan.FromMinutes(59),
                AircraftType = "AN 148",
                Terminal = "1"
            },
            new Flight
            {
                FlightNumber = "UA791",
                Airline = "Turkish Airlines",
                Destination = "Kharkiv",
                DepartureTime = DateTime.Parse("2023-09-22T06:07:54"),
                ArrivalTime = DateTime.Parse("2023-09-22T07:08:54"),
                Gate = "E18",
                Status = FlightStatus.Delayed,
                Duration = TimeSpan.FromHours(1) + TimeSpan.FromMinutes(01),
                AircraftType = "Boeing 787",
                Terminal = "2"
            },
            new Flight
            {
                FlightNumber = "DL206",
                Airline = "WizAir",
                Destination = "New York",
                DepartureTime = DateTime.Parse("2023-03-22T15:46:53"),
                ArrivalTime = DateTime.Parse("2023-03-22T19:30:53"),
                Gate = "F6",
                Status = FlightStatus.InFlight,
                Duration = TimeSpan.FromHours(3) + TimeSpan.FromMinutes(44),
                AircraftType = "Boeing 737",
                Terminal = "1"
            },
           
        };

        var data = new Dictionary<string, List<Flight>> { { "flights", flights } };
        string filePath = @"D:\Универ\ООП\Codes\Lab_6\Lab_6\bin\Debug\net6.0\flights.json";
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        System.IO.File.WriteAllText(filePath, json);

        Console.WriteLine("JSON файл створено успішно.");
    }
}
