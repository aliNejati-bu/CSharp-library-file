using cliLearningManagment.Entities;
using cliLearningManagment.Repositories;
using cliLearningManagment.Repositories.Exceptions;

class Program
{
    static void Main(string[] args)
    {
        // clear Console
        Console.Clear();

        Console.Write("Login As(1 Admin,2 Teacher,3 Student): ");
        short command = short.Parse(Console.In.ReadLine() ?? "1");

        if (command == 1)
        {
            AdminRun();
        }
        else if (command == 2)
        {
            TeacherRun();
        }
        else
        {
        }
    }


    static void TeacherRun()
    {
        Console.Clear();

        Console.Write("Please Enter Teacher Name: ");
        string teacherName = Console.ReadLine() ?? "";

        Console.Write("Please Enter Teacher Password: ");
        string password = Console.ReadLine() ?? "";


        Teacher teacher = new Teacher(teacherName, password);
        if (!teacher.Login())
        {
            Console.WriteLine("Teacher name And Password is not Match.");
            Console.ReadKey();
            return;
        }


        while (true)
        {
            Console.Clear();

            Console.WriteLine("Chooses: ");
            Console.WriteLine("\t 1) add Grade.");
            Console.WriteLine("\t 2) courses List.");
            Console.WriteLine("\t 0) Back.");


            string command = Console.ReadLine() ?? "";
            if (command == "1")
            {
                Console.Clear();
                Console.WriteLine("Add Grade...");

                Console.Write("Enter Course Id: ");
                string courseId = Console.ReadLine() ?? "";

                Console.Write("Enter Student Id: ");
                string studentId = Console.ReadLine() ?? "";

                Console.Write("Enter Student grade in This Course: ");
                string grade = Console.ReadLine() ?? "";


                bool updateGradeResult = teacher.SetGrade(courseId, studentId, grade);

                if (!updateGradeResult)
                {
                    Console.Clear();
                    Console.WriteLine("course or Student is invalid.");
                    Console.ReadKey();
                    continue;
                }
            }
            else if (command == "2")
            {
                Console.Clear();
                List<Course> courses = teacher.GetCourses();
                foreach (Course course in courses)
                {
                    Console.WriteLine($"id: {course.Id},\tname: {course.Name},");
                }

                Console.ReadKey();
            }
            else if (command == "0")
            {
                Main(new String[] { });
                return;
            }
        }
    }

    static void AdminRun()
    {
        // clear Console
        Console.Clear();
        Admin admin = new Admin();

        Console.Write("Enter UserName: ");
        string name = Console.ReadLine() ?? "";
        Console.Write("Enter password: ");
        string password = Console.ReadLine() ?? "";

        if (!admin.Login(name, password))
        {
            Console.WriteLine("UserName And password not match.");
            return;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Chooses: ");
            Console.WriteLine("\t 1) create Teacher");
            Console.WriteLine("\t 2) create Course.");
            Console.WriteLine("\t 3) create Student.");
            Console.WriteLine("\t 4) add Course To Student.");
            Console.WriteLine("\t 0) Exit.");
            Console.Write("Please Enter Command: ");
            short command = short.Parse(Console.In.ReadLine() ?? "1");

            if (command == 1)
            {
                CreateTeacher();
            }
            else if (command == 2)
            {
                CreateCourse();
            }
            else if (command == 3)
            {
                CreateStudent();
            }
            else if(command == 4)
            {
                AddCoursesToStudent();
            }
            else
            {
                Main(new String[] { });
                return;
            }
        }
    }


    static void CreateTeacher()
    {
        Console.Clear();
        Console.WriteLine("Creating A Teacher... ");

        Console.Write("Enter Teacher Name: ");
        string teacherName = Console.In.ReadLine() ?? "";

        Console.Write("Enter Teacher Password: ");
        string teacherPassword = Console.In.ReadLine() ?? "";

        try
        {
            Teacher t = TeacherRepository.Instance.AddTeacher(new Teacher(teacherName, teacherPassword));
            Console.WriteLine("Teacher Created.");
            Console.WriteLine($"Teacher Id: {t.Id}");
            Console.ReadKey();
            return;
        }
        catch (DuplicateException)
        {
            Console.Clear();
            Console.WriteLine("Teacher Exists.");
            Console.ReadKey();
            return;
        }
    }


    static void CreateCourse()
    {
        Console.Clear();

        Console.WriteLine("Creating Course... ");

        Console.Write("Enter Name: ");
        string name = Console.In.ReadLine() ?? "";

        Console.Write("Please Enter Teacher Name: ");
        string teacherName = Console.In.ReadLine() ?? "";

        Teacher? teacher = TeacherRepository.Instance.FindByName(teacherName);
        if (teacher == null)
        {
            Console.Clear();
            Console.WriteLine("Teacher Not Exists.");
            Console.ReadKey();
            return;
        }


        Console.Clear();
        Course course = CourseRepository.Instance.AddCourse(new Course(name, teacher.Id ?? ""));
        Console.WriteLine("Course Created...");
        Console.WriteLine($"Course id = {course.Id}");
        Console.ReadKey();
        return;
    }

    static void CreateStudent()
    {
        Console.Clear();
        Console.WriteLine("Creating A Student... ");

        Console.Write("Enter Student Name: ");
        string studentName = Console.In.ReadLine() ?? "";

        Console.Write("Enter Student Password: ");
        string studentPassword = Console.In.ReadLine() ?? "";

        try
        {
            Student student = StudentRepository.Instance.AddStudent(new Student(studentName, studentPassword));
            Console.WriteLine("Student Created.");
            Console.WriteLine($"Student Id: {student.Id}");
            Console.ReadKey();
            return;
        }
        catch (DuplicateException)
        {
            Console.Clear();
            Console.WriteLine("Student Exists.");
            Console.ReadKey();
            return;
        }
    }

    public static void AddCoursesToStudent()
    {
        Console.Clear();
        Console.WriteLine("Add Course To Student...");


        Console.Write("Enter Course Id: ");
        string courseId = Console.In.ReadLine() ?? "";

        Console.Write("Enter Student Id: ");
        string studentId = Console.In.ReadLine() ?? "";

        Student? student = StudentRepository.Instance.FindById(studentId);

        if (student == null)
        {
            Console.Clear();
            Console.WriteLine("Invalid Student Id.");
            Console.ReadKey();
            return;
        }

        Course? courseForCheck = CourseRepository.Instance.FindById(courseId);
        if (courseForCheck == null)
        {
            Console.Clear();
            Console.WriteLine("Invalid Course Id.");
            Console.ReadKey();
            return;
        }

        List<Course> courses = StudentCourseRelation.Instance.StudentCourses(studentId);

        foreach (Course course in courses)
        {
            if (course.Id == courseId)
            {
                Console.Clear();
                Console.WriteLine("Course Already for Student Exists.");
                Console.ReadKey();
                return;
            }
        }

        StudentCourseRelation.Instance.CreateRelation(studentId, courseId);
        Console.WriteLine("Student Added.");
        Console.ReadKey();
        return;
    }
}