using MovieTheater.Data.Models;

namespace MovieTheater.Models.User
{
    public class CustomerTypeVmd
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public CustomerTypeVmd(CustomerType customerType)
        {
            if (customerType == null)
                return;

            Name = customerType.Name;
            Id = customerType.Id;
        }
    }
}