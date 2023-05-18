namespace Creational.Builder.ComplexObject
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAdress { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Division { get; set; }
        public string Position { get; set; }
        public int TotalCompensation { get; set; }
    }

    public class UserBuilder
    {
        protected User User { get; }

        public UserBuilder(string firstName, string lastName) => User = new User
        {
            FirstName = firstName,
            LastName = lastName
        };

        protected UserBuilder(User user) => User = user;

        public UserAddressBuilder Lives => new (User);
        public UserJobBuilder Works => new (User);

        public static implicit operator User(UserBuilder builder) => builder.User;

        public class UserJobBuilder : UserBuilder
        {
            private UserJobBuilder() : base(new User())
            { }

            internal UserJobBuilder(User user) : base(user)
            { }

            public UserJobBuilder In(string division)
            {
                User.Division = division;
                return this;
            }

            public UserJobBuilder AsA(string position)
            {
                User.Position = position;
                return this;
            }

            public UserJobBuilder Earning(int tc)
            {
                User.TotalCompensation = tc;
                return this;
            }
        }

        public class UserAddressBuilder : UserBuilder
        {
            private UserAddressBuilder() : base(new User())
            { }

            internal UserAddressBuilder(User user) : base(user)
            { }

            public UserAddressBuilder At(string streetAddress)
            {
                User.StreetAdress = streetAddress;
                return this;
            }

            public UserAddressBuilder In(string city)
            {
                User.City = city;
                return this;
            }

            public UserAddressBuilder WithZipCode(string zipCode)
            {
                User.Zipcode = zipCode;
                return this;
            }
        }
    }
}
