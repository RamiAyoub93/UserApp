using System.Security.Authentication.ExtendedProtection;
using MainApp.Data;
using MainApp.Dialogs;
using MainApp.Models;
using MainApp.Services;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    
    .AddSingleton<DataContext<User>>()
    .AddScoped<UserService>()
    .AddTransient<MenuDialogs>()
    .BuildServiceProvider();

var menuDialog = serviceProvider.GetRequiredService<MenuDialogs>();

    menuDialog.MenuOptionsDialog();