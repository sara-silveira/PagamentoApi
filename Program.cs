var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<PagamentoService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("*") // ou "*" para teste
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors();

app.MapGet("/health_check", () => "Api de pagamento esté no ar!");

app.MapPost("/checkout", (string periodicity, PagamentoService pagamentoService) =>
{
	var result = pagamentoService.CheckoutStripeMonthly(periodicity);
	
	return Results.Ok(result);
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

