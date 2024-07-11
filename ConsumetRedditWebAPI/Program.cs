using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConsumeRedditWebAPI.Models;
using ConsumeRedditWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IRedditAccountService, RedditAccountService>(c =>
{
    c.BaseAddress = new Uri("https://www.reddit.com/api/v1/access_token");

});


builder.Services.AddHttpClient<IRedditService, RedditService>(c =>
{
    c.BaseAddress = new Uri("https://oauth.reddit.com/r/subreddit/top");
    c.DefaultRequestHeaders.Add("Accept", "application/.json");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
