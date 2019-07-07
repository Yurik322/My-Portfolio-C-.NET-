//
// Created by yurik_322 on 09/05/19.
//
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


#region MyRegion
//      !!!аудиторії (а, б, хор., фіз., муз.)!!! аудиторія клас - 1 , якщо фіз або хор, то інші кабінети! 
#endregion

// я склав розклад так, щоб предмети не повторювалися в аудиторіях, якщо цей предмет є в одного класу, то в іншого він не може бути.
// (1):
//  (від c.113 - (124))
namespace Lab6AI
{
        // (2):
    // Розробити структуру хромосоми(структуру розкладу) для розв’язання задачі оптимізації:
    struct Teacher
    {
        private static int id = 0;

        int _iD;
        string _name;
        string _subjects;

        //public int ID
        //{
        //    get
        //    {
        //        return _iD;
        //    }
        //}
        // це те саме:
        public int ID => _iD;


        public string Subjects
        {
            get { return _subjects; }
            set { _subjects = value; }
        }


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        public Teacher(string Name, string Subjects, bool incrementID = true)
        {
            _iD = id;
            _name = Name;
            _subjects = Subjects;

            if (incrementID)
                id++;
        }


        public override string ToString()
        {
            return ID + " " + _subjects + " " + _name;
        }
    }



    static class Program
    {
        // Реалізація розкладу:
        static void PrintPlan(Teacher[] teachers, Plan plan)
        {
            Console.ForegroundColor = ConsoleColor.Green; // Color
            Console.Write("\t\t\t\t\t\t\t\t(ГЕНЕТИЧНИЙ АЛГОРИТМ)\n");
            Console.Write("\n\t\t\t\t\t\t\tРОЗКЛАД ЗАНЯТЬ ДЛЯ ПОЧАТКОВОЇ ШКОЛИ:\n");
            Console.ResetColor();   // Color
            #region MyRegion
            //var sb = new StringBuilder();
            //for (byte day = 0; day < Plan.DaysPerWeek; day++)
            //{
            //    sb.AppendFormat("Day {0}\r\n", day);
            //    for (byte hour = 0; hour < Plan.HoursPerDay; hour++)
            //    {
            //        sb.AppendFormat("Hour {0}: ", hour);
            //        foreach (var p in plan.HourPlans[day, hour].GroupToTeacher)
            //            sb.AppendFormat("       Група-Вчитель: {0}-{1} ", p.Key, p.Value);
            //        sb.AppendLine();
            //    }
            //} 
            #endregion
            for (byte day = 0; day < Plan.DaysPerWeek; day++)
            {


                Console.Write("День {0}\r\n", day+1);

                for (byte hour = 0; hour < Plan.HoursPerDay; hour++)
                {
                    Console.Write(" Година {0}: ", hour+1);
                    // для переліку колекції:
                    foreach (var p in plan.HourPlans[day, hour].GroupToTeacher)
                    {
                        Console.Write("\t\tГрупа-Вчитель-Урок: {0}  -  {1}  -  {2} ", p.Key, teachers.Where(id => id.ID == p.Value).First().Name, teachers.Where(id => id.ID == p.Value).First().Subjects);
                    }
                    Console.WriteLine();
                }
            }

            // Fitness- здоров"я популяції!
            // Console.Write("Fitness: {0}\r\n", plan.FitnessValue);
        }


        // Читання з файлу:
        static List<string> ReadFromFile(string path)
        {
            try
            {
                List<string> list = File.ReadAllLines(path).ToList();
                #region MyRegion
                // Читає весь файл;
                // розбиває по містях; 
                // розбиває з string в int і перетворюйте IEnumerable у список 
                #endregion
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in " + ex.TargetSite.Name + "Error message: " + ex.Message);
            }

            return null;
        }


