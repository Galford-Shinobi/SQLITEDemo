using SQLite;
using SQLITEDemo.MVVM.Models;
using System.Linq;
using System.Linq.Expressions;

namespace SQLITEDemo.Repositories
{
    public class CustomerModelRepository
    {
        SQLiteConnection connection;
        //https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-7.0
        public string StatusMessage { get; set; }
        public CustomerModelRepository()
        {
            connection =
                    new SQLiteConnection(Constants.DatabasePath,
                    Constants.Flags);
            connection.CreateTable<CustomerModel>();
        }
        public void AddOrUpdate(CustomerModel customer)
        {
            int result = 0;
            try
            {
                if (customer.ID != 0)
                {
                    result =
                         connection.Update(customer);
                    StatusMessage =
                         $"{result} row(s) updated";
                }
                else
                {
                    result = connection.Insert(customer);
                    StatusMessage =
                         $"{result} row(s) added";
                }

            }
            catch (Exception ex)
            {
                StatusMessage =
                     $"Error: {ex.Message}";
            }
        }

        public List<CustomerModel> GetAll()
        {
            try
            {
                return connection.Table<CustomerModel>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage =
                     $"Error: {ex.Message}";
            }
            return null;
        }

        public List<CustomerModel> GetAll(Expression<Func<CustomerModel, bool>> predicate)
        {
            try
            {
                return connection.Table<CustomerModel>().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                StatusMessage =
                     $"Error: {ex.Message}";
            }
            return null;
        }

        public List<CustomerModel> GetAll2()
        {
            try
            {
                return connection.Query<CustomerModel>("SELECT * FROM CustomerModels").ToList();
            }
            catch (Exception ex)
            {
                StatusMessage =
                     $"Error: {ex.Message}";
            }
            return null;
        }

        public CustomerModel Get(int id)
        {
            try
            {
                return
                     connection.Table<CustomerModel>()
                     .FirstOrDefault(x => x.ID == id);
            }
            catch (Exception ex)
            {
                StatusMessage =
                     $"Error: {ex.Message}";
            }
            return null;
        }

        public void Delete(int customerId)
        {
            try
            {
                var customer =
                     Get(customerId);
                connection.Delete(customer);
            }
            catch (Exception ex)
            {
                StatusMessage =
                     $"Error: {ex.Message}";
            }
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}
