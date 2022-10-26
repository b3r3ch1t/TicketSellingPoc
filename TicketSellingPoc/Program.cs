using TicketSellingPoc;
using TicketSellingPoc.Interfaces;
using TicketSellingPoc.Repositories;
using TicketSellingPoc.Services;

var services = new ServiceCollection();

 

services.AddScoped<ISendEmailCampaingn, SendEmailCampaingn>(); 

services.AddSingleton<IEventsRepository>(new EventsRepository() );
services.AddTransient<ConsoleApp>();


services.BuildServiceProvider()
    .GetService<ConsoleApp>()!.Run();
