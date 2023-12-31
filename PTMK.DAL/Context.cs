﻿using Microsoft.EntityFrameworkCore;
using PTMK.Models.Entities;

namespace PTMK.DAL;

public class Context : DbContext
{
    public DbSet<PersonInfoEntity> Persons { get; set; }
    private readonly string _conStr;
    public Context(string connection)
    {
        _conStr = connection;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlServer(_conStr, builder => builder.EnableRetryOnFailure());
    }
}