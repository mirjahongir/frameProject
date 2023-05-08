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
        //public async Task StartTest()
        //{
        //    Result<Company>.Create(new Company())
        //        .OnEach(m => { })
        //        .StartTry1(async m=> await GenerateUser(m.Value))
                
        //}
        [Fact]
        public async Task AsyncTest()
        {
            _ = Result<Company>
                .Create(new Company())
                .OnEach(model => { })
                .StartTry<User>(async m => await GenerateUser(m.Value))
                .OnNext<User>(async (m, user) => await ParseUser(user))
                .OnError((m, error) =>
                {
                    Console.WriteLine(error.Message);
                })
                .Finally();


        }

        public async ValueTask<User> GenerateUser(Company company)
        {
            return new User() {  Name="Some Change"};
        }
        public async Task ParseUser(User user)
        {
            Console.WriteLine(user.Name);
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