using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection"));

});
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowAll",
//         builder => builder.AllowAnyOrigin()
//                           .AllowAnyMethod()
//                           .AllowAnyHeader());
// });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder => builder
            .WithOrigins("https://modestastravels.com", "https://www.modestastravels.com") 
            .AllowAnyMethod() 
            .AllowAnyHeader() 
            .AllowCredentials());
});


builder.Services.AddScoped<IStorageServiceRepository, StorageServiceRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<ITrailRepository, TrailRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IGalleryRepository, GalleryRepository>();
builder.Services.AddScoped<ISubscriberRepository, SubscriberRepository>();
builder.Services.AddScoped<TokenAuthAttribute>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseCors("AllowSpecificOrigins");
app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync();
