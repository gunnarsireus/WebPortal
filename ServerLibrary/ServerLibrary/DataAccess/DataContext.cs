using System.Data.Entity;

namespace ServerLibrary.Model
{
    public class DataContext : DbContext
    {
        public DbSet<Account>         Accounts         { get; set; }
        public DbSet<Customer>        Customers        { get; set; }
        public DbSet<Issue>           Issues           { get; set; }
        public DbSet<IssueClass>      IssueClasses     { get; set; }
        public DbSet<IssueFeedback>   IssueFeedbacks   { get; set; }
        public DbSet<IssueMaterial>   IssueMaterials   { get; set; }
        public DbSet<IssuePhoto>      IssuePhotos      { get; set; }
        public DbSet<IssueReader>     IssueReaders     { get; set; }
        public DbSet<IssueTime>       IssueTimes       { get; set; }
        public DbSet<IssueTransition> IssueTransitions { get; set; }
        public DbSet<News>            News             { get; set; }
        public DbSet<NewsReader>      NewsReaders      { get; set; }
        public DbSet<Offer>           Offers           { get; set; }
        public DbSet<OfferReader>     OfferReaders     { get; set; }
        public DbSet<TimeType>        TimeTypes        { get; set; }
    }
}
