using System.Net.Http.Headers;

using Jh.Core.Results.Generic;
using Jh.Core.Results.Normal;

namespace Jh.CoreTest
{
    public class UnitTest1
    {
        [Fact]
        public void TestGenericResult()
        {
            var company = Result<Company>
                 .Create(new Company())
                 .OnEach(model =>
                 {
                     Console.WriteLine(model);
                 }).StartTry<User>(m =>
                 {
                     m.SetValue(new Company() { Name = "Joha" });
                     return new User();
                 }).OnNext<User>((model, user) =>
                 {
                     return user;
                 }).OnError((model, err) =>
                 {

                 });
            
        }

    }
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}