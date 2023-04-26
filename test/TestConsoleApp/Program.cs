using Jh.Core.Interfaces.Repository;

namespace TestConsoleApp
{
    internal class Program
    {
        static  void Main(string[] args)
        {
           
        }

    }
    public class Message
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }
   
    public class Company:IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
    }
    public class User
    {
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
    }
}