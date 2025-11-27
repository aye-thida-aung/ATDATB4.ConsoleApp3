using System;
using System.Collections.Generic;
using System.Linq;

namespace TB4.ConsoleApp2
{
    internal class Program
    {
        static List<Room> Rooms = new List<Room>()
        {
            new Room(1, "101", "Single", 30000, "Available"),
            new Room(2, "202", "Double", 50000, "Booked"),
        };

        static void Main(string[] args)
        {
        Start:
            Console.WriteLine("--- Hotel Room Management ---");
            Console.WriteLine("1. Add Room");
            Console.WriteLine("2. Room List");
            Console.WriteLine("3. Edit Room");
            Console.WriteLine("4. Delete Room");
            Console.WriteLine("5. Exit");

            Console.Write("Please choose an option: ");

            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    AddRoom();
                    goto Start;

                case 2:
                    GetRooms();
                    goto Start;

                case 3:
                    EditRoom();
                    goto Start;

                case 4:
                    DeleteRoom();
                    goto Start;

                case 5:
                default:
                    break;
            }

            Console.ReadLine();
        }

        private static void DeleteRoom()
        {
            Console.Write("Please enter Room Id: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Are you sure want to delete? (Y/N): ");
            string confirm = Console.ReadLine();
            if (confirm.ToUpper() != "Y")
            {
                return;
            }

            var room = Rooms.Where(x => x.RoomId == id).FirstOrDefault();
            if (room is null)
            {
                Console.WriteLine("No room found.");
                return;
            }

            Rooms.Remove(room);
            Console.WriteLine("Room deleted.");
        }

        private static void EditRoom()
        {
            Console.Write("Please enter Room Id: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var room = Rooms.Where(x => x.RoomId == id).FirstOrDefault();
            if (room is null)
            {
                Console.WriteLine("No room found.");
                return;
            }

            Console.WriteLine($"{room.RoomId}. / No - {room.RoomNumber} / Type - {room.RoomType} / Price - {room.Price} / Status - {room.Status}");
            Console.WriteLine("------------------------");

            Console.Write("Please enter new Room Number: ");
            string roomNumber = Console.ReadLine();
            if (string.IsNullOrEmpty(roomNumber))
            {
                roomNumber = room.RoomNumber;
            }

            Console.Write("Please enter new Room Type: ");
            string roomType = Console.ReadLine();
            if (string.IsNullOrEmpty(roomType))
            {
                roomType = room.RoomType;
            }

            Console.Write("Please enter new Price: ");
            string str = Console.ReadLine();
            decimal price;
            if (string.IsNullOrEmpty(str))
            {
                price = room.Price;
            }
            else
            {
                price = Convert.ToDecimal(str);
            }

            Console.Write("Please enter new Status: ");
            string status = Console.ReadLine();
            if (string.IsNullOrEmpty(status))
            {
                status = room.Status;
            }

            int index = Rooms.FindIndex(x => x.RoomId == id);
            Rooms[index].RoomNumber = roomNumber;
            Rooms[index].RoomType = roomType;
            Rooms[index].Price = price;
            Rooms[index].Status = status;

            Console.WriteLine("Room updated.");
        }

        private static void GetRooms()
        {
            Console.WriteLine("Room List:");
            Console.WriteLine($"Room Count: {Rooms.Count}");
            foreach (Room room in Rooms)
            {
                Console.WriteLine($"{room.RoomId}. / No - {room.RoomNumber} / Type - {room.RoomType} / Price - {room.Price} / Status - {room.Status}");
                Console.WriteLine("------------------------");
            }
        }

        private static void AddRoom()
        {
            Console.Write("Please enter Room Number: ");
            string roomNumber = Console.ReadLine();

            Console.Write("Please enter Room Type: ");
            string roomType = Console.ReadLine();

            Console.Write("Please enter Price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Please enter Status: ");
            string status = Console.ReadLine();

            int no = Rooms.Max(x => x.RoomId) + 1;
            Room room = new Room(no, roomNumber, roomType, price, status);
            Rooms.Add(room);

            Console.WriteLine("Room Saved.");
        }

        public class Room
        {
            public Room(int roomId, string roomNumber, string roomType, decimal price, string status)
            {
                RoomId = roomId;
                RoomNumber = roomNumber;
                RoomType = roomType;
                Price = price;
                Status = status;
            }
            public int RoomId { get; set; }
            public string RoomNumber { get; set; }
            public string RoomType { get; set; }
            public decimal Price { get; set; }
            public string Status { get; set; }
        }
    }
}
