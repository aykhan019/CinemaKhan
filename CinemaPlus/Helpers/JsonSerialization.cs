using CinemaPlus.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;


namespace CinemaPlus.Helpers
{
    public class JsonSerialization<T> where T : class
    {
        public static void Serialize(List<T> values, string filename)
        {
            var serializer = new JsonSerializer();

            using (var sw = new StreamWriter(filename))
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Formatting.Indented;
                    serializer.Serialize(jw, values);
                }
            }
        }

        public static List<T> Deserialize(string filename)
        {
            List<T> values = new List<T>();
            var serializer = new JsonSerializer();
            using (var sr = new StreamReader(filename))
            {
                using (var jr = new JsonTextReader(sr))
                {
                    values = serializer.Deserialize<List<T>>(jr);
                }
            }
            return values;
        }

        //public static List<Hall> GetDefaultHalls(string filename)
        //{
        //    List<Hall> halls;
        //    var serializer = new JsonSerializer();
        //    using (var sr = new StreamReader(filename))
        //    {
        //        using (var jr = new JsonTextReader(sr))
        //        {
        //            halls = serializer.Deserialize<List<Hall>>(jr);
        //        }
        //    }
        //    return halls;
        //}

        //public static List<Cinema> GetDefaultCinemas(string filename)
        //{
        //    List<Cinema> cinemas;
        //    var serializer = new JsonSerializer();
        //    using (var sr = new StreamReader(filename))
        //    {
        //        using (var jr = new JsonTextReader(sr))
        //        {
        //            cinemas = serializer.Deserialize<List<Cinema>>(jr);
        //        }
        //    }
        //    return cinemas;
        //}
    }
}
