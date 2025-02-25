var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();//Theem 1
builder.Services.AddDistributedMemoryCache(); //them 2
var app = builder.Build();
app.UseRouting(); //THem 3
app.UseSession();//THem 4
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
 
app.MapAreaControllerRoute(
        name: "AdminArea",
        areaName: "Admin",
        pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
        );

app.MapAreaControllerRoute(
        name: "NguoiLaoDongArea",
        areaName: "NguoiLaoDong",
        pattern: "NguoiLaoDong/{controller=Home}/{action=Index}/{id?}"
        );

app.MapAreaControllerRoute(
        name: "NguoiTuyenDungArea",
        areaName: "NguoiTuyenDung",
        pattern: "NguoiTuyenDung/{controller=Home}/{action=Index}/{id?}"
        );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
