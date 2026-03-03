using Microsoft.VisualBasic;
using System;
using System.Net.Sockets;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace assignmentOOP04
{

    #region Q1
    //Q1 : What is the difference between static binding and dynamic binding? 
    //    When does each one happen?
    //method overloading and non virtual methods use static binding which happens at compile time
    //while overriding and virtual methods use dnamic binding which happens at runtime 
    #endregion
    #region Q2
    //Q2 :  What is the difference between method overloading and method overriding?
    //overloading means writing the same function name but the change happens in the parameters and it happens at compile time (static binding)
    //while overriding is changing the method implementation using virtual or override and happens at runtime (dynamic binding) 
    #endregion
    #region Q3
    //Q3 : What keywords are used for Method Overriding? What does each one mean ?
    //override: provides a new implementation for the virtual method in the base class
    //virtual: the method is allowed to be changed in the child class
    #endregion
    #region Part02

    public class Ticket
    {
        private static int counter = 0;
        public string MovieName { get; set; }
        public int TicketId { get; }
        private decimal price;
        public decimal Price
        {
            get => price;
            set
            {
                if (price < 0)
                    Console.WriteLine("Price must be greater than 0");
                price = value;
            }
        }

        //Constructor
        public Ticket(string _movieName, decimal _price)
        {
            MovieName = _movieName;
            Price = _price;
            TicketId = ++counter;
        }
        public decimal PriceAfterTax { get => Price * 1.14m; }
        public static int GetTotalTickets()
        {
            return counter;
        }


        public virtual void PrintTicket()
        {
            Console.WriteLine($"Ticket #{TicketId} | Movie {MovieName} | Price {Price} | After Tax {PriceAfterTax}");
        }

        public void SetPrice(decimal price) {
            Price=price;
            Console.WriteLine($"Setting Price Directly: {price}");
        }

        public void SetPrice(decimal BasePrice, decimal multiplier)
        {
            price = BasePrice * multiplier;
            Console.WriteLine($"Setting price with multiplier: {BasePrice} x {multiplier}= {price}");
        }
        public static void ProcessTicket(Ticket t)
        {
            t.PrintTicket();
        }
    }

    public class StandardTicket : Ticket
    {
        public string SeatNumber { get; set; }
        public StandardTicket(string _movieName, decimal _price, string _seatNumber) : base(_movieName, _price)
        {
            SeatNumber = _seatNumber;
        }
        public void PrintTicket()
        {
            base.PrintTicket();
            Console.WriteLine($"| Seat: {SeatNumber}");
        }
    }
    
    public class VIPTicket : Ticket
    {
        public bool LoungeAccess { get; set; }
        public decimal ServiceFee { get; } = 50m;
        public VIPTicket(string _movieName, decimal _price, bool _loungeAccess) : base(_movieName, _price + 50m)
        {
            LoungeAccess = _loungeAccess;
        }
        public override void PrintTicket()
        {
            base.PrintTicket();
            Console.WriteLine($" | Lounge Access: {LoungeAccess} | Service Fee: {ServiceFee}");       
        }
    }
    public class IMAXTicket : Ticket
    {
        public bool Is3d { get; set; }
        public IMAXTicket(string _movieName, decimal _price, bool _is3d) : base(_movieName, _price + 30m)
        {
            Is3d = _is3d;
        }
        public override void PrintTicket()
        {
            base.PrintTicket();
            Console.WriteLine($" | 3D: {Is3d}");       ;
        }
    }

    public sealed class Projector
    {
        public void Start() => Console.WriteLine("Projector started.");
        public void Stop() => Console.WriteLine("Projector stopped.");
    }

    public class Cinema
    {
        public string CinemaName { get; set; }
        private Projector projector;
        public Cinema(string cinemaName)
        {
            CinemaName = cinemaName;
            projector = new Projector();
        }

        private Ticket[] tickets = new Ticket[20];
        public void AddTicket(Ticket t)
        {
            for (int i = 0; i < tickets.Length; i++)
            {
                if (tickets[i] == null)
                {
                    tickets[i] = t;
                    return;
                }
            }
            Console.WriteLine("Cinema is full");

        }
        public void PrintAllTickets()
        {
            foreach (var ticket in tickets)
            {
                if (ticket != null)
                    ticket.PrintTicket();
            }
        }
        public void OpenCinema()
        {
            Console.WriteLine($"=========={CinemaName} opened.=============");
            projector.Start();
        }

        public void CloseCinema()
        {
            Console.WriteLine($"=========={CinemaName} closed.==============");
            projector.Stop();
        }
    }
    
    internal class Program
    {
        static void Main(string[] args)
        {
            Cinema cinema = new Cinema("Cinema");
            cinema.OpenCinema();
            Console.WriteLine("\n=========Set Price Test========");
            Ticket t1 = new StandardTicket("Inception", 100m, "A10");
            Ticket t2 = new VIPTicket("Avatar 2", 200m, true);
            Ticket t3 = new IMAXTicket("Oppenheimer", 150m, true);
            cinema.AddTicket(t1);
            t1.SetPrice(150);
            t1.SetPrice(100,1.5m);
            cinema.AddTicket(t2);
            cinema.AddTicket(t3);
            Console.WriteLine("\n======= All Tickets ======");
            cinema.PrintAllTickets();
            Console.WriteLine("\n======= Process Single Ticket ======");
            Ticket.ProcessTicket(t1);
            Console.WriteLine("\n");
            cinema.CloseCinema();
        }
    }

}
    #endregion
    

