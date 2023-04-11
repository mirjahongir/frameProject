

using Jh.Core.Interfaces.Repository;
using Jh.EfCoreRepository.Interfaces;

namespace TestConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //IDataContext context;
            //var company = new Jh.EfCoreRepository.Repository.EfRepository<Company,int>(context);
            //company.Add(new Company() { });
        }

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