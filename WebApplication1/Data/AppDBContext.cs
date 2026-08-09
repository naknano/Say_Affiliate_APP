using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.Authentication;
using WebApplication1.Models.Content;


namespace WebApplication1.Data;


public class AppDBContext : IdentityDbContext<IdentityUser>
// public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    { }


    //Entity
    public DbSet<UserDetail> UserDetails { get; set; }
    public DbSet<Broker> brokers { get; set; }
    public DbSet<UserTradingAccount> UserTradingAccounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionUnauth> TransactionsUnauth { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<TransactionLog> TransactionLogs { get; set; }
    public DbSet<UserCrypto> UserCryptos { get; set; }
    public DbSet<UserBank> UserBanks { get; set; }

}

