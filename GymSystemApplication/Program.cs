using Entities;
using Entities.Domain;
using Entities.Identities;
using Entities.Seeders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.Common;
using Services.MemberServices;
using Services.MembershipServices;
using Services.PaymentServices;
using Services.UserServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>( options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ICommonRepo<Member>, CommonRepo<Member>>();
builder.Services.AddScoped<ICommonRepo<Membership>, CommonRepo<Membership>>();
builder.Services.AddScoped<ICommonRepo<Payment>, CommonRepo<Payment>>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IMembershipService, MembershipService>();

builder.Services.AddIdentity<UserApplication, RoleApplication>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
    //.AddUserStore<UserStore<UserApplication, RoleApplication, ApplicationDbContext,Guid>>()
    //.AddRoleStore<RoleStore<RoleApplication,ApplicationDbContext,Guid>>();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

    options.AddPolicy("NotAuthenticated",
        policy =>
        {
            policy.RequireAssertion(
                context => !context.User.Identity.IsAuthenticated
                );
        });
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleApplication>>();
    await RoleSeeder.SeedRoleAsync(roleManager);
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
