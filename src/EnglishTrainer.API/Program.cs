using EnglishTrainer.API.ActionFilters;
using EnglishTrainer.API.Extensions;
using EnglishTrainer.API.Extensions.ServiceExtensions;
using EnglishTrainer.LoggerService;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Ќастройка позвол€ет возвращать XML формат если добавить заголовок в запрос
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true; //Ѕудет возвращать 406 Not Acceptable
                                           //если клиент запросит неподдерживаемый возвращаемый тип данных
}).AddNewtonsoftJson()
   .AddXmlDataContractSerializerFormatters()
   .AddCustomCSVFormatter();


//For validation
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region From ServiceExtensions
builder.Services.ConfigureCors(); 
builder.Services.ConfigureIISIntegration(); 
builder.Services.ConfigureLoggerService(); //NLog
builder.Services.ConfigureSqlContext(builder.Configuration); //EF
builder.Services.ConfigureRepositoryManager(); //DI for all services
#endregion

//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//NLog
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

//Custom filter
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddScoped<ValidateWordExistAttribute>();
builder.Services.AddScoped<ValidateExampleForWordExistsAttribute>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else 
{ 
    app.UseHsts(); 
}

using (var scope = app.Services.CreateScope())
{
    var loggerManager = scope.ServiceProvider.GetService<ILoggerManager>()!; 
    app.ConfigureExceptionHandler(loggerManager);
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseForwardedHeaders(new ForwardedHeadersOptions 
{ 
    ForwardedHeaders = ForwardedHeaders.All 
});

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