        // Перетворює строкове представлення числа в еквівалентне йому 32-бітове ціле число зі знаком. (string->int)
        // Повертає значення, яке вказує, чи успішно виконана операція.
        static bool TryParse(List<string> list, ref int[] groups, int IDGroup, ref Teacher[] teachers)
        {
            char[] temp1 = new char[256];
            char[] temp2 = new char[256];
            char[] temp3 = new char[256];
            var teachersBuf = new List<Teacher>();

            var groupsBuf = new List<int>();

            try
            {
                for (int i = 0; i < list.Count; ++i)
                {
                    list[i].CopyTo(list[i].IndexOf("{") + 1, temp1, 0, list[i].IndexOf("}") - 1);
                    list[i] = list[i].Remove(list[i].IndexOf("{"), list[i].IndexOf("}") + 1);

                    list[i].CopyTo(list[i].IndexOf("{") + 1, temp2, 0, list[i].IndexOf("}") - 2);
                    list[i] = list[i].Remove(list[i].IndexOf("{"), list[i].IndexOf("}") + 1);

                    list[i].CopyTo(list[i].IndexOf("{") + 1, temp3, 0, list[i].IndexOf("}") - 2);


                    #region MyRegion
                    //Console.WriteLine(new string(temp3).Trim()); 
                    #endregion
                    for (int j = 0; j < Int32.Parse(new string(temp3).Trim()); ++j)
                    {
                        if (j + 1 == Int32.Parse(new string(temp3).Trim()))
                            teachersBuf.Add(new Teacher(new string(temp2).TrimEnd('\0'), new string(temp1).TrimEnd('\0')));
                        else
                            teachersBuf.Add(new Teacher(new string(temp2).TrimEnd('\0'), new string(temp1).TrimEnd('\0'), false));
                        groupsBuf.Add(IDGroup);
                    }
                    Array.Clear(temp1, 0, temp1.Length - 1);
                    Array.Clear(temp2, 0, temp1.Length - 1);
                    Array.Clear(temp3, 0, temp1.Length - 1);
                }

                // Вчителі:
                teachers = teachers.Concat(teachersBuf.ToArray()).ToArray();
                // Групи:
                groups = groups.Concat(groupsBuf.ToArray()).ToArray();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }



        static void Main()
        {
            int[] groups = new int[0];
            Teacher[] teachers = new Teacher[0];
            // Зчитування з файлів:
            TryParse( ReadFromFile("Class1.txt"), ref groups, 1, ref teachers );
            TryParse( ReadFromFile("Class2.txt"), ref groups, 2, ref teachers );

            var list = new List<Lessоn>();

            for (int i = 0; i < groups.Length; i++)
            {
                list.Add(new Lessоn(groups[i], teachers[i].ID));
            }

            // створюємо вирішувач:
            var solver = new Solver();

            Plan.DaysPerWeek = 5;
            Plan.HoursPerDay = 5;

            // будемо штрафувати за вікна:
            solver.FitnessFunctions.Add(FitnessFunctions.Windows);
            
            // будемo штрафувати за пізні уроки:
            solver.FitnessFunctions.Add(FitnessFunctions.LateLesson);
            
            // будемo штрафувати за однакові предмети в день:
            solver.FitnessFunctions.Add(FitnessFunctions.SameSubject);


            // знаходимо найкращий план( краще рішення):
            var res = solver.Solve(list);

            PrintPlan(teachers, res);
            Console.ReadLine();
        }
    }


    /// <summary>
    /// Функції здоров"я - введу їх вище
    /// </summary>
    static class FitnessFunctions
    {
        // штраф за вікно у групи:
        public static int GroupWindowPenalty = 10;
        
        // штраф за вікно у викладача:
        public static int TeacherWindowPenalty = 12;
        
        // штраф за пізний урок:
        public static int LateLessonPenalty = 1;
        
        // штраф за 2 і більше однакових уроків в день:
        public static int ManySameSubjectPenalty = 5;

        // максимальний урок, коли вигідно проводити уроки:
        public static int LatesetHour = 5;


        /// <summary>
        /// Штраф за вікна
        /// </summary>
        public static int Windows(Plan plan)
        {
            var res = 0;

            for (byte day = 0; day < Plan.DaysPerWeek; day++)
            {
                var groupHasLessions = new HashSet<int>();
                var teacherHasLessions = new HashSet<int>();

                for (byte hour = 0; hour < Plan.HoursPerDay; hour++)
                {
                    foreach (var pair in plan.HourPlans[day, hour].GroupToTeacher)
                    {

                        var group = pair.Key;
                        var teacher = pair.Value;
                        if (groupHasLessions.Contains(group)  &&  !plan.HourPlans[day, hour - 1].GroupToTeacher.ContainsKey(group))
                        {
                            res += GroupWindowPenalty;
                        }
                        if (teacherHasLessions.Contains(teacher)  &&  !plan.HourPlans[day, hour - 1].TeacherToGroup.ContainsKey(teacher))
                        {
                            res += TeacherWindowPenalty;
                        }

                        groupHasLessions.Add(group);
                        teacherHasLessions.Add(teacher);
                    }
                }
            }

            return res;
        }


