using SetMaker.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SetMaker.Console.Manager
{
    public class CourseSetSettingManager
        : ICourseSetSettingManager
    {
        private string _directory;
        private ICourseManager _courseManager;

        public CourseSetSettingManager(ICourseManager courseManager)
        {
            _directory = @"D:\Downloads\CourseSetsettings";
            if(!Directory.Exists(_directory))
            {
                Directory.CreateDirectory(_directory);
            }
            _courseManager =courseManager;
        }
        public bool SaveSetSetting(string courseid, CourseSetSetting setting)
        {
            var course = _courseManager.GetCourse(courseid);
            if (course == null) return false;
            if (setting == null) return false;
            string filename = course.name + "_" + course.id+".json";
            if (_directory == null)
            {
                System.Console.WriteLine("Invalid Directory!");
                System.Console.ReadKey();
                return false;
            }
            string filepath=Path.Combine(_directory, filename);
            try
            {
                if(File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                using(FileStream fs = File.Create(filepath))
                {
                    string json= JsonSerializer.Serialize(setting);
                    byte[] data=new UTF8Encoding().GetBytes(json);
                    fs.Write(data, 0, data.Length);
                    System.Console.WriteLine("Saved to: \n" + filepath);
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public CourseSetSetting? GetCourseSetSetting(string courseid)
        {
            try
            {
                string[] files=Directory.GetFiles(_directory);
                foreach(string file in files)
                {
                    if(file==null) continue;
                    var items = Path.GetFileNameWithoutExtension(file).Split("_");
                    if (!(items.Count() == 2)) continue;
                    var name=items[0];
                    var id=items[1];
                    if (id != courseid) continue;
                    string filedata=File.ReadAllText(file);
                    if (string.IsNullOrEmpty(filedata)) continue;
                    var setting= JsonSerializer.Deserialize<CourseSetSetting>(filedata);
                    return setting;
                }
                return null;
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
