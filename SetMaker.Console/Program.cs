// See https://aka.ms/new-console-template for more information
using SetMaker.Console.Manager;
using SetMaker.Console.Models;
using SetMaker.Console.Reader;

Console.WriteLine("Welcome to SetMaker App!");
Console.WriteLine("");
string choice;
do
{
    Console.WriteLine("-----Choose Options:------");

    Console.WriteLine("1. Create Course");
    Console.WriteLine("2. Add Book to Course");
    Console.WriteLine("3. Remove Book from Course");
    Console.WriteLine("4. Create Exam Set of Course");
    Console.WriteLine("5. Create Practice Sets of Course");
    Console.WriteLine("6. Create Course Exam Setting");
    Console.WriteLine("0. Exit");

    choice = Console.ReadLine().ToUpper();
    if(choice == "0")
    {
        break;
    }
    ICourseManager coursemanager = new CourseManager();
    IBookManager bookManager = new BookManager();
    ICourseSetSettingManager setsettingmanager = new CourseSetSettingManager(coursemanager);
    ICourseSetManager setManager = new CourseSetManager();
    ICourseSetsCreator courseSetsCreator = new CourseSetCreator(coursemanager);
    
    string? bookfolderpath = null ;
    Book book;
    switch (choice)
    {
        #region 1. Create Course
        case "1":
            Console.WriteLine("Enter Course Name");
            var coursename = Console.ReadLine();
            Console.WriteLine("Enter Course code");
            var coursecode = Console.ReadLine();
            if (coursecode == null && coursename == null)
            {
                Console.WriteLine("Invalid entry!");
                Console.ReadKey();
                continue;
            }
            Course course = new Course()
            {
                id = coursecode,
                name = coursename,
            };
            Console.WriteLine("\nDo you want to add book to this course(Y/N)");
            choice = Console.ReadLine().ToUpper();
            if (choice == "Y".ToUpper())
            {
                Console.WriteLine("Enter path of the folder of Book");
                bookfolderpath = Console.ReadLine().ToLower();
                try
                {
                    bookManager = new BookManager();
                    book = bookManager.ReadBookfromfolder(bookfolderpath);
                    if (book != null)
                    {
                        course.books.Add(book);
                    }
                    if (book == null)
                    {
                        Console.WriteLine("Book not found!");
                    }
                    coursemanager= new CourseManager();
                    coursemanager.SaveCourse(course);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            break;
        #endregion
        #region 2. Add Book to Course
        case "2":
            try
            {
                Console.WriteLine("Enter Course code");
                coursecode = Console.ReadLine();
                Console.WriteLine("Enter path of the folder of Book");
                bookfolderpath = Console.ReadLine().ToLower();
                book = bookManager.ReadBookfromfolder(bookfolderpath);
                course=coursemanager.GetCourse(coursecode);
                if (book != null && course != null)
                {
                    course.books.Add(book);
                }
                else 
                {
                    Console.WriteLine("Cousre or Book not found!");
                }
                coursemanager = new CourseManager();
                coursemanager.SaveCourse(course);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            break;
        #endregion
        #region 4. Create Exam Set of Course
        case "4":
            //Read course
            Console.WriteLine("Enter the course id:");
            coursecode= Console.ReadLine();
            if(coursecode == null)
            {
                break;
            }
            course = coursemanager.GetCourse(coursecode);

            //Find Exam Set Setting
            var setsetting=setsettingmanager.GetCourseSetSetting(coursecode);
            if(setsetting==null)
            {
                Console.WriteLine("Set Setting for course is not available....");
                break;
            }

            //Create Exam Set
            var practiceset=courseSetsCreator.CreateExamSet(course, setsetting);
            if(practiceset==null)
            {
                Console.WriteLine("Exam Set not created!");
                break;
            }

            //Save Exam Set
            if(setManager.SaveExamSet(course, practiceset))
            {
                Console.WriteLine("Exam Set saved");
                break;
            }
            Console.WriteLine("Exam Set is failed to save!");
            break;
        #endregion
        #region 4. Create Practice Set of Course
        case "5":
            //Read course
            Console.WriteLine("Enter the course id:");
            coursecode = Console.ReadLine();
            if (coursecode == null)
            {
                break;
            }
            course = coursemanager.GetCourse(coursecode);

            //Create Practice Set
            Console.Write("Enter the no.of question in a set:\t");
            var noofquestions=int.Parse(Console.ReadLine());
            foreach(var subjectidname in coursemanager.GetSubjectIdandNamePairs(coursecode))
            {
                var practicesets = courseSetsCreator.CreateSubjectPracticeSets(course,subjectidname.Key,noofquestions);
                if (practicesets == null)
                {
                    Console.WriteLine($"Practice Set of {subjectidname.Value} not created!");
                    continue;
                }
                Console.WriteLine($"Practice Set of {subjectidname.Value} created!");
                if (setManager.SaveSubjectPracticeSets(course, subjectidname.Key, practicesets))
                {
                    Console.WriteLine($"Practice Set of {subjectidname.Value} is saved");
                    continue;
                };
                Console.WriteLine($"Practice Set of {subjectidname.Value} is not saved");
            }
            break;
        #endregion
        #region 6. Create ExamSet Setting
        case "6":
            //Read course
            Console.WriteLine("Enter the course id:");
            coursecode = Console.ReadLine();
            if (coursecode == null)
            {
                break;
            }
            course = coursemanager.GetCourse(coursecode);

            //Create Exam Set
            Console.WriteLine("Enter the Completion time in minutes:");
            var completiontime=int.Parse(Console.ReadLine());
            Console.WriteLine("Enetr the negative markng in percentage:");
            var negativemarking=decimal.Parse(Console.ReadLine())/100;
            IDictionary<string, int> subjectquestions=new Dictionary<string, int>();
            Console.WriteLine("Enetr the question to select from subjects of course");
            foreach (var subjectidandnamepair in coursemanager.GetSubjectIdandNamePairs(course.id))
            {
                Console.Write($"{subjectidandnamepair.Value} :\t");
                var noofquestion = int.Parse(Console.ReadLine());
                subjectquestions.Add(subjectidandnamepair.Key, noofquestion);
            }
            CourseSetSetting courseSetSetting=new CourseSetSetting()
            {
                completionTime=TimeSpan.FromMinutes(completiontime),
                negativemarking=negativemarking,
                subjectquestions=subjectquestions,
            };

            //Save Exam Set
            if(setsettingmanager.SaveSetSetting(course.id, courseSetSetting))
            {
                Console.WriteLine("Sucessfully saved exam set Question");
                break;
            }
            Console.WriteLine("Unable to save exam set");
            break;
        #endregion
        default:
            Console.WriteLine("Invalid Input.....!");
            break;
    }
    Console.WriteLine("Do you want to exit(Y/N)?");
    choice = Console.ReadLine().ToUpper();
} while (choice == "N");
Console.WriteLine("Thank you.....!");
Console.ReadKey();
