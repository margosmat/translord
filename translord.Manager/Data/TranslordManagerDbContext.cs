using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace translord.Manager.Data;

public class TranslordManagerDbContext(DbContextOptions<TranslordManagerDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
}