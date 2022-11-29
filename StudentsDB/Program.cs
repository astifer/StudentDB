using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StudentsDB
{
    
    internal class Program
    {

        public static int[] idlist = new int[] { };
        static void Main(string[] args)
        {
            Console.Title = "Test progrramm laba 2";
            Console.WindowWidth = 120;
            Console.WindowHeight = 30;
            Console.WriteLine("loading...");
            
            

            System.IO.File.WriteAllText("data.txt", "");

            var MyStudent1 = new Student(0)
            {
                Name = "Artem",
                Insitute = "Misis",
                Group = "bivt-22-8",
                AvgMark = 4.5f,
                Course = 1,
                Birthday = new DateTime(2004, 4, 6, 10, 10, 10),

            };
            MyStudent1.create = DateTime.Now;
            var Student1JSON = JsonConvert.SerializeObject(MyStudent1)+"\n";
            File.AppendAllText("data.txt", Student1JSON);
            idlist = idlist.Concat(new int[] { 0 }).ToArray();

            System.Threading.Thread.Sleep(1000);

            var MyStudent2 = new Student(32)
            {
                Name = "Artem",
                Insitute = "Misis",
                Group = "bivt-22-2",
                AvgMark = 4.5f,
                Course = 1,
                Birthday = new DateTime(2004, 12, 3, 10, 10, 10),

            };
            MyStudent2.create = DateTime.Now;
            var Student2JSON = JsonConvert.SerializeObject(MyStudent2)+"\n";
            File.AppendAllText("data.txt", Student2JSON);
            idlist = idlist.Concat(new int[] { 32 }).ToArray();

            var MyStudent3 = new Student(78)
            {
                Name = "Igor",
                Insitute = "Misis",
                Group = "bivt-20-8",
                AvgMark = 4.2f,
                Course = 3,
                Birthday = new DateTime(2002, 4, 6, 10, 10, 10),

            };
            MyStudent3.create = DateTime.Now;
            var Student3JSON = JsonConvert.SerializeObject(MyStudent3) + "\n";
            File.AppendAllText("data.txt", Student3JSON);
            idlist = idlist.Concat(new int[] { 78 }).ToArray();

            Menu m = new Menu();
            m.Run();

            
        }
        public static void AddPerson()
        {
            Random rand = new Random();
            var newStudent = new Student(232)
            {
                Name = null,
                Insitute = null,
                Group = null,
                AvgMark = float.NaN,
                Course = 0,
                Birthday = DateTime.Now,

            };
            Console.SetCursorPosition(0,0);
            Console.WriteLine("Adding person in data");
            
            newStudent.SetAll();

            var StudentJSON = JsonConvert.SerializeObject(newStudent) + "\n";
            File.AppendAllText("data.txt", StudentJSON);
            idlist = idlist.Concat(new int[] { newStudent._Id }).ToArray();



        }
        public static void FindData()
        {
            there:
            var ArrayData = File.ReadAllLines("data.txt");
            
            Console.SetCursorPosition(0,0);
            Console.WriteLine("You can exit(exit) or find recorded data with tag: birthday, id, name ");

            string type = "";
            Console.CursorVisible = true;

            while (type!="id" && type != "name" && type!="exit" && type!="birthday")
            {
                Console.SetCursorPosition(0, 1);
                type = Console.ReadLine().ToLower();
                Menu.CleanSt(1);
            }
            switch (type)
            {
                case "id":
                    CheckId(ArrayData);
                    goto there;
                    
                case "birthday":
                    CheckBirth(ArrayData);
                    goto there;

                case "name":
                    CheckName(ArrayData);
                    goto there;

                case "exit":
                    goto default;

                default:
                    break;
            }
            

        }
        static void CheckBirth(string[] ArrayData)
        {
            string[] birthdays = new string[ArrayData.Length];
            for (int i = 0; i < ArrayData.Length; i++)
            {
                var st = JsonConvert.DeserializeObject<Student>(ArrayData[i]);
                birthdays[i] = st.Birthday.ToString().Split(' ')[0];
            }

        labl:
            
            Menu.CleanSt(0);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Enter birth day in format <DD.MM.YYYY> ");

            Menu.CleanSt(1);
            Console.CursorVisible = true;
            Console.SetCursorPosition(0, 1);
            
            string input = Console.ReadLine();

            if (input.Length != 10) 
            { 
                 
                Menu.CleanSt(0);
                Menu.CleanSt(1);
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Not correct format"); 
                System.Threading.Thread.Sleep(2000); 
                Menu.CleanSt(1); 
                goto labl; 
            };
            bool flag = true;

            for (int j = 0; j < birthdays.Length; j++)
            {
                if (birthdays[j] == input)
                {
                    flag = false;
                    GetInfo(ArrayData[j]);
                }
            }
            if (flag)
            {
                Menu.CleanSt(0);
                Menu.CleanSt(1);
                Console.SetCursorPosition(0,0);
                Console.WriteLine("have not found");
                System.Threading.Thread.Sleep(2000);
                Menu.CleanSt(1);
            }
        }
        static void CheckId(string[] ArrayData)
        {
            string[] ids = new string[ArrayData.Length];
            for (int i = 0; i < ArrayData.Length; i++)
            {
                var st = JsonConvert.DeserializeObject<Student>(ArrayData[i]);
                ids[i] = st._Id.ToString();
            }
            Menu.CleanSt(0);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Enter id");

            Menu.CleanSt(1);
            Console.SetCursorPosition(0, 1);
            Console.CursorVisible = true;
            string input = Console.ReadLine();

            bool flag = true;

            for (int j = 0; j < ids.Length; j++)
            {
                if (ids[j] == input)
                {
                    flag = false;
                    GetInfo(ArrayData[j]);
                }
            }
            if (flag)
            {
                Menu.CleanSt(0);
                Menu.CleanSt(1);
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("have not found");
                System.Threading.Thread.Sleep(2000);    
            }
        }
        static void CheckName(string[] ArrayData)
        {
            string[] names = new string[ArrayData.Length];
            for (int i = 0; i < ArrayData.Length; i++)
            {
                var st = JsonConvert.DeserializeObject<Student>(ArrayData[i]);
                names[i] = st.Name;
            }
            Menu.CleanSt(0);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Enter name");

            Menu.CleanSt(1);
            Console.SetCursorPosition(0, 1);
            Console.CursorVisible = true;
            string input = Console.ReadLine();

            bool flag = true;

            for (int j = 0; j < names.Length; j++)
            {
                if (names[j].ToLower() == input)
                {
                    flag = false;
                    GetInfo(ArrayData[j]);
                }
            }
            if (flag)
            {
                Menu.CleanSt(0);
                Menu.CleanSt(1);
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("have not found");
                System.Threading.Thread.Sleep(2000);
            }
        }
        static void GetInfo(string jsonDict)
        {
            var st = JsonConvert.DeserializeObject<Student>(jsonDict);

            bool exit = false;
            ConsoleKeyInfo key;

            while (!exit)
            {

                Menu.CleanSt(0);
                Menu.CleanSt(1);
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Work with student " +
                    " >> S - show all data about it," +
                    " E - exit," +
                    " D - delete student from data base <<");

                Menu.CleanSt(1);
                Console.SetCursorPosition(0, 1);
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.S:
                        st.Show(2);
                        break;
                    case ConsoleKey.E:
                        Menu.FonBlack();
                        exit = true;
                        break;
                    case ConsoleKey.D:
                        Menu.CleanSt(0);
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Are you sure?(y or n)");
                        string inp="";
                        while (inp !="y" && inp != "n")
                        {
                            Menu.CleanSt(1);
                            Console.SetCursorPosition(0, 1);
                            Console.CursorVisible = true;
                            inp = Console.ReadLine();
                        }
                        if (inp == "y")
                        {
                            Delt(st._Id);
                            exit = true;
                            FindData();
                        }
                        break;
                    default:
                        break;

                }
            }
            
        }
        public static void SortByMarks()
        {
            var ArrayData = File.ReadAllLines("data.txt");
            float[,] marks = new float[ArrayData.Length, 2];

            Dictionary<int, string> people = new Dictionary<int, string>();
            
            for (int i = 0; i < ArrayData.Length; i++)
            {
                var st = JsonConvert.DeserializeObject<Student>(ArrayData[i]);
                people[st._Id] = ArrayData[i];
                marks[i, 0] = st.AvgMark;
                marks[i, 1] = st._Id;

            }
            for (int i = 0; i < marks.GetLength(0); i++)
            {
                for (int j = 0; j < marks.GetLength(0)-1; j++)
                {
                    if (marks[j, 0] < marks[j+1, 0])
                    {
                        
                        float m = marks[j, 0];
                        float d = marks[j, 1];

                        marks[j, 0] = marks[j+1,0];
                        marks[j, 1] = marks[j+1,1];

                        marks[j+1, 0] = m;
                        marks[j+1, 1] = d;

                    }
                }
            }
            Menu.FonBlack();
            for (int i = 0; i < marks.GetLength(0); i++)    
            {
                int ident = (int)marks[i, 1];

                for (int j =0; j<ArrayData.Length; j++)
                {
                    var st = JsonConvert.DeserializeObject<Student>(ArrayData[j]);
                    if (st._Id == ident)
                    {
                        Menu.FonBlack();
                        Console.SetCursorPosition(0, 1);
                        Console.WriteLine($"Place {i+1}");
                        st.Show(3); 
                        Menu.CleanSt(0);
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Tap any key to continue cheking");
                        Console.CursorVisible = false;
                        Console.ReadKey();
                    }
                }
            }
            Menu.FonBlack();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("That's all");
            System.Threading.Thread.Sleep(2000);
            Menu.CleanSt(0);

                   
        }
        static void Delt(int id)
        {
            var ArrayData = File.ReadAllLines("data.txt");
            File.WriteAllText("data.txt", "");

            for (int i = 0; i < ArrayData.Length; i++)
            { 
                var st = JsonConvert.DeserializeObject<Student>(ArrayData[i]);
                if (st._Id != id)
                {
                    var StudentJSON = JsonConvert.SerializeObject(st) + "\n";
                    File.AppendAllText("data.txt", StudentJSON);
                }
            }
            Menu.FonBlack();
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("OK");
            System.Threading.Thread.Sleep(2000);
            Menu.CleanSt(1);
            Menu.CleanSt(0);
            
        }
        public static void Protocol35() 
        {
            var ArrayData = File.ReadAllLines("data.txt");

            /// [0]:AVGmark, [1]:ID
            float[,] marks = new float[ArrayData.Length, 2];

            /// key: iD, value: JSONDICT
            Dictionary<int, string> people = new Dictionary<int, string>();

            for (int i = 0; i < ArrayData.Length; i++)
            {
                var st = JsonConvert.DeserializeObject<Student>(ArrayData[i]);
                people[st._Id] = ArrayData[i];
                marks[i, 0] = st.AvgMark;
                marks[i, 1] = st._Id;

            }
            for (int i = 0; i < marks.GetLength(0); i++)
            {
                for (int j = 0; j < marks.GetLength(0) - 1; j++)
                {
                    if (marks[j, 0] < marks[j + 1, 0])
                    {

                        float m = marks[j, 0];
                        float d = marks[j, 1];

                        marks[j, 0] = marks[j + 1, 0];
                        marks[j, 1] = marks[j + 1, 1];

                        marks[j + 1, 0] = m;
                        marks[j + 1, 1] = d;

                    }
                }
            }
            Menu.CleanSt(0);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Protocol 3.5. Choose type of work(min or max)");
            string type = "";
            while (type!="min" && type != "max")
            {
                Menu.CleanSt(1);
                Console.SetCursorPosition(0, 1);
                type = Console.ReadLine();
            }

            if (type == "min")
            {
                Menu.CleanSt(0);
                Menu.CleanSt(1);
                Console.SetCursorPosition(0, 0);
                
                Console.WriteLine($"Finding minimal marks. Miniamal value: {marks[marks.GetLength(0) - 1, 0]}");
                int i = marks.GetLength(0);
                do
                {
                    i--;
                    Menu.CleanSt(1);
                    var st = JsonConvert.DeserializeObject<Student>(ArrayData[i]);
                    Console.SetCursorPosition(0, 1);
                    Console.WriteLine($"Student {st.Name} with id: {st._Id} have the worth mark: {st.AvgMark}");

                    Console.WriteLine("tap to continue...");
                    Console.ReadKey();
                    Menu.CleanSt(2);

                    if (i < 0 || i >= marks.Length)
                    {
                        break;
                    }
                } while (marks[i, 0] == marks[i-1, 0]); 
            }
            else
            {
                Menu.CleanSt(0);
                Menu.CleanSt(1);
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"Finding maximal marks. Maximal value: {marks[0, 0]}");
                int i = -1;
                do
                {
                    i++;
                    Menu.CleanSt(1);
                    var st = JsonConvert.DeserializeObject<Student>(ArrayData[i]);
                    Console.SetCursorPosition(0, 1);
                    Console.WriteLine($"Student {st.Name} with id: {st._Id} have the best mark: {st.AvgMark}");

                    Console.WriteLine("tap to continue...");
                    Console.ReadKey();
                    Menu.CleanSt(2);

                    if(i<0 || i >= marks.Length)
                    {
                        break;
                    }
                } while (marks[i, 0] == marks[i + 1, 0]);
            }
            Menu.CleanSt(0);
            Menu.CleanSt(1);
            Menu.CleanSt(2);
        }
        public static void DelByName()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Process...");
            int cnt = 0;
            
           
            var ArrayData = File.ReadAllLines("data.txt");
            string[,] names = new string[ArrayData.Length, 2] ;

            
            for (int i=0; i<ArrayData.Length; i++)
            {
                var st = JsonConvert.DeserializeObject<Student>(ArrayData[i]);
                names[i, 0] = st.Name;
                names[i, 1] = st._Id.ToString();
            }

            for (int i = 0; i<names.GetLength(0); i++)
            {
                for (int j = i+1; j<names.GetLength(0); j++)
                {
                    if (names[i, 0].ToLower() == names[j, 0].ToLower())
                    {
                        int id1 = int.Parse(names[i, 1]);
                        int id2 = int.Parse(names[j, 1]);

                        var st1 = JsonConvert.DeserializeObject<Student>(ArrayData[i]);
                        var st2 = JsonConvert.DeserializeObject<Student>(ArrayData[j]);

                        if (st1.create > st2.create)
                        {
                            Delt(id2);
                            cnt++;
                        }
                        else { Delt(id1); cnt++; }
                    }
                }
                
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"OK. Deleted {cnt} examples");
            System.Threading.Thread.Sleep(2000);
            Menu.CleanSt(0);

        } 

        public static void FindSimMarks()
        {
            var ArrayData = File.ReadAllLines("data.txt");

            /// [0]:AVGmark, [1]:ID
            float[,] marks = new float[ArrayData.Length, 2];
            Dictionary<int, string> people = new Dictionary<int, string>();

            for (int i = 0; i < ArrayData.Length; i++)
            {
                var st = JsonConvert.DeserializeObject<Student>(ArrayData[i]);
                people[st._Id] = ArrayData[i];
                marks[i, 0] = st.AvgMark;
                marks[i, 1] = st._Id;

            }
            Console.CursorVisible = false;
            for (int i = 0; i<ArrayData.GetLength(0); i++)
            {
                for (int j=i+1; j<ArrayData.GetLength(0); j++)
                {
                    if (marks[i, 0] == marks[j, 0])
                    {
                        var st1 = JsonConvert.DeserializeObject<Student>(ArrayData[i]);
                        var st2 = JsonConvert.DeserializeObject<Student>(ArrayData[j]);
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine($"Students {st1.Name}(id={st1._Id}) and {st2.Name}(id={st2._Id}) have similar mark: {marks[i,0]}");
                        Console.SetCursorPosition(0, 1);
                        Console.WriteLine("Tap any key to continue...");
                        Console.ReadKey();
                        Menu.CleanSt(0);
                        Menu.CleanSt(1);
                    }
                }
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Thats all");
            System.Threading.Thread.Sleep(2000);
            Menu.CleanSt(0);

        }
    }
    public class Student
    {
        public int _Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Insitute { get; set; }
        public string Group { get; set; }
        public int Course { get; set; }
        public float AvgMark { get; set; }
        public DateTime create { get; set; }

        public Student(int id)
        {
            _Id = id;
        }
        public void Show(int y)
        {
            Console.SetCursorPosition(0, y);
            Console.WriteLine("ID: "+this._Id);
            Console.WriteLine("NAME: "+this.Name);
            Console.WriteLine("BIRTHDAY: " + this.Birthday.ToString().Split(' ')[0]);
            Console.WriteLine("INSITUTE: " + this.Insitute);
            Console.WriteLine("GROUP: " + this.Group);
            Console.WriteLine("COURSE: " + this.Course);
            Console.WriteLine("AVERAGE MARK: " + this.AvgMark);
            Console.WriteLine("CREATING: " + this.create);
            Console.CursorVisible = false;

        }

        public void SetAll()
        {
            Menu.CleanSt(1);
            Menu.CleanSt(2);
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("Enter NAME: ");
            
            this.Name = Console.ReadLine();

            bool flag = false;
            while (!flag)
            {
            //Birthday
                e:
                flag = false;
                try
                {
                agday:
                    Menu.CleanSt(1);
                    Menu.CleanSt(2);
                    Console.SetCursorPosition(0, 1);
                    Console.WriteLine("Enter day of birthday(DD): ");

                    var inp = Console.ReadLine();
                    int d = int.Parse(inp);
                    if (inp.Length != 2 || d <= 0 || d>31) { goto agday; }
                    
                    agmon:
                    Menu.CleanSt(1);
                    Menu.CleanSt(2);
                    Console.SetCursorPosition(0, 1);
                    Console.WriteLine("Enter month of birthday(MM): ");
                    
                    inp = Console.ReadLine();
                    int m = int.Parse(inp);
                    if (inp.Length != 2 || m<=0 || m>=13) { goto agmon; }
                    
                    agyear:
                    Menu.CleanSt(1);
                    Menu.CleanSt(2);
                    Console.SetCursorPosition(0, 1);
                    Console.WriteLine("Enter year of birthday(YYYY): ");
                    
                    inp = Console.ReadLine();
                    int y = int.Parse(inp);
                    if (inp.Length != 4 || y>2022 || y<1900) { goto agyear; }
                    

                    this.Birthday = new DateTime(y, m, d, 10, 10, 10);


                }
                catch { goto e; };

                flag = true;
                
            }

            Menu.CleanSt(1);
            Menu.CleanSt(2);
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("Enter INSTITUTE: ");
            
            this.Insitute = Console.ReadLine();

            Menu.CleanSt(1);
            Menu.CleanSt(2);
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("Enter GROUP: ");
            
            this.Group = Console.ReadLine();

            agcour:
            try
            {
                
                Menu.CleanSt(1);
                Menu.CleanSt(2);
                Console.SetCursorPosition(0, 1);
                Console.WriteLine("Enter COURSE: ");
                int c = int.Parse(Console.ReadLine());
                this.Course = c;
            } catch { goto agcour; }

            agmark:
            try
            {

                Menu.CleanSt(1);
                Menu.CleanSt(2);
                Console.SetCursorPosition(0, 1);
                Console.WriteLine("Enter AVERAGE MARK: ");
                float c = float.Parse(Console.ReadLine());
                this.AvgMark = c;
            }
            catch { goto agmark; }
            Menu.CleanSt(0);
            Menu.CleanSt(1);
            Menu.CleanSt(2);

            Random rand = new Random();
            int id = rand.Next(100);
            flag = false;
            while (!flag)
            {
                flag = true;
                foreach(int i in Program.idlist)
                {
                    if (i == id)
                    {
                        flag = false;
                    }
                }
            }
            this._Id = id;
            Program.idlist = Program.idlist.Concat(new int[] { id }).ToArray();

            Menu.CleanSt(0);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("OK");

            Menu.CleanSt(1);
            Console.SetCursorPosition(0, 1);
            Console.WriteLine($"ID: {this._Id}. Tap any key to continue...");
            Console.CursorVisible = false;
            Console.ReadKey();
            this.create = DateTime.Now;
            Menu.CleanSt(0);
            Menu.CleanSt(1);
            Menu.CleanSt(2);

        }
    }
    public class Menu
    {
        public void Run()
        {

            ConsoleKeyInfo key;
            var Exit = false;

            while (!Exit)
            {
                Console.ResetColor();
                CleanSt(0);
                CleanSt(Console.WindowHeight - 2);
                Console.SetCursorPosition(0, Console.WindowHeight - 2);
                Console.WriteLine("W-work with data, C - clear area, A - add personin data, M - sort by marks, esc - exit from app");

                CleanSt(Console.WindowHeight - 1);
                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                Console.WriteLine("G - protocol 3.5, N - Delete similar(based on name), V - show students with similar marks");

                Console.SetCursorPosition(0, 0);
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        Exit = true;
                        break;
                    case ConsoleKey.C:
                        FonBlack();
                        break;
                    case ConsoleKey.W:
                        Program.FindData();
                        FonBlack();                        
                        break;
                    case ConsoleKey.M:
                        FonBlack();
                        Program.SortByMarks();
                        break;
                    case ConsoleKey.A:
                        Program.AddPerson();
                        break;
                    case ConsoleKey.G:
                        Program.Protocol35();
                        break;
                    case ConsoleKey.N:
                        Program.DelByName();
                        break;
                    case ConsoleKey.V:
                        Program.FindSimMarks();
                        break;
                    default:
                        break;
                }
            }
        }
        static public void FonBlack()
        {

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;
            for (var y = 0; y < Console.WindowHeight; y++)
            {
                for (var x = 0; x < Console.WindowWidth; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write("#");
                }
            }
            Console.CursorVisible = true;
            Console.ResetColor();
        }
        static public void CleanSt(int i)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;
            for(var x = 0; x < Console.WindowWidth; x++)
            {
                Console.SetCursorPosition(x, i);
                Console.Write("#");
            }
            Console.CursorVisible = true;
            Console.ResetColor();
        }
    }
}