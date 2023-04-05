using Core.Results.Normal;

namespace CoreTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var result = Result.Create()
                .OnEach(() => { })
                .StartTry(() => { })
                .OnNext<Company>(model =>
                {
                    return new Company() { Name = "Joha" };

                }).OnNext<Company>((model, company) =>
                {
                    
                }).OnNext(model =>
                {
                    return (new Company(), new User());
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