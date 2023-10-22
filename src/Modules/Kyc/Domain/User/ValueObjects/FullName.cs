namespace CXChange.Modules.Kyc.Domain.User.ValueObjects
{
    public record FullName
    {
        public FullName(string name, string surname)
        {

            Name = name;
            SurName = surname;
        }
        public string Name { get; init; }
        public string SurName { get; init; }
    }
}
