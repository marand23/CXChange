namespace CXChange.Modules.Kyc.Domain.User.ValueObjects
{
    public record FullName
    {
        public FullName(string name, string surname)
        {

            Name = name;
            SurName = surname;
        }
        private string Name { get; init; }
        private string SurName { get; init; }
        public FullNameSnapShot GetSnapshot()
        {
            return new FullNameSnapShot
            {
                SurName = this.SurName,
                Name = this.Name,
            };
        }

    }
}
