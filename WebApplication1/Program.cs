
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Configurations;
using WebApplication1.Data;
using WebApplication1.myloggers;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection.Metadata;
using Microsoft.OpenApi.Models;
using WebApplication1.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.
// we need to mention the DBcontext that is being used by the application 
// without these the application won't work  ============================================================================== important
builder.Services.AddDbContext<CollegeDbContext>(options =>
{             
    // we connect the DbConnection String to the appsettings.json
    options.UseSqlServer(builder.Configuration.GetConnectionString("CollegeAppDbConnection"));
    
});
                                // below line will only allow JSON response and won't support XML format
builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable=true).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description ="JWT Authorization headere using bearer scheme. Example:Bearer sddskmds15r",
            Name="Authorization",
            In=ParameterLocation.Header,
            Scheme="Bearer"

        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
            new OpenApiSecurityScheme // enforeces security requirement for all endpoint
            {
                Reference = new OpenApiReference 
                {
                    Id="Bearer",
                    Type=ReferenceType.SecurityScheme,

                }, // CONFIGS a JWT token for accessing endpoints
                Scheme="oauth2",
                Name="Bearer",
                In=ParameterLocation.Header
            },
            new List<string>()
            }
        });
    }

    
    
    
    );  // here is the line where JWT auth is added for swagger UI

builder.Services.AddAutoMapper(typeof(AutoMapperConfig)); 
// initializing the AutoMapper configuratoins from the AutoMapperConfig

builder.Services.AddTransient<IMyLogger, Logtofile>();
builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddTransient(typeof(ICollegeRepository<>), typeof(CollegeRepository<>));
builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddCors(options => options.AddPolicy("MyTestCORS", policy =>
//{

//    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
//    // allow only few origins
//    //policy.WithOrigins("http://localhost:4200"); only allows this url and it acts asthe request with credential Scenario
//}
//)
//); This is only one policy using direct arrow method , to define any number of policy , create a body and define all the policy in it
var keyJWTSecretforLocalUsers = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecretforLocal"));
var keyJWTSecretforGoogleUsers = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecretforGoogle"));
var keyJWTSecretforMicrosoftUsers = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecretforMicrosoft"));
string GoogleAudience = builder.Configuration.GetValue<string>("GoogleAudience");
string MicrosoftAudience = builder.Configuration.GetValue<string>("MicrosoftAudience");
string LocalAudience = builder.Configuration.GetValue<string>("LocalAudience");
string GoogleIssuer = builder.Configuration.GetValue<string>("GoogleIssuer");
string MicrosoftIssuer = builder.Configuration.GetValue<string>("MicrosoftIssuer");
string LocalIssuer = builder.Configuration.GetValue<string>("LocalIssuer");
builder.Services.AddCors(options =>

{ // when defining default policies , no need to mention in the name below => app.UseCors("MyTestCORS");

    //options.AddDefaultPolicy(policy =>
    //{
    //    policy.AllowCredentials().AllowAnyMethod();
    //});

    options.AddPolicy("AllOnlyLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
      //  policy.WithOrigins("http://localhost:4200").WithOrigins("").WithHeaders("").WithMethods(""); specifying a particular method , header and origins

    });
    options.AddPolicy("AllowAllMethods", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod();
    });
    options.AddPolicy("AllowGoogle", policy =>
    {
        policy.WithOrigins("http://google.com", "http://gmail.com");
    });
    options.AddPolicy("AllowMicrosoft", policy =>
    {
        policy.WithOrigins("http://google.com", "http://gmail.com");
    });

}
); // allowing multiple policies 

// JWT Authentication Configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;// expects authentication using JWT tokens
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // define how apps should respond to unauthorized access


}
    ).AddJwtBearer("LoginForGoogleUsers",options =>
    {
        //options.RequireHttpsMetadata=false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,// ensures token is signed using correct secret key . API will reject the token if the signing key does not match
            IssuerSigningKey = new SymmetricSecurityKey(keyJWTSecretforGoogleUsers), // defines the secret key used to sign and verify JWT tokens
            
            
            
            ValidateIssuer = true, // validation of the issuer is made true to ensure that issuer is validated
            ValidIssuer=GoogleIssuer, // so here it defines the valid issuer of the services
            
            ValidateAudience = true, // validation of the audience is set to false
            ValidAudience=GoogleAudience, // 

        };
    }).AddJwtBearer("LoginForMicrosoftUsers", options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyJWTSecretforMicrosoftUsers),

            ValidateIssuer = true, // validation of the issuer is made false
            ValidIssuer=MicrosoftIssuer,

            ValidateAudience = false, // validation of the audience is set to false
            ValidAudience=MicrosoftAudience,
        };
    }).AddJwtBearer("LoginForLocalUsers", options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyJWTSecretforLocalUsers),

            ValidateIssuer = true, 
            ValidIssuer=LocalIssuer,

            ValidateAudience = true,
            ValidAudience=LocalAudience,
        };
    });


var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) 
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}  

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("AllowAllMethods");
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/api/testingendpoint1",
        context => context.Response.WriteAsync("echo"))
        .RequireCors("AllOnlyLocalhost");

    endpoints.MapControllers()
             .RequireCors("AllowAllMethods");

    endpoints.MapGet("/api/testingendpoint2",
        context => context.Response.WriteAsync(builder.Configuration.GetValue<string>("JWTSecret")));

    //endpoints.MapRazorPages();
});

//app.MapControllers(); this s the middle ware used for attribute 
// based routing+
app.Run();
/*
Steps to start with a new ASP . NET CORE PROJECT 
1) install all the EFC packages design , tools , sqlServer
2) create all the tables from the database and perform database first approach 
3) mention the connection string in appsettings.json file and configure it in program.cs file 
4) for performing patch operation install the packages mentioned in the student controller
5) if performing code first approach - > create classed -> create a common DbContext and then connect it with a SqlServer database using the connection string
6) start with creating repositories , all methods in a common repo
7) then start with building services and later the controllers 
8) use autoMapper if needed -> install AutoMapper and AutoMapper with DepedencyInjection and create a folder called Configurations and then create AutoMapperConfig
9) once the repository is delcared with interface and its implementation , then initialize it 
    in the program.cs file



*/
