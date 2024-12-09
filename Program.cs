using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapGet("/Calendar", () =>
{ 

    return CalendarEntry.LoadAll();

})
.WithName("GetCalendar")
.WithOpenApi();

app.MapPost("/Calendar", ([FromBody]CalendarEntry cal) =>
{

    cal.Save();
    return cal;

})
.WithName("PostCalendar").WithOpenApi();



app.Run();