        /// <summary>
        /// Штраф за пізні уроки
        /// </summary>
        public static int LateLesson(Plan plan)
        {
            var res = 0;
            // по суті робота з колекцією:
            foreach (var pair in plan.GetLessons())
                if (pair.Hour > LatesetHour)
                { 
                    res += LateLessonPenalty;
                }
            return res;
        }

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// <summary>
        /// Штраф за однакові предмети на день!
        /// </summary>
        public static int SameSubject(Plan plan)
        {
            var res = 0;

            for (int i = 0; i < plan.HourPlans.GetLength(0); ++i)
            {
                var countLessonPerDay = new Dictionary<int, int>();
                for (int j = 0; j < plan.HourPlans.GetLength(1); ++j)
                { 
                    for (int k = 0; k < plan.HourPlans[i, j].TeacherToGroup.Count; ++k)
                    {
                        int value;

                        if (!countLessonPerDay.TryGetValue(plan.HourPlans[i, j].TeacherToGroup.Keys.ElementAt(k), out value))
                        {
                            countLessonPerDay[plan.HourPlans[i, j].TeacherToGroup.Keys.ElementAt(k)] = 1;
                        }
                        else
                        { 
                            countLessonPerDay[plan.HourPlans[i, j].TeacherToGroup.Keys.ElementAt(k)]++;
                        }
                    }
                }
                foreach (KeyValuePair<int, int> kvp in countLessonPerDay)
                {
                    if (kvp.Value >= 1)//2
                    { 
                        res += ManySameSubjectPenalty * (kvp.Value - 1);
                    }
                }
            }

            return res;
        }
    }



        // (7):
    // Безпосередньо реалізувати генетичний алгоритм оптимізації розкладу занять згідно завдання:
    /// <summary>
    /// Вирішувач (безпосередньо генетичний алгоритм)
    /// </summary>
    class Solver
    {
        public int MaxIterations = 1000;
        
        // має ділитись на 4:
        public int PopulationCount = 100;


            // (3):
        // Реалізувати допоміжну функцію для обчислення значення цільової функції:
        // FitnessFunctions- ф-ції здоров"я
        public List<Func<Plan, int>> FitnessFunctions = new List<Func<Plan, int>>();


        // Fitness- здоров"я
        public int Fitness(Plan plan)
        {
            var res = 0;

            foreach (var f in FitnessFunctions)
            { 
                res += f(plan);
            }
            return res;
        }


        public Plan Solve(List<Lessоn> pairs)
        {
            // створюємо популяцію:
            var pop = new Population(pairs, PopulationCount);
            if (pop.Count == 0)
                throw new Exception("Can not create any plan");
            
            var count = MaxIterations;
            while (count-- > 0)
            {
                // рахуємо функцію здоров"я для всіх планів:
                pop.ForEach(p => p.FitnessValue = Fitness(p));

                // сортуємо популяцію по функції здоров"я:
                pop.Sort((p1, p2) => p1.FitnessValue.CompareTo(p2.FitnessValue));
                
                // чи найшовся ідеальний план?
                if (pop[0].FitnessValue == 0)
                    return pop[0];
                
                // відбираємо 25% найкращих планів:
                pop.RemoveRange(pop.Count / 4, pop.Count - pop.Count / 4);
                
                    
                // від кожного створюємо 3 нащадки з мутаціями:
                var c = pop.Count;
                for (int i = 0; i < c; i++)
                {
                    pop.AddChildOfParent(pop[i]);
                    pop.AddChildOfParent(pop[i]);
                    pop.AddChildOfParent(pop[i]);
                }
            }

            // рахуємо функцію здоров"я для всіх планів:
            pop.ForEach(p => p.FitnessValue = Fitness(p));

            // сортуємо популяцію по функції здоров"я:
            pop.Sort((p1, p2) => p1.FitnessValue.CompareTo(p2.FitnessValue));

            // повертаємо найкращий план:
            return pop[0];
        }
    }




