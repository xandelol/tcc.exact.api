﻿// <auto-generated />
using exact.api.Data;
using exact.api.Data.Enum;
using exact.data.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace exact.api.Migrations
{
    [DbContext(typeof(ExactContext))]
    [Migration("20180609193600_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("exact.api.Data.GroupEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<string>("Roles");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("exact.api.Data.Model.ActionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Icon");

                    b.Property<int>("Index");

                    b.Property<bool>("IsActive");

                    b.Property<string>("MenuId");

                    b.Property<string>("Name");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("exact.api.Data.Model.GroupActionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ActionId");

                    b.Property<Guid>("GroupId");

                    b.Property<bool>("IsActive");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupActions");
                });

            modelBuilder.Entity("exact.api.Data.Model.SettingEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("ClientUses");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Key");

                    b.Property<string>("Name");

                    b.Property<string>("SubKey");

                    b.Property<int>("Type");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("exact.api.Data.Model.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<Guid>("GroupId");

                    b.Property<string>("Identifier");

                    b.Property<string>("ImageUrl");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime?>("LastLogin");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Phone");

                    b.Property<DateTime>("RegisterDate");

                    b.Property<string>("ResetPasswordCode");

                    b.Property<string>("Roles");

                    b.Property<string>("Token");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("exact.api.Data.Model.GroupActionEntity", b =>
                {
                    b.HasOne("exact.api.Data.Model.ActionEntity", "Action")
                        .WithMany()
                        .HasForeignKey("ActionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("exact.api.Data.GroupEntity", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("exact.api.Data.Model.UserEntity", b =>
                {
                    b.HasOne("exact.api.Data.GroupEntity", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
