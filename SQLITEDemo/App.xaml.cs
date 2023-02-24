using SQLITEDemo.MVVM.Models;
using SQLITEDemo.MVVM.Views;
using SQLITEDemo.Repositories;

namespace SQLITEDemo;

public partial class App : Application
{
    public static CustomerModelRepository CustomerModelRepo { get; private set; }
    public static BaseRepository<Customer> CustomerRepo { get; private set; }
    public static BaseRepository<Order> OrdersRepo { get; private set; }
    public static BaseRepository<Passport> PassportsRepo { get; private set; }
    public App(CustomerModelRepository CustModelRepo, BaseRepository<Customer> repo,
          BaseRepository<Order> ordersRepo,
          BaseRepository<Passport> passportsRepo)
	{
		InitializeComponent();
        CustomerModelRepo = CustModelRepo;
        CustomerRepo = repo;
        OrdersRepo = ordersRepo;
        PassportsRepo = passportsRepo;

        MainPage = new MainPage();
	}
}
