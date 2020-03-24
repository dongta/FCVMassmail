namespace FCVStockTool
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using FCVStockTool.Models;

    public partial class StockDbContext : DbContext
    {
        public StockDbContext()
            : base("name=StockDbContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; } 
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<ProductItem> ProductItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductTicket> ProductTickets { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<StockDiary> StockDiaries { get; set; }
        public virtual DbSet<SupplierContact> SupplierContacts { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<AutoNumber> AutoNumbers { get; set; }
        public virtual DbSet<ProductStatus> ProductStatuses { get; set; }
        public virtual DbSet<StockStatus> StockStatuses { get; set; }
        public virtual DbSet<StockDiaryReason> StockDiaryReasons { get; set; }
        public virtual DbSet<TicketStatus> TicketStatuses { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<AssetGroup> AssetGroups { get; set; }
        public virtual DbSet<AssetClass> AssetClasses { get; set; }
        public virtual DbSet<AccountantAsset> AccountantAssets { get; set; }
        public virtual DbSet<ReceiveEmail> ReceiveEmails { get; set; }
        public virtual DbSet<ConfigEmail> ConfigEmails { get; set; }

        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<FilteredProductItem> FilteredProductItems { get; set; }
        public virtual DbSet<FilteredStockDiary> FilteredStockDiaries { get; set; }

        public virtual DbSet<FilteredAccessory> FilteredAccessories { get; set; }
        public virtual DbSet<Accessory> Accessories { get; set; }
        public virtual DbSet<AccessoryExport> AccessoryExports { get; set; }
        public virtual DbSet<AccessoryImport> AccessoryImports { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        //    modelBuilder.Entity<InvoiceDetail>()
        //        .Property(e => e.UnitPrice)
        //        .HasPrecision(19, 4);

        //    modelBuilder.Entity<Invoice>()
        //        .Property(e => e.Amount)
        //        .HasPrecision(19, 4);

            //modelBuilder.Entity<Product>()
            //    .HasMany(e => e.Products1)
            //    .WithOptional(e => e.Product1)
            //    .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ProductTicketRequestBys)
                .WithOptional(e => e.RequestBy)
                .HasForeignKey(e => e.RequestById);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ProductTicketApproveBys)
                .WithOptional(e => e.ApproveBy)
                .HasForeignKey(e => e.ApproveById);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ProductTicketExportBys)
                .WithOptional(e => e.ExportBy)
                .HasForeignKey(e => e.ExportById);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ProductTicketCancelBys)
                .WithOptional(e => e.CancelBy)
                .HasForeignKey(e => e.CancelById);
        }
    }
}