    /// <summary>
    /// Популяція планів
    /// </summary>
    class Population : List<Plan>
    {
            // (4):
        // Реалізувати допоміжну функцію ініціалізації початкової «популяції»:
        public Population(List<Lessоn> pairs, int count)
        {
            var maxIterations = count * 2;

            do
            {
                var plan = new Plan();

                if (plan.Init(pairs))
                    Add(plan);

            } while ( maxIterations-- > 0  &&  Count < count );
        }

            
            // (5):
        // Реалізувати допоміжну функцію «мутації»
        public bool AddChildOfParent(Plan parent)
        {
            int maxIterations = 10;

            do
            {
                var plan = new Plan();

                if (plan.Init(parent))
                {
                    Add(plan);
                    return true;
                }

            } while ( maxIterations--  >  0 );

            return false;
        }
    }


        // (6):
    // Реалізувати допоміжні функції одно-точкового та багато-точкового «схрещування» (кросовера):
    /// <summary>
    /// План заняття
    /// </summary>
    class Plan
    {
        // 5 навчальних дня в неділю:
        public static int DaysPerWeek = 5;
  
        // до 5 уроків в день:
        public static int HoursPerDay = 5;

        static Random rnd = new Random();
        //static Random rnd = new Random(3);


        /// <summary>
        /// План по дням (перший індекс) і уроках (другий індекс)
        /// </summary>
        public HourPlan[,] HourPlans = new HourPlan[DaysPerWeek, HoursPerDay];


        // значення функції здоров"я:
        public int FitnessValue { get; internal set; }


        public bool AddLesson(Lessоn les)
        {
            return HourPlans[les.Day, les.Hour].AddLesson(les.Group, les.Teacher);
        }


        public void RemoveLesson(Lessоn les)
        {
            HourPlans[les.Day, les.Hour].RemoveLesson(les.Group, les.Teacher);
        }


            // (6):
        // Реалізувати допоміжні функції одно-точкового та багато-точкового «схрещування» (кросовера):
        /// <summary>
        /// Добавити групу з вчителем на будь-який день і  в будь-який урок
        /// </summary>
        public bool AddToAnyDayAndHour(int group, int teacher)
        {
            int maxIterations = 30;
            do
            {
                var day = (byte)rnd.Next(DaysPerWeek);

                if (AddToAnyHour(day, group, teacher))
                { 
                    return true;
                }

            } while (maxIterations-- > 0);

            // не змогли добавити нікуди:
            return false;
        }


            // (6):
        // Реалізувати допоміжні функції одно-точкового та багато-точкового «схрещування» (кросовера):
        /// <summary>
        /// Добавити групу з вчителем на будь-який урок
        /// </summary>
        bool AddToAnyHour(byte day, int group, int teacher)
        {
            for (byte hour = 0; hour < HoursPerDay; hour++)
            {
                var les = new Lessоn(day, hour, group, teacher);

                if (AddLesson(les))
                { 
                    return true;
                }
            }

            //немає вільних уроків в цей день:
            return false;
        }


