using System;
using System.Collections.Generic;
using System.IO;
namespace DataLibrary
{
    public interface DataBase
    {
        public static List<Kons_Test> Just_Themes = new List<Kons_Test>();                                          // List of only themes                      BASES
        public static Dictionary<string, Kons_Test> ALL = new Dictionary<string, Kons_Test>();                      // List of themes from it's names           BASES
        public static Dictionary<string, int> Indexes = new Dictionary<string, int>();                              // List of numbers of topics                BASES

        /**********************************************************/
        /**********************************************************/

        public static Kons_Test Create_and_add_new_theme(string topic)                                              // Method that create a new theme,add it to BASES and return it
        {                                                                                                           // But if it already exist just return it from BASES
            try
            {
                Kons_Test a = new Kons_Test(topic);
                ALL.Add(topic, a);
                Just_Themes.Add(a);
                Indexes.Add(topic, Just_Themes.IndexOf(a));
                a.Number = Indexes[topic];
                return a;
            }
            catch (ArgumentException)                                                                               // Exception for situation in what theme already exist
            {
                return ALL[topic];
            }
        }

        public static Kons_Test Create_and_add_new_theme(Kons_Test a)                                               // Method that create a new theme,add it to BASES and return it
        {                                                                                                           // But if it already exist just return it from BASES
            try
            {
                ALL.Add(a.Name, a);
                Just_Themes.Add(a);
                Indexes.Add(a.Name, Just_Themes.IndexOf(a));
                a.Number = Indexes[a.Name];
                return a;
            }
            catch (ArgumentException)                                                                               // Exception for situation in what theme already exist
            {
                return ALL[a.Name];
            }
        }

        /**********************************************************/
        /**********************************************************/

        public static void Add_O_Comm(Kons_Test top, Kons_Test down)                                                // Add TOP theme to DOWN.TopTheme.
        {                                                                                                           // And add DOWN to TOP.O-Comm list
            if (Just_Themes.Contains(down))
            {                                                                                                       // But if DOWN already contains in TOP.O-Comm list do nothing
                if (!top.O_COMM_TOPICS.Contains(down))
                {
                    top.O_COMM_TOPICS.Add(down);
                    down.TopTopic = top;
                }
            }
        }
        public static void Add_O_Comm(string top, string down)                                                      // This is the same as the previous method
        {                                                                                                           // But for STRING
            if (ALL.ContainsKey(down))
            {
                if (!ALL[top].O_COMM_TOPICS.Contains(ALL[down]))
                {
                    ALL[top].O_COMM_TOPICS.Add(ALL[down]);
                    ALL[down].TopTopic = ALL[top];
                }
            }
        }


        public static void Add_B_Comm(Kons_Test top, Kons_Test down)                                                // Add TOP to DOWN.B-Comm list 
        {                                                                                                           // But if TOP already contains in DOWN.B-Comm list do nothing
            if (Just_Themes.Contains(top))
            {
                if (!down.B_COMM_TOPICS.Contains(top))
                {
                    down.B_COMM_TOPICS.Add(top);
                }
            }
        }
        public static void Add_B_Comm(string top, string down)                                                      // This is the same as the previous method
        {                                                                                                           // But for STRING
            if (ALL.ContainsKey(top))
            {
                if (!ALL[down].B_COMM_TOPICS.Contains(ALL[top]))
                {
                    ALL[down].B_COMM_TOPICS.Add(ALL[top]);
                }
            }
        }

        /**********************************************************/
        /**********************************************************/

        public static void Read_file_and_add(string path)                                                           // Create new Konspect from params in file and add  it to BASES
        {
            StreamReader file = new StreamReader(path);                                                             // Reading file
            string name = file.ReadLine();                                                                          // Name of Topic
            string top = file.ReadLine();                                                                           // Name of Top Topic
            string[] o_comm = file.ReadLine().Split(new char[] { '+' });                                            // Array of O-Comm topics
            string[] b_comm = file.ReadLine().Split(new char[] { '+' });                                            // Array of B-Comm topics
            Kons_Test a = new Kons_Test(                                                                            // Create new topic
                name,
                top,
                o_comm,
                b_comm                                                                                               
                );
            a.Material = file.ReadToEnd();
            Just_Themes.Add(a);                                                                                     // Add new topic to bASES
            ALL.Add(name, a);
            Indexes.Add(name, Just_Themes.IndexOf(a));
            a.Number = Just_Themes.IndexOf(a);
            file.Close();
            //return a;
        }











        public static void Start(string path)                                                                       // Star the DataBase
        {
            Create_and_add_new_theme("ALL");
            StreamReader file = new StreamReader(path);
            while ((path = file.ReadLine()) != null)
            {
                Read_file_and_add(path);
            }
            file.Close();
        }


        public static void Update()                                                                                 // Update all themes
        {
            for(int i=1; i < Just_Themes.Count; i++)                                                                // Iterate on all topics except the first one, because this is the main topic
            {
                Just_Themes[i].Update();
            }
        }
    }
}
