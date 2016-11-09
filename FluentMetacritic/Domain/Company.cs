namespace FluentMetacritic.Domain
{
    public class Company : Entity, ICompany
    {
        public Company(string name)
            : base(name)
        {
        }

        public int? AverageCareerScore { get; set; }
    }
}