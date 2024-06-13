using Microsoft.EntityFrameworkCore;
using WebMusicaGrupoC.Models;
using WebMusicaGrupoC.Services.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext<GrupoCContext>(options => options.UseSqlServer("server=musicagrupos.database.windows.net;database=GrupoC;user=as;password=P0t@t0P0t@t0"));

//el código siguiente en el nivel de conexión de EF Core permite conexiones resistentes
//de SQL que se vuelven a intentar si se produce un error en la conexión.
builder.Services.AddDbContext<GrupoCContext>(options =>
{
    options.UseSqlServer(builder.Configuration["server=musicagrupos.database.windows.net;database=GrupoC;user=as;password=P0t@t0P0t@t0\""],
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        });
});
builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<IGruposRepositorio, EFGruposRepositorio>();
//builder.Services.AddScoped<IGruposRepositorio, FakeGruposRepositorio>();
builder.Services.AddScoped(typeof(IGenericRepositorio<>), typeof(EfGenericRepositorio<>));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