            // (6):
        // Реалізувати допоміжні функції одно-точкового та багато-точкового «схрещування» (кросовера):
        /// <summary>
        /// Створення плану за списком пар
        /// </summary>
        public bool Init(List<Lessоn> pairs)
        {
            for (int i = 0; i < HoursPerDay; i++)
                for (int j = 0; j < DaysPerWeek; j++)
                    HourPlans[j, i] = new HourPlan();

            foreach (var p in pairs)

                if (!AddToAnyDayAndHour(p.Group, p.Teacher))
                    return false;

            return true;
        }

        
        /// <summary>
        /// Сворення нащадка з мутацією
        /// </summary>
        public bool Init(Plan parent)
        {
            // копіюємо предка:
            for (int i = 0; i < HoursPerDay; i++)
                for (int j = 0; j < DaysPerWeek; j++)
                    HourPlans[j, i] = parent.HourPlans[j, i].Clone();

            // вибираємо два рандомних дня:
            var day1 = (byte)rnd.Next(DaysPerWeek);
            var day2 = (byte)rnd.Next(DaysPerWeek);

            // знаходимо уроки в ці дні:
            var pairs1 = GetLessonsOfDay(day1).ToList();
            var pairs2 = GetLessonsOfDay(day2).ToList();


            ///!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //вибираємо випадкові уроки
            if (pairs1.Count == 0  ||  pairs2.Count == 0)
            {
                return false;
            }

            var pair1 = pairs1[rnd.Next(pairs1.Count)];
            var pair2 = pairs2[rnd.Next(pairs2.Count)]; 
            //створюємо мутацію - переставляємо випадкові уроки містями
            RemoveLesson(pair1);// видаляю
            RemoveLesson(pair2);// видаляю
            ////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            var res1 = AddToAnyHour(pair2.Day, pair1.Group, pair1.Teacher);//вставляємо у випадкове місце
            var res2 = AddToAnyHour(pair1.Day, pair2.Group, pair2.Teacher);//вставляємо у випадкове місце

            //if ()
            //{
            //    return false;
            //}


            return res1 && res2;
        }


        public IEnumerable<Lessоn> GetLessonsOfDay(byte day)
        {
            for (byte hour = 0; hour < HoursPerDay; hour++)
                foreach (var p in HourPlans[day, hour].GroupToTeacher)
                {
                    yield return new Lessоn(day, hour, p.Key, p.Value);
                }
        }


        public IEnumerable<Lessоn> GetLessons()
        {
            for (byte day = 0; day < DaysPerWeek; day++)
                for (byte hour = 0; hour < HoursPerDay; hour++)
                    foreach (var p in HourPlans[day, hour].GroupToTeacher)
                    {
                        yield return new Lessоn(day, hour, p.Key, p.Value);
                    }
        }


        // Перевантаження в тип стрічка:
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (byte day = 0; day < Plan.DaysPerWeek; day++)
            {
                sb.AppendFormat("Day {0}\r\n", day);
                for (byte hour = 0; hour < Plan.HoursPerDay; hour++)
                {
                    sb.AppendFormat("Година {0}: ", hour);
                    foreach (var p in HourPlans[day, hour].GroupToTeacher)
                        sb.AppendFormat("Група-Вчитель: {0}  -  {1} ", p.Key, p.Value);
                    sb.AppendLine();
                }
            }

            sb.AppendFormat("Fitness: {0}\r\n", FitnessValue);

            return sb.ToString();
        }
    }


    /// <summary>
    /// План на урок
    /// </summary>
    class HourPlan
    {
        /// <summary>
        /// Зберігає пару група-вчитель
        /// </summary>
        public Dictionary<int, int> GroupToTeacher = new Dictionary<int, int>();


        /// <summary>
        /// Зберігає пару вчитель-група
        /// </summary>
        public Dictionary<int, int> TeacherToGroup = new Dictionary<int, int>();


        public bool AddLesson(int group, int teacher)
        {
            if (TeacherToGroup.ContainsKey(teacher)  ||  GroupToTeacher.ContainsKey(group))
            { 
                return false;   // цей урок вже є урок в вчителя або в групи
            }
            GroupToTeacher[group] = teacher;
            TeacherToGroup[teacher] = group;

            return true;
        }


        public void RemoveLesson(int group, int teacher)
        {
            GroupToTeacher.Remove(group);
            TeacherToGroup.Remove(teacher);
        }


        public HourPlan Clone()
        {
            var res = new HourPlan();
            res.GroupToTeacher = new Dictionary<int, int>(GroupToTeacher);
            res.TeacherToGroup = new Dictionary<int, int>(TeacherToGroup);

            return res;
        }
    }



    /// <summary>
    /// Урок
    /// </summary>
    class Lessоn
    {
        public byte Day = 255;
        public byte Hour = 255;
        public int Group;
        public int Teacher;


        public Lessоn(byte day, byte hour, int group, int teacher)
            : this(group, teacher)
        {
            Day = day;
            Hour = hour;
        }


        public Lessоn(int group, int teacher)
        {
            Group = group;
            Teacher = teacher;
        }
    }
}