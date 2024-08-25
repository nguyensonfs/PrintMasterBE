﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrintMaster.Infrastructure.DataContext;

#nullable disable

namespace PrintMaster.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240821163306_InitDb")]
    partial class InitDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PrintMaster.Domain.Entities.Bill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BillName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BillStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TotalMoney")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TradingCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.ConfirmEmail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConfirmCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsConfirm")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ConfirmEmails");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.CustomerFeedback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FeedbackContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FeedbackTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ResponseByCompany")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResponseTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserFeedbackId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserFeedbackId");

                    b.ToTable("CustomerFeedbacks");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Delivery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ActualDeliveryTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DeliverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DeliveryAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DeliveryStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("EstimateDeliveryTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ShippingMethodId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DeliverId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ShippingMethodId");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Design", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ApproverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DesignStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("DesignTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DesignerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DesignerId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Designs");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.ImportCoupon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("ResourcePropertyDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TotalMoney")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TradingCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ResourcePropertyDetailId");

                    b.ToTable("ImportCoupons");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.KeyPerformanceIndicator", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("AchieveKPI")
                        .HasColumnType("bit");

                    b.Property<int>("ActuallyAchieved")
                        .HasColumnType("int");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IndicatorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Period")
                        .HasColumnType("int");

                    b.Property<int>("Target")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("KeyPerformanceIndicators");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSeen")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.PrintJob", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DesignId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("PrintJobStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DesignId");

                    b.ToTable("PrintJobs");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ActualEndDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("CommissionPercentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EmployeeCreateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpectedEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("LeaderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("Progress")
                        .HasColumnType("float");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestDescriptionFromCustomer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("StartingPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("LeaderId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Resource", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AvailableQuantity")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ResourceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ResourceTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ResourceTypeId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.ResourceForPrintJob", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PrintJobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("ResourcePropertyDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PrintJobId");

                    b.HasIndex("ResourcePropertyDetailId");

                    b.ToTable("ResourceForPrintJobs");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.ResourceProperty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("ResourceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ResourcePropertyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ResourceId");

                    b.ToTable("ResourceProperties");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.ResourcePropertyDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("ResourcePropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ResourcePropertyId");

                    b.ToTable("ResourcePropertyDetails");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.ResourceType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("NameOfResourceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ResourceTypes");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("RoleCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.ShippingMethod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ShippingMethodName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ShippingMethods");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("ManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfMember")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Bill", b =>
                {
                    b.HasOne("PrintMaster.Domain.Entities.Customer", "Customer")
                        .WithMany("Bills")
                        .HasForeignKey("CustomerId");

                    b.HasOne("PrintMaster.Domain.Entities.User", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.HasOne("PrintMaster.Domain.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.Navigation("Customer");

                    b.Navigation("Employee");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.ConfirmEmail", b =>
                {
                    b.HasOne("PrintMaster.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.CustomerFeedback", b =>
                {
                    b.HasOne("PrintMaster.Domain.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("PrintMaster.Domain.Entities.Project", "Project")
                        .WithMany("CustomerFeedbacks")
                        .HasForeignKey("ProjectId");

                    b.HasOne("PrintMaster.Domain.Entities.User", "UserFeedback")
                        .WithMany()
                        .HasForeignKey("UserFeedbackId");

                    b.Navigation("Customer");

                    b.Navigation("Project");

                    b.Navigation("UserFeedback");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Delivery", b =>
                {
                    b.HasOne("PrintMaster.Domain.Entities.Customer", "Customer")
                        .WithMany("Delivery")
                        .HasForeignKey("CustomerId");

                    b.HasOne("PrintMaster.Domain.Entities.User", "Deliver")
                        .WithMany()
                        .HasForeignKey("DeliverId");

                    b.HasOne("PrintMaster.Domain.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.HasOne("PrintMaster.Domain.Entities.ShippingMethod", "ShippingMethod")
                        .WithMany("Deliveries")
                        .HasForeignKey("ShippingMethodId");

                    b.Navigation("Customer");

                    b.Navigation("Deliver");

                    b.Navigation("Project");

                    b.Navigation("ShippingMethod");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Design", b =>
                {
                    b.HasOne("PrintMaster.Domain.Entities.User", "Designer")
                        .WithMany()
                        .HasForeignKey("DesignerId");

                    b.HasOne("PrintMaster.Domain.Entities.Project", "Project")
                        .WithMany("Designs")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Designer");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.ImportCoupon", b =>
                {
                    b.HasOne("PrintMaster.Domain.Entities.User", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.HasOne("PrintMaster.Domain.Entities.ResourcePropertyDetail", "ResourcePropertyDetail")
                        .WithMany("ImportCoupons")
                        .HasForeignKey("ResourcePropertyDetailId");

                    b.Navigation("Employee");

                    b.Navigation("ResourcePropertyDetail");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.KeyPerformanceIndicator", b =>
                {
                    b.HasOne("PrintMaster.Domain.Entities.User", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Notification", b =>
                {
                    b.HasOne("PrintMaster.Domain.Entities.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Permission", b =>
                {
                    b.HasOne("PrintMaster.Domain.Entities.Role", "Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId");

                    b.HasOne("PrintMaster.Domain.Entities.User", "User")
                        .WithMany("Permissions")
                        .HasForeignKey("UserId");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.PrintJob", b =>
                {
                    b.HasOne("PrintMaster.Domain.Entities.Design", "Design")
                        .WithMany("PrintJobs")
                        .HasForeignKey("DesignId");

                    b.Navigation("Design");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Project", b =>
                {
                    b.HasOne("PrintMaster.Domain.Entities.Customer", "Customer")
                        .WithMany("Projects")
                        .HasForeignKey("CustomerId");

                    b.HasOne("PrintMaster.Domain.Entities.User", "Leader")
                        .WithMany()
                        .HasForeignKey("LeaderId");

                    b.Navigation("Customer");

                    b.Navigation("Leader");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("PrintMaster.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Resource", b =>
                {
                    b.HasOne("PrintMaster.Domain.Entities.ResourceType", "ResourceType")
                        .WithMany("Resources")
                        .HasForeignKey("ResourceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ResourceType");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.ResourceForPrintJob", b =>
                {
                    b.HasOne("PrintMaster.Domain.Entities.PrintJob", "PrintJob")
                        .WithMany("ResourceForPrints")
                        .HasForeignKey("PrintJobId");

                    b.HasOne("PrintMaster.Domain.Entities.ResourcePropertyDetail", "Resource")
                        .WithMany("ResourceForPrintJobs")
                        .HasForeignKey("ResourcePropertyDetailId");

                    b.Navigation("PrintJob");

                    b.Navigation("Resource");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.ResourceProperty", b =>
                {
                    b.HasOne("PrintMaster.Domain.Entities.Resource", "Resource")
                        .WithMany("ResourceProperties")
                        .HasForeignKey("ResourceId");

                    b.Navigation("Resource");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.ResourcePropertyDetail", b =>
                {
                    b.HasOne("PrintMaster.Domain.Entities.ResourceProperty", "ResourceProperty")
                        .WithMany("ResourcePropertyDetails")
                        .HasForeignKey("ResourcePropertyId");

                    b.Navigation("ResourceProperty");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.User", b =>
                {
                    b.HasOne("PrintMaster.Domain.Entities.Team", "Team")
                        .WithMany("Users")
                        .HasForeignKey("TeamId");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Bills");

                    b.Navigation("Delivery");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Design", b =>
                {
                    b.Navigation("PrintJobs");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.PrintJob", b =>
                {
                    b.Navigation("ResourceForPrints");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Project", b =>
                {
                    b.Navigation("CustomerFeedbacks");

                    b.Navigation("Designs");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Resource", b =>
                {
                    b.Navigation("ResourceProperties");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.ResourceProperty", b =>
                {
                    b.Navigation("ResourcePropertyDetails");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.ResourcePropertyDetail", b =>
                {
                    b.Navigation("ImportCoupons");

                    b.Navigation("ResourceForPrintJobs");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.ResourceType", b =>
                {
                    b.Navigation("Resources");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Role", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.ShippingMethod", b =>
                {
                    b.Navigation("Deliveries");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.Team", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("PrintMaster.Domain.Entities.User", b =>
                {
                    b.Navigation("Notifications");

                    b.Navigation("Permissions");
                });
#pragma warning restore 612, 618
        }
    }
}
