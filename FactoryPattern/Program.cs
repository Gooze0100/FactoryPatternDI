using FactoryPattern.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using FactoryPattern.Samples;
using FactoryPattern.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
//builder.Services.AddTransient<ISample1, Sample1>();
// Func is delegate is a method that you pass around like a variable, the purpose of it for somebody else to call a method later
// So we saying with Func<ISample1> that put it in dependency injection ask for a Func ISample1 and then you get instance of this x => () => x.GetService<ISample1>()!
// this x => () => x.GetService<ISample1>()! has one method () with no parameters in it and its job is when you call a method 
// it actually goes to services and gets you interface ISample1
// so it let us to create it later it is not running when you put it in dependency injection it is run when you call it
//builder.Services.AddSingleton<Func<ISample1>>(x => () => x.GetService<ISample1>()!);
// register those as transients, and that is going to register the factory itself which calls the Func
builder.Services.AddAbstractFactory<ISample1, Sample1>();
builder.Services.AddAbstractFactory<ISample2, Sample2>();
// dont need to give types because it handles types for us
builder.Services.AddGenericClassWithDataFactory();
builder.Services.AddVehicleFactory();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();


// do not use design patterns everyday they are typically complex and only reason you add complex design pattern is if the complexity of your code
// is more complex than design pattern
// so design patterns you use when you have complex situation that you need to make it less complex and more scalable over time
// if is not that complex do not use the pattern