namespace FCV_DutchLadyMail_Model.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class MassMailsDbContext : DbContext
    {
        public MassMailsDbContext()
            : base("name=MassMailsEntities")
        {
        }
        public DbSet<ContactLists> ContactLists { get; set; }
        public DbSet<SMTPProfiles> SMTPProfiles { get; set; }
        public DbSet<QueueMails> QueueMails { get; set; }
        public DbSet<SendMails> SendMails { get; set; }
        public DbSet<Templates> Templates { get; set; }
        public DbSet<Signatures> Signatures { get; set; }

        public DbSet<NhaPhanPhoi> NhaPhanPhois { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<MailModel> MailModels { get; set; }
        public DbSet<DistributorModel> Distributors { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        //public DbSet<DistributorsView> DistributorsView { get; set; }
        public virtual DbSet<PERMISSION> PERMISSIONS { get; set; }
        public virtual DbSet<ROLE> ROLES { get; set; }
        public virtual DbSet<USER> USERS { get; set; }
        //public virtual DbSet<USERs_ROLEs> USERS_ROLES { get; set; }
        public virtual DbSet<LOG_ACCESS> LOG_ACCESS { get; set; }

        public virtual DbSet<FILE> FILES { get; set; }
        public virtual DbSet<Folders> Folders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<PERMISSION>()
               .HasMany(e => e.ROLES)
               .WithMany(e => e.PERMISSIONS)
               .Map(m => m.ToTable("ROLE_PERMISSION").MapLeftKey("Permission_Id").MapRightKey("Role_Id"));

            modelBuilder.Entity<ROLE>()
                .HasMany(e => e.USERS)
                .WithMany(e => e.ROLES)
                .Map(m => m.ToTable("USER_ROLE").MapLeftKey("Role_Id").MapRightKey("User_Id"));
            modelBuilder.Entity<USER>()
               .HasMany(e => e.LOG_ACCESS)
               .WithRequired(e => e.USER)
               .WillCascadeOnDelete(false);
                
            modelBuilder.Entity<USER>()
                .HasMany(e => e.FILES)
                .WithRequired(e => e.USER)
                .HasForeignKey(e => e.userId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<USER>()
              .HasMany(e => e.ROLES)
              .WithMany(e => e.USERS)
              .Map(m => m.ToTable("USER_ROLE").MapLeftKey("Role_Id").MapRightKey("User_Id"));
        }
    }
}