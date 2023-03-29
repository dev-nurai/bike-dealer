using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BikeDealer.Models;

public partial class DbbikeDealerContext : DbContext
{
    public DbbikeDealerContext()
    {
    }

    public DbbikeDealerContext(DbContextOptions<DbbikeDealerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accessory> Accessories { get; set; }

    public virtual DbSet<BikeCompany> BikeCompanies { get; set; }

    public virtual DbSet<BikeModel> BikeModels { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeesDesignation> EmployeesDesignations { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderAccessory> OrderAccessories { get; set; }

    public virtual DbSet<Quotation> Quotations { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=INBOOK_X1_SLIM\\NURAIDB;Database=DBBikeDealer;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accessory>(entity =>
        {
            entity.HasKey(e => e.AccessoriesId).HasName("PK__Accessor__7DB4AEDF779E0804");

            entity.Property(e => e.AccessoriesId).HasColumnName("accessories_id");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BikeCompany>(entity =>
        {
            entity.HasKey(e => e.BikeCompId).HasName("PK__BikeComp__EE9916C274C0D5D1");

            entity.ToTable("BikeCompany");

            entity.Property(e => e.BikeCompId).HasColumnName("bike_comp_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BikeModel>(entity =>
        {
            entity.HasKey(e => e.BikeModelId).HasName("PK__BikeMode__45C38F942B86CA75");

            entity.ToTable("BikeModel");

            entity.Property(e => e.BikeModelId).HasColumnName("bike_model_id");
            entity.Property(e => e.BikeCompId).HasColumnName("bike_comp_id");
            entity.Property(e => e.ModelName)
                .HasMaxLength(500)
                .HasColumnName("Model_Name");
            entity.Property(e => e.ModelYear).HasColumnName("Model_Year");

            entity.HasOne(d => d.BikeComp).WithMany(p => p.BikeModels)
                .HasForeignKey(d => d.BikeCompId)
                .HasConstraintName("FK_BikeModel_BikeCompany");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustId).HasName("PK__Customer__A1B71F903DD9DD01");

            entity.ToTable("Customer");

            entity.Property(e => e.CustId).HasColumnName("cust_id");
            entity.Property(e => e.DateOfQuotation)
                .HasColumnType("date")
                .HasColumnName("Date_of_Quotation");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__1299A8616C359F3F");

            entity.Property(e => e.EmpId).HasColumnName("emp_id");
            entity.Property(e => e.DateOfJoining)
                .HasColumnType("date")
                .HasColumnName("Date_of_Joining");
            entity.Property(e => e.DateOfResign)
                .HasColumnType("date")
                .HasColumnName("Date_of_Resign");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.DesignationNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Designation)
                .HasConstraintName("FK_employees_destination_id");
        });

        modelBuilder.Entity<EmployeesDesignation>(entity =>
        {
            entity.HasKey(e => e.EmpDesignationId).HasName("PK__Employee__9E3F776459C578A6");

            entity.ToTable("EmployeesDesignation");

            entity.Property(e => e.EmpDesignationId).HasColumnName("emp_designation_id");
            entity.Property(e => e.Designation)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__465962290CA8AA38");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.BikeModelId).HasColumnName("bike_model_id");
            entity.Property(e => e.CustId).HasColumnName("cust_id");
            entity.Property(e => e.EmpId).HasColumnName("emp_id");
            entity.Property(e => e.OrderDate).HasColumnType("date");
            entity.Property(e => e.Remarks)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("date")
                .HasColumnName("Updated_Date");

            entity.HasOne(d => d.BikeModel).WithMany(p => p.Orders)
                .HasForeignKey(d => d.BikeModelId)
                .HasConstraintName("FK_order_bikemodel_id");

            entity.HasOne(d => d.Cust).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustId)
                .HasConstraintName("FK_order_customer_id");

            entity.HasOne(d => d.Emp).WithMany(p => p.OrderEmps)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK_order_Employees_id");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Status)
                .HasConstraintName("FK_Order_Status");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.OrderUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_order_updatedby");
        });

        modelBuilder.Entity<OrderAccessory>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.AccessoriesId).HasColumnName("accessories_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");

            entity.HasOne(d => d.Accessories).WithMany()
                .HasForeignKey(d => d.AccessoriesId)
                .HasConstraintName("FK_accessories_id");

            entity.HasOne(d => d.Order).WithMany()
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_order_accessories_id");
        });

        modelBuilder.Entity<Quotation>(entity =>
        {
            entity.HasKey(e => e.QuoteId).HasName("PK__Quotatio__0D37DF0C55B8CC21");

            entity.ToTable("Quotation");

            entity.Property(e => e.QuoteId).HasColumnName("quote_id");
            entity.Property(e => e.BikeModelId).HasColumnName("bike_model_id");
            entity.Property(e => e.CustId).HasColumnName("cust_id");
            entity.Property(e => e.EmpId).HasColumnName("emp_id");
            entity.Property(e => e.QuotationDate)
                .HasColumnType("date")
                .HasColumnName("Quotation_Date");
            entity.Property(e => e.QuoteDetails)
                .HasMaxLength(500)
                .HasColumnName("Quote_Details");
            entity.Property(e => e.Remarks)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasColumnType("date")
                .HasColumnName("Update_Date");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_by");

            entity.HasOne(d => d.BikeModel).WithMany(p => p.Quotations)
                .HasForeignKey(d => d.BikeModelId)
                .HasConstraintName("FK_quotation_bikemodel_id");

            entity.HasOne(d => d.Cust).WithMany(p => p.Quotations)
                .HasForeignKey(d => d.CustId)
                .HasConstraintName("FK_quoation_customer_id");

            entity.HasOne(d => d.Emp).WithMany(p => p.QuotationEmps)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK_quotation_employee_id");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.QuotationUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_Quotation_Updatedby");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Status__3214EC07FC65C529");

            entity.ToTable("Status");

            entity.Property(e => e.Status1)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Status");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
