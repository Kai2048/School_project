using System;
using System.Collections.Generic;
namespace DataLibrary
{
    public class Kons_Test
    {
        public string Name;                                                                                     // Name of topic
        public int Number;
        public Kons_Test TopTopic;
        public List<Kons_Test> O_COMM_TOPICS = new List<Kons_Test>();                                           // List of all O-Comm Topics
        public List<Kons_Test> B_COMM_TOPICS = new List<Kons_Test>();                                           // List of all B-Comm Topics


        public string Material;                                                                                 // String of text materials

        string top;
        string[] o_comm;
        string[] b_comm;

        ///////////////////////////////////////////////////////


        public Kons_Test(string a)
        {
            this.Name = a;
        }
        public Kons_Test(string a, Kons_Test b)
        {
            this.Name = a;
            this.TopTopic = b;
        }
        public Kons_Test(string name, string top, string[] o, string[] b)
        {
            this.Name = name;
            this.top = top;
            this.b_comm = b;
            this.o_comm = o;
        }


        public override string ToString()
        {
            return this.Name + " : " + this.TopTopic?.Name;
        }
        public void Mess()
        {
            Console.WriteLine("####################");

            Console.WriteLine(this.Name);
            Console.WriteLine(this.TopTopic?.Name);
            Console.WriteLine(this.Number);

            Console.WriteLine("O TOPICS:");
            foreach (Kons_Test i in this.O_COMM_TOPICS)
            {
                Console.WriteLine($"    {i.Name}");
            }

            Console.WriteLine("B TOPICS:");
            foreach (Kons_Test i in this.B_COMM_TOPICS)
            {
                Console.WriteLine($"    {i.Name}");
            }

            Console.WriteLine("******************");
        }



        public void Update()
        {
            DataBase.Add_O_Comm(this.top, this.Name);                                                           // Add Top topic
            foreach (string j in this.o_comm)
            {
                DataBase.Add_O_Comm(this.Name, j);                                                              // Adding all O-Comm topics
            }
            foreach (string j in this.b_comm)
            {
                DataBase.Add_B_Comm(j, this.Name);                                                              // Adding all B-Comm topics
            }
        }
    }
}
