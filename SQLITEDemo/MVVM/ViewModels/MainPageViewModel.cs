using Bogus;
using PropertyChanged;
using SQLITEDemo.MVVM.Models;
using System.Windows.Input;

namespace SQLITEDemo.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel
    {
        public List<Customer> Customers { get; set; }
        public Customer CurrentCustomer { get; set; }

        public ICommand AddOrUpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public MainPageViewModel()
        {

            var orders =
                 App.OrdersRepo.GetItems();
            Refresh();
            GenerateNewCustomer();

            AddOrUpdateCommand = new Command(async() =>
            {
                //App.CustomerRepo.SaveItem(CurrentCustomer);
                App.CustomerRepo.SaveItemWithChildren(CurrentCustomer);
                Console.WriteLine(App.CustomerModelRepo.StatusMessage);
                GenerateNewCustomer();
                Refresh();
            });

            DeleteCommand = new Command(() =>
            {
                App.CustomerRepo.DeleteItem(CurrentCustomer);
                Refresh();
            });
        }
      

        private void GenerateNewCustomer()
        {
            CurrentCustomer = new Faker<Customer>()
                 .RuleFor(x => x.Name, f => f.Person.FullName)
                 .RuleFor(x => x.Address, f => f.Person.Address.Street)
                 .RuleFor(x => x.Phone, f => f.Person.Phone)
                 .RuleFor(x => x.Email, f => f.Person.Email)
                 .RuleFor(x => x.Age, f => f.Random.Int(10, 500))
                 .Generate();

            CurrentCustomer.Passport = new List<Passport>
               {
                    new Passport
                    {
                         ExpirationDate =
                              DateTime.Now.AddDays(30)
                    },
                    new Passport
                    {
                         ExpirationDate =
                              DateTime.Now.AddDays(15)
                    },
               };
        }

        private void Refresh()
        {
            //Customers = App.CustomerModelRepo.GetAll();
            Customers = App.CustomerRepo.GetItemsWithChildren();
            //Customers = App.CustomerRepo.GetItems(x => x.Name.StartsWith("A"));
            //Customers = App.CustomerRepo.GetItems();
            var passports =
                 App.PassportsRepo.GetItems();
        }
    }
}
