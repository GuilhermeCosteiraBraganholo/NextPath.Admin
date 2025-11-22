using Microsoft.EntityFrameworkCore;
using NextPath.Application.Interfaces;
using NextPath.Application.Services;
using NextPath.Domain.Interfaces;
using NextPath.Infrastructure.Data;
using NextPath.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Controllers + ProblemDetails padrão
builder.Services.AddControllers();

// Swagger SEM CONDIÇÃO DE AMBIENTE
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext - usando LocalDB por padrão (funciona direto no VS)
builder.Services.AddDbContext<NextPathDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositórios
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

// Serviços de aplicação
builder.Services.AddScoped<ICourseService, CourseService>();

var app = builder.Build();

// Garante que o banco será criado automaticamente
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<NextPathDbContext>();
    db.Database.EnsureCreated();
}

// **Swagger SEM IF** -> sempre ligado
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
